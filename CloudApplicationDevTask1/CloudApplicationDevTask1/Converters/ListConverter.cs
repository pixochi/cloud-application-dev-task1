using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Converters
{
    // Converts List and its items to other data types
    public static class ListConverter
    {
        /// <summary>
        /// Converts string representation of items to its corresponding float values
        /// </summary>
        /// <returns>
        /// Converted List of floats
        /// </returns>
        /// <param name="stringList">List of strings to be converted</param>
        public static List<float> StringToFloat(List<string> stringList)
        {
            List<float> floatList = new List<float>();

            foreach (string stringItem in stringList)
            {
                floatList.Add(float.Parse(stringItem));
            }

            return floatList;
        }
    }
}
