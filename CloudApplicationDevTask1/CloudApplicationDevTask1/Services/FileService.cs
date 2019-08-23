using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1
{
    // Service for manipulation with files
    public class FileService
    {
        // Inspired by:
        // https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-read-text-from-a-file
        public static string ReadFile(string path)
        {
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    string fileContent = sr.ReadToEnd();
                    return fileContent;
                }
            }
            catch (IOException e)
            {
                return e.Message;
            }
        }
    }
}
