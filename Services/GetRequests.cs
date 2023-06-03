using IntelliGradeUI.Models;
using Newtonsoft.Json;
using Services;
using System.Net.Http.Json;
using System.Text;

namespace IntelliGradeUI.Services
{
    public class GetRequests : Requests
    {
        public static Response Get(string controllerName, string path)
        {
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();
            var response = client.GetAsync(url);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("GET", url, statusCode, result));

            return new Response(result, statusCode);
        }

        public static Response Get(string controllerName, string path, string token)
        {
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.GetAsync(url);
      
            string result = response.Result.Content.ReadAsStringAsync().Result;
            
            string statusCode =  response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("GET", url, statusCode, result));


            return new Response(result, statusCode);
        }

        

    }
   

}
