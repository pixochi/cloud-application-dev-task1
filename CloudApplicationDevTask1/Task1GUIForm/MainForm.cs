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
            bool isTANFileValid = validateTANFile(TANfileContent);
            bool isConfigFileValid = validateConfigFile(configFileContent);

            if (isTANFileValid && isConfigFileValid)
            {
                var allocations = TaskAllocationFileParser.GetAllocations(TANfileContent);
                Console.WriteLine(allocations);
                foreach (var allocation in allocations) {
                    float energyConsumed = ConfigFileValidator.GetTotalEnergyConsumed(configFileContent, allocation.Value);
                    mainFormLabel.Text += $"Allocation {allocation.Key}\n";
                    mainFormLabel.Text += $"Energy = {energyConsumed}\n\n";
                }
            }
        }

        private void initFileValidation()
        {
            if (errorsForm == null) {
                errorsForm = new ErrorsForm();
            }

            mainFormLabel.Text = "";
            errorsForm.ClearErrors();
        }

        private bool validateTANFile(string TANfileContent)
        {
            List<string> TANErrors = TaskAllocationFileValidator.ValidateAll(TANfileContent);

            if (TANErrors.Count == 0)
            {
                mainFormLabel.Text += "TAN file is valid.\n";
                return true;
            }
            else 
            {
                mainFormLabel.Text += "TAN file is invalid.\n";
                foreach (var errorMsg in TANErrors)
                {
                    errorsForm.AppendError(errorMsg);
                }
                return false;
            }
        }

        private bool validateConfigFile(string configFileContent)
        {
            List<string> configErrors = ConfigFileValidator.ValidateAll(configFileContent);

            if (configErrors.Count == 0)
            {
                mainFormLabel.Text += "Configuration file is valid.\n\n";
                return true;
            }
            else
            {
                mainFormLabel.Text += "Configuration file is invalid.\n\n";
                foreach (var errorMsg in configErrors) {
                    errorsForm.AppendError(errorMsg);
                }
                return false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
