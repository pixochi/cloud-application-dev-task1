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
using CloudApplicationDevTask1.Services;

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
                displayAllocationDetails(TANfileContent, configFileContent);
            }
        }

        private void initFileValidation()
        {
            if (errorsForm == null) {
                errorsForm = new ErrorsForm();
            }

            mainFormTextBox.Text = "";
            errorsForm.ClearErrors();
        }

        private bool validateTANFile(string TANfileContent)
        {
            List<string> TANErrors = TaskAllocationFileValidator.ValidateAll(TANfileContent);

            if (TANErrors.Count == 0)
            {
                mainFormTextBox.AppendText($"TAN file is valid.{Environment.NewLine}");
                return true;
            }
            else 
            {
                mainFormTextBox.AppendText($"TAN file is invalid.{Environment.NewLine}");
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
                mainFormTextBox.Text += "Configuration file is valid.";
                mainFormTextBox.AppendText(Environment.NewLine);
                mainFormTextBox.AppendText(Environment.NewLine);
                return true;
            }
            else
            {
                mainFormTextBox.Text += "Configuration file is invalid.";
                mainFormTextBox.AppendText(Environment.NewLine);
                foreach (var errorMsg in configErrors) {
                    errorsForm.AppendError(errorMsg);
                }
                return false;
            }
        }

        private void displayAllocationDetails(string TANFileContent, string configFileContent)
        {
            var allocations = TaskAllocationFileParser.GetAllocations(TANFileContent);
            foreach (var allocation in allocations) {
                float allocationRuntime = AllocationService.GetAllocationRuntime(configFileContent, allocation.Value);
                string allocationRuntimeErrorMsg = AllocationService.IsAllocationRuntimeValid(configFileContent, allocationRuntime);

                displayAllocation(allocation);

                if (allocationRuntimeErrorMsg == "")
                {
                    float energyConsumed = AllocationService.GetTotalEnergyConsumed(configFileContent, allocation.Value);
                    mainFormTextBox.AppendText($"Time = {allocationRuntime}, Energy = {energyConsumed}");
                }
                else
                {
                    mainFormTextBox.AppendText(allocationRuntimeErrorMsg);
                }

                mainFormTextBox.AppendText(Environment.NewLine);
                mainFormTextBox.AppendText(Environment.NewLine);
            }
        }

        private void displayAllocation(KeyValuePair<string, List<List<bool>>> allocation)
        {
            mainFormTextBox.AppendText($"Allocation {allocation.Key}{Environment.NewLine}");
            foreach (var processor in allocation.Value) {
                for (int taskId = 0; taskId < processor.Count; taskId++) {
                    mainFormTextBox.Text += processor[taskId] ? "1" : "0";

                    if (taskId != processor.Count - 1) {
                        mainFormTextBox.Text += ",";
                    }
                }

                mainFormTextBox.AppendText(Environment.NewLine);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
