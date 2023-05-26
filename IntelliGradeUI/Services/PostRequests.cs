﻿using IntelliGradeUI.Models;
using System.Text;

namespace IntelliGradeUI.Services
{
    public class PostRequests
    {
        public static Response Post(object model, string controllerName, string path)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
            using var client = new HttpClient();

            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();

            return new Response(result, statusCode);
        }

        public static Response Post(object model, string controllerName, string path, string token)
        {
            JsonContent content = JsonContent.Create(model);

            var json = content.ReadAsStringAsync().Result;
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://intelligradebackend.azurewebsites.net/api/" + controllerName + "/" + path;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            var response = client.PostAsync(url, data);
            string result = response.Result.Content.ReadAsStringAsync().Result;
            string statusCode = response.Result.StatusCode.ToString();
            
            return new Response(result, statusCode);
        }

    }
}
