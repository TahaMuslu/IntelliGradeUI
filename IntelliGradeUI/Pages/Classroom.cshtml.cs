using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace IntelliGradeUI.Pages
{
    public class ClasroomModel : PageModel
    {

        [BindProperty(SupportsGet = true)]//way anasýný millet ne yapýyor be !! adamlar yapmýþlar
        public string classId { get; set; }

        [BindProperty]
        public List<Assignment> assignment { get; set; }
        [BindProperty]
        public string assignmentTitle { get; set; }
        [BindProperty]
        public string assignmentDefinition { get; set; }
        [BindProperty]
        public string assignmentDescription { get; set; }
        [BindProperty]
        public string assignmentRequirements { get; set; }
        [BindProperty]
        public string assignmentDeadline { get; set; }
        [BindProperty]
        public string assignmentFile { get; set; }


    


        MultipartFormDataContent content = new MultipartFormDataContent();




        public void OnGet()
        {
            string result =GetRequests.Get("Assignment", "getbyclass/" + classId, Request.Cookies["Token"]).Result.message;
            Console.WriteLine(result);
            this.assignment = JsonConvert.DeserializeObject<List<Assignment>>(result);
            Console.WriteLine(this.assignment);

        }

        List<string> dataList = new List<string>
        {
            "Veri 1",
            "Veri 2",
            "Veri 3"
        };


        List<Homework> homeWorks = new List<Homework>();

        string jsonData = "{\"id\":\"string\",\"userId\":\"string\",\"filePath\":\"string\",\"fileUrl\":\"string\"}";

        public void OnPostCreateAssignment(IFormFile file)
        {
            var content = new MultipartFormDataContent();

            Console.WriteLine("OnPostCreateAssignment");
            content.Add(new StringContent("353264"), "Assignment.TeacherId");
            content.Add(new StringContent("filePath"), "Assignment.FilePath");
            content.Add(new StringContent("fileUrl"), "Assignment.FileUrl");
            content.Add(new StringContent(assignmentTitle), "Assignment.Title");
            content.Add(new StringContent(assignmentDefinition), "Assignment.Definition");
            content.Add(new StringContent(assignmentDescription), "Assignment.Description");
            foreach (var data in dataList)
            {
                var stringContent = new StringContent(data);
                content.Add(stringContent, "Assignment.Requriments");
            }
      
            content.Add(new StringContent(jsonData, Encoding.UTF8, "application/json") , "Assignment.UploadedHomeworks");
            content.Add(new StringContent("2023-05-29T14:10:09.629Z"), "Assignment.Deadline");

            content.Add(new StringContent("2023-05-29T14:11:04.426Z"), "Assignment.Date");
            
            string idAssignment = Guid.NewGuid().ToString();

            content.Add(new StringContent(idAssignment), "Assignment.Id");


            if(file != null && file.Length>0)
            {
                content.Add(new StreamContent(file.OpenReadStream()), "File", "pdf");

            }
            else
            {
                content.Add(new StringContent(""), "File", "pdf");
            }

            PostRequests.PostOnFormData(content, "Assignment", "create/68470258-8110-4567-ad47-240cc673b019", Request.Cookies["Token"].ToString());






        }

    }
}
