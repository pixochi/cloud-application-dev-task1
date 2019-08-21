using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1GUIForm
{
    public partial class ErrorsForm : Form
    {
        public ErrorsForm()
        {
            InitializeComponent();
        }

        private void ErrorsForm_Load(object sender, EventArgs e)
        {

        }

        private void ErrorsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void AppendError(string error)
        {
            this.errorsLabel.Text += "\n" + error;
        }
    }
}
