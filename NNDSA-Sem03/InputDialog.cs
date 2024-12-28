using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NNDSA_Sem03
{
    public partial class InputDialog : Form
    {
        public string Result { get; set; }
        public InputDialog()
        {
            InitializeComponent();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            Result = textBoxInput.Text;
        }
    }
}
