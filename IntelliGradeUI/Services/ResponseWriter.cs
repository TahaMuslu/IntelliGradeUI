using Microsoft.AspNetCore.Http;
using System;

namespace IntelliGradeUI.Services
{
    public class ResponseWriter
    {

        public static string WriteResponse(string responseType, string url, string json, string statusCode, string result)
        {
            string text = "\n\n--------------------- " + responseType + " ---------------------";
            text += "\nURL: " + url;
            text += "\nRequest Json: " + json;
            text += "\nStatus Code: " + statusCode;
            text += "\nResponse: " + result;
            text += "\n\n";


            return text;
        }

        public static string WriteResponse(string responseType, string url, string statusCode, string result)
        {
            string text = "\n\n--------------------- " + responseType + " ---------------------";
            text += "\nURL: " + url;
            text += "\nRequest Json: " + "-Null-";
            text += "\nStatus Code: " + statusCode;
            text += "\nResponse: " + result;
            text += "\n\n";


            return text;
        }

    }
}
