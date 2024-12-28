using System.Collections.Specialized;
using NNDSA_Sem03.ISFile;

namespace NNDSA_Sem03
{
    public partial class Form1 : Form
    {
        private const int RecordsAmount = 10000;
        private IsFile<int, string> _isFile;
        public Form1()
        {
            InitializeComponent();
            _isFile = new IsFile<int, string>(Log);
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            var inputDialog = new InputDialog();
            var result = inputDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(inputDialog.Result) && int.TryParse(inputDialog.Result, out int pattern))
            {
                var findResult = _isFile.Find(Convert.ToInt32(pattern));
                Log(findResult is null? $"The key {pattern} not found." : $"Found: {findResult}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxOutput.Clear();
        }

        private void Log(string message)
        {
            textBoxOutput.Text += message + Environment.NewLine;
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            Log($"Building file for {RecordsAmount:#,##0} records.");
            var data = new SortedDictionary<int, string>();
            for (int i = 0; i < RecordsAmount; i++)
            {
                data.Add(i, Guid.NewGuid().ToString());
            }
            _isFile.Build(data);
            Log($"File for {RecordsAmount:#,##0} records has been built.");

        }

        private void buttonKeys_Click(object sender, EventArgs e)
        {
            _isFile.TraverseKeys();
        }
    }
}