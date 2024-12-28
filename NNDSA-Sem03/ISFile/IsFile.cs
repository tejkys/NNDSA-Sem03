using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace NNDSA_Sem03.ISFile;

public delegate void Logger(string message);

public class IsFile<TKey, TValue> where TKey : IComparable<TKey>
{
    private FileStream _indexFile;
    private FileStream _dataFile;
    private int _recordsAmount = 0;
    private int _recordsPerBlock = 0;
    private int _blocksAmount = 0;
    private int _headerSize = 0;
    private event Logger _logger;
    public IsFile(Logger logger)
    {
        _logger = logger;
        _headerSize = (Serialize(new BlockHeader() { Size = int.MaxValue })).Length;

    }

    public void Build(SortedDictionary<TKey, TValue> data)
    {
        _recordsAmount = data.Count;
        _recordsPerBlock = (int)Math.Sqrt(data.Count);
        _blocksAmount = (int)Math.Ceiling((decimal)(_recordsAmount / _recordsPerBlock));


        OpenStreams(FileMode.Create);
        int dataBlockCounter = 0;
        int indexBlockCounter = 0;
        var block = new Block<TKey, TValue>() {Id = dataBlockCounter };
        var indexBlock = new Block<TKey, long>() {Id = indexBlockCounter};
        foreach (var record in data)
        {
            block.Records.Add(record.Key, record.Value);
            if (block.Records.Count == _recordsPerBlock)
            {
                var offset = WriteDataBlock(block);

                indexBlock.Records.Add(block.Records.First().Key, offset);

                if (indexBlock.Records.Count == _recordsPerBlock)
                {
                    WriteIndexBlock(indexBlock);
                    indexBlock = new Block<TKey, long>() { Id = ++indexBlockCounter };
                }

                block = new Block<TKey, TValue>() { Id = ++dataBlockCounter };
            }
        }

        if (indexBlock.Records.Count > 0)
        {
            WriteIndexBlock(indexBlock);
        }
        CloseStreams();

    }
    private void WriteIndexBlock(Block<TKey, long> block)
    {
        var blockBytes = Serialize(block);

        BinaryWriter writer = new BinaryWriter(_indexFile);
        writer.Write(blockBytes.Length);
        writer.Write(blockBytes);

    }
    private long WriteDataBlock(Block<TKey, TValue> block)
    {
        _logger($"Writing to block ID={block.Id}");

        var blockBytes = Serialize(block);
        var headerBytes = Serialize(new BlockHeader() { Size = blockBytes.Length });
        if (_headerSize == 0)
        {
            _headerSize = headerBytes.Length;
        }
        BinaryWriter writer = new BinaryWriter(_dataFile);
        long position = writer.BaseStream.Position;
        writer.Write(headerBytes);
        writer.Write(blockBytes);
        return position;
    }

    private BlockHeader ReadHeader(long offset)
    {
        BinaryReader reader = new BinaryReader(_dataFile);
        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
        var bytes = reader.ReadBytes(_headerSize);
        return Deserialize<BlockHeader>(bytes);
    }
    private Block<TKey, TValue> ReadDataBlock(long offset, int size, bool log = true)
    {
        BinaryReader reader = new BinaryReader(_dataFile);
        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
        var bytes = reader.ReadBytes(size);
        var block = Deserialize<Block<TKey, TValue>>(bytes);
        if(log)
            _logger($"Reading block ID={block.Id}");
        return block;
    }
    private Block<TKey, long> ReadIndexBlock(long offset, int size)
    {
        BinaryReader reader = new BinaryReader(_indexFile);
        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
        var bytes = reader.ReadBytes(size);
        return Deserialize<Block<TKey, long>>(bytes);
    }

