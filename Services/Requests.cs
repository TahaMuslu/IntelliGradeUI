using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Requests
    {
        public static string UrlCreate(string controllerName, string path)
        {
            return "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
        }

    }
}
