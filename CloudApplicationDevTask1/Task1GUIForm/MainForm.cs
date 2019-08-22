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
using CloudApplicationDevTask1.Parsers;

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
            string TANfileName = openFileDialog1.FileName;
            string TANfileContent = FileService.ReadFile(TANfileName);

            string configFilename = TaskAllocationFileParser.GetConfigFilename(TANfileContent);
            string path = Path.GetDirectoryName(TANfileName);
            string configFilePath = path + @"\" + configFilename;
            string configFileContent = FileService.ReadFile(configFilePath);

            initFileValidation();
            validateTANFile(TANfileContent);
            validateConfigFile(configFileContent);
        }

        private void initFileValidation()
        {
            if (errorsForm == null) {
                errorsForm = new ErrorsForm();
            }

            errorsForm.ClearErrors();
        }

        private void validateTANFile(string TANfileContent)
        {
            List<string> TANErrors = TaskAllocationFileValidator.ValidateAll(TANfileContent);

            if (TANErrors.Count == 0)
            {
                mainFormLabel.Text += "TAN file is valid.\n";
            }
            else 
            {
                mainFormLabel.Text += "TAN file is invalid.\n";
                foreach (var errorMsg in TANErrors)
                {
                    errorsForm.AppendError(errorMsg);
                }
            }
        }

        private void validateConfigFile(string configFileContent)
        {
            List<string> configErrors = ConfigFileValidator.ValidateAll(configFileContent);

            if (configErrors.Count == 0)
            {
                mainFormLabel.Text += "Configuration file is valid.\n\n";
            }
            else
            {
                mainFormLabel.Text += "Configuration file is invalid.\n\n";
                foreach (var errorMsg in configErrors) {
                    errorsForm.AppendError(errorMsg);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
