using IntelliGradeUI.Models;
using Services;
using System.Net.Http.Json;
using System.Text;

namespace IntelliGradeUI.Services
{
    public class PostRequests : Requests
    {
        public static Response Post(object model, string controllerName, string path)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();
            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("POST", url, json, statusCode, result));

            return new Response(result, statusCode);
        }

        public static Response Post(object model, string controllerName, string path, string token)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("POST", url, json, statusCode, result));

            return new Response(result, statusCode);
        }

        public static Response PostOnFormData(MultipartFormDataContent form, string controllerName, string path, string token)
        {
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = client.PostAsync(url, form);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("POST", url, form.ReadAsStringAsync().Result, statusCode, result));

            return new Response(result, statusCode);
        }


    }
}
