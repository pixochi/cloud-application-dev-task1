using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CloudApplicationDevTask1;

namespace Task1GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        delegate string ValidationFunction(string fileContent);
        private string TASK_ALLOCATION_FILE_PATH = System.IO.Path.GetFullPath("..\\..\\..\\..\\Task_1_Files\\MyTestFile1.tan");
        private List<string> allocationFileErrors;
        private List<ValidationFunction> TASK_ALLOCATION_FILE_VALIDATION_METHODS;

        public MainWindow()
        {
            InitializeComponent();
            allocationFileErrors = new List<string>();
            TASK_ALLOCATION_FILE_VALIDATION_METHODS = new List<ValidationFunction>() {
               TaskAllocationFileValidator.ContainsValidConfigPath,
               TaskAllocationFileValidator.DataMixedWithComments,
            };
        }


        private void ValidateFilesClick(object sender, RoutedEventArgs e)
        {
            string taskAllocationFileContent = FileService.ReadFile(TASK_ALLOCATION_FILE_PATH);
            foreach (var taskAllocationFileValidator in TASK_ALLOCATION_FILE_VALIDATION_METHODS) {
                string errorMsg = taskAllocationFileValidator(taskAllocationFileContent);
                this.allocationFileErrors.Add(errorMsg);
            }

            string allErrors = "";
            foreach (var errorMsg in allocationFileErrors) {
                allErrors += errorMsg + "\n";
            }
            MessageBox.Text = allErrors;
        }
    }
}
