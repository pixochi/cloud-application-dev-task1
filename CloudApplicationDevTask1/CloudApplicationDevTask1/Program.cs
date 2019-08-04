using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = System.IO.Path.GetFullPath("..\\..\\..\\..\\Task_1_Files\\Test1.tan");
            string fileContent = FileService.ReadFile(filePath);
            Console.Write(fileContent);
            Console.ReadKey();
        }
    }
}
