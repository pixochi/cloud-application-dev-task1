using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudApplicationDevTask1;
using CloudApplicationDevTask1.validators;
using System.IO;

namespace Task1GUIForm
{
    public partial class MainForm : Form
    {
        private ErrorsForm errorsForm = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void errorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
           if (errorsForm == null) {
                errorsForm = new ErrorsForm();
           }

            errorsForm.Show();
        }

        private void openTANFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fileName = openFileDialog1.FileName;

            string fileContent = FileService.ReadFile(fileName);

            string configFilename = TaskAllocationFileValidator.GetConfigFilename(fileContent);
            string path = Path.GetDirectoryName(fileName);
            string configFilePath = path + @"\" + configFilename;
            string configFileContent = FileService.ReadFile(configFilePath);

            if (errorsForm == null) {
                errorsForm = new ErrorsForm();
            }
            errorsForm.AppendError(configFileContent);
        }
    }
}
