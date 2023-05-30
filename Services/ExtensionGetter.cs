using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ExtensionGetter
    {

        public static string GetExtension(string fileName)
        {
            string[] split = fileName.Split('.');
            string extension = split[split.Length - 1];
            return extension;
        }

    }
}
