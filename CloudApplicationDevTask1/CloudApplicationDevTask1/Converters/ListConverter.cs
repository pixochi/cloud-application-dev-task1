using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApplicationDevTask1.Converters
{
    public static class ListConverter
    {
        public static List<float> StringToFloat(List<string> stringList)
        {
            List<float> floatList = new List<float>();

            foreach (string stringItem in stringList) {
                floatList.Add(float.Parse(stringItem));
            }

            return floatList;
        }
    }
}