    public TValue? Find(TKey key)
    {
        long indexOffset = 0;
        OpenStreams(FileMode.Open);
        BinaryReader indexReader = new BinaryReader(_indexFile);
        BinaryReader dataReader = new BinaryReader(_dataFile);
        while (indexOffset < indexReader.BaseStream.Length)
        {
            indexReader.BaseStream.Seek(indexOffset, SeekOrigin.Begin);
            var indexBlockSize = indexReader.ReadInt32();
            var indexBlock = ReadIndexBlock(indexOffset + sizeof(int), indexBlockSize);
            indexOffset = sizeof(int) + indexBlockSize;
            for (int i = 0; i < indexBlock.Records.Count; i++)
            {
                var blockAIndex = indexBlock.Records.ElementAt(i);
                KeyValuePair<TKey, long>? blockBIndex = (i + 1) < indexBlock.Records.Count ? indexBlock.Records.ElementAt(i + 1) : null;

                KeyValuePair<TKey, long>? blockIndexToExplore = null;
                if (blockAIndex.Key.CompareTo(key) <= 0)
                {
                    if (blockBIndex != null && blockBIndex.Value.Key.CompareTo(key) > 0)
                    {
                        blockIndexToExplore = blockAIndex;
                    } else if (blockBIndex != null && blockBIndex.Value.Key.CompareTo(key) <= 0)
                    {
                        blockIndexToExplore = null;
                    }
                    else
                    {
                        blockIndexToExplore = blockAIndex;
                    }
                }
                else
                {
                    CloseStreams();
                    return default(TValue?);
                }

                if (blockIndexToExplore != null)
                {
                    var dataHeader = ReadHeader(blockIndexToExplore.Value.Value);
                    var dataBlock = ReadDataBlock(blockIndexToExplore.Value.Value + _headerSize, dataHeader.Size);
                    foreach (var dataRecord in dataBlock.Records)
                    {
                        _logger($"Comparing with {dataRecord.Key}");

                        if (dataRecord.Key.CompareTo(key) == 0)
                        {
                            CloseStreams();

                            return dataRecord.Value;
                        }
                    }
                }
            }
        }
        CloseStreams();
        return default(TValue?);
    }
    public void TraverseKeys()
    {
        long indexOffset = 0;
        OpenStreams(FileMode.Open);
        BinaryReader indexReader = new BinaryReader(_indexFile);
        BinaryReader dataReader = new BinaryReader(_dataFile);
        var resultMessage = String.Empty;
        while (indexOffset < indexReader.BaseStream.Length)
        {
            indexReader.BaseStream.Seek(indexOffset, SeekOrigin.Begin);
            var indexBlockSize = indexReader.ReadInt32();
            var indexBlock = ReadIndexBlock(indexOffset+sizeof(int), indexBlockSize);
            indexOffset = sizeof(int) + indexBlockSize;
            foreach (var record in indexBlock.Records)
            {
                var dataHeader = ReadHeader(record.Value);
                var dataBlock = ReadDataBlock(record.Value + _headerSize, dataHeader.Size, false);
                resultMessage += $"Reading block ID={dataBlock.Id}{Environment.NewLine}";

                foreach (var dataRecord in dataBlock.Records)
                {
                    resultMessage += $"Key={dataRecord.Key}" + Environment.NewLine;
                }
            }
        }
        _logger(resultMessage);
        CloseStreams();
    }
    private static TData Deserialize<TData>(byte[] data)
    {
        using var stream = new MemoryStream(data);
        var formatter = new BinaryFormatter();
        stream.Seek(0, SeekOrigin.Begin);
        return (TData)formatter.Deserialize(stream);
    }

    private static byte[] Serialize<TData>(TData data)
    {
        using var stream = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Flush();
        stream.Position = 0;
        return stream.ToArray();
    }

    private void OpenStreams(FileMode mode)
    {
        _indexFile = new FileStream("index.dat", mode);
        _dataFile = new FileStream("data.dat", mode);
    }

    private void CloseStreams()
    {
        _indexFile.Close();
        _dataFile.Close();
    }
}