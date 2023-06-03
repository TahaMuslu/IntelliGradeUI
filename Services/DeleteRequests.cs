using IntelliGradeUI.Models;
using Services;
using System.Text;

namespace IntelliGradeUI.Services
{
    public class DeleteRequests : Requests
    {

        public static Response Delete(string controllerName, string path, string token)
        {
            var url = Requests.UrlCreate(controllerName, path);
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.DeleteAsync(url);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            Console.WriteLine(ResponseWriter.WriteResponse("DELETE", url, statusCode, result));

            return new Response(result, statusCode);
        }

    }
}
