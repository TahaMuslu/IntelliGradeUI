using IntelliGradeUI.Models;
using System.Text;

namespace IntelliGradeUI.Services
{
    public class PutRequests
    {

        public static Response Put(string controllerName, string path, string token)
        {

            var url = "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.PutAsync(url, null);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine("URL: " + url);
            Console.WriteLine("Status Code: " + statusCode);
            Console.WriteLine("Response: " + result);

            return new Response(result, statusCode);
        }

        public static Response Put(string controllerName, string path)
        {

            var url = "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
            using var client = new HttpClient();

            var response = client.PutAsync(url, null);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine("URL: " + url);
            Console.WriteLine("Status Code: " + statusCode);
            Console.WriteLine("Response: " + result);

            return new Response(result, statusCode);
        }

        public static Response Put(object model, string controllerName, string path)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
            using var client = new HttpClient();

            var response = client.PutAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine("URL: " + url);
            Console.WriteLine("Request Json: " + json);
            Console.WriteLine("Status Code: " + statusCode);
            Console.WriteLine("Response: " + result);

            return new Response(result, statusCode);
        }

        public static Response Put(object model, string controllerName, string path, string token)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.PutAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine("URL: " + url);
            Console.WriteLine("Request Json: " + json);
            Console.WriteLine("Status Code: " + statusCode);
            Console.WriteLine("Response: " + result);

            return new Response(result, statusCode);
        }


    }
}
