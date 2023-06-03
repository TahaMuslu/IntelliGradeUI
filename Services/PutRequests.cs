using IntelliGradeUI.Models;
using Services;
using System.Net.Http.Json;
using System.Text;

namespace IntelliGradeUI.Services
{
    public class PutRequests : Requests
    {

        public static Response Put(string controllerName, string path, string token)
        {

            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.PutAsync(url, null);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("PUT", url, statusCode, result));

            return new Response(result, statusCode);
        }

        public static Response Put(string controllerName, string path)
        {

            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            var response = client.PutAsync(url, null);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("PUT", url, statusCode, result));

            return new Response(result, statusCode);
        }

        public static Response Put(object model, string controllerName, string path)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            var response = client.PutAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("PUT", url, json, statusCode, result));

            return new Response(result, statusCode);
        }

        public static Response Put(object model, string controllerName, string path, string token)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.PutAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("PUT", url, json, statusCode, result));

            return new Response(result, statusCode);
        }


    }
}
