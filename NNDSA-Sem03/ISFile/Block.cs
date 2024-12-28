namespace NNDSA_Sem03.ISFile;

[Serializable]
public class Block<TKey, TValue>
{
    public long Id { get; set; }
    public Dictionary<TKey, TValue> Records { get; set; } = new();
}