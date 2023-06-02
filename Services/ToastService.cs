using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;


namespace IntelliGradeUI.Services
{
    public class ToastService
    {

        public static void createSuccessToast(string message, HttpResponse response)
        {
            
            response.Cookies.Append("Success", "show");
            response.Cookies.Append("SuccessMessage", message);
        }

        public static void createErrorToast(string message, HttpResponse response)
        {
            response.Cookies.Append("Error", "show");
            response.Cookies.Append("ErrorMessage", message);
        }

        public static void deleteToasts(HttpResponse response)
        {
            response.Cookies.Delete("Success");
            response.Cookies.Delete("SuccessMessage");
            response.Cookies.Delete("Error");
            response.Cookies.Delete("ErrorMessage");
        }

    }
}
