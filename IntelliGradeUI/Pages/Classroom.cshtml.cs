using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using IntelliGradeUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using System.Globalization;
using Services;
using IntelliGradeUI.Services;

namespace IntelliGradeUI.Pages
{
    public class ClasroomModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
        [BindProperty]
        public Lesson lesson { get; set; }
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
        public DateTime assignmentDeadline { get; set; }
        [BindProperty]
        public string assignmentFile { get; set; }
        [BindProperty]
        public User currentUser { get; set; }





        MultipartFormDataContent content = new MultipartFormDataContent();




        public void OnGet()
        {
            string result = GetRequests.Get("Assignment", "getbyclass/" + classId, Request.Cookies["Token"]).Result.message;
            this.assignment = JsonConvert.DeserializeObject<List<Assignment>>(result);

            string result2 = GetRequests.Get("lesson", "getbyid/" + classId, Request.Cookies["Token"]).Result.message;
            this.lesson = JsonConvert.DeserializeObject<Lesson>(result2);

            string result3 = GetRequests.Get("user", "getuser", Request.Cookies["Token"]).Result.message;
            this.currentUser = JsonConvert.DeserializeObject<User>(result3);


        }

        public void OnPostCreateAssignment(IFormFile file)
        {
            List<string> dataList = assignmentRequirements.Split("-").ToList();

            string jsonData = "{\"id\":\"string\",\"userId\":\"string\",\"filePath\":\"string\",\"fileUrl\":\"string\"}";

            var content = new MultipartFormDataContent();

            Console.WriteLine("OnPostCreateAssignment");
            content.Add(new StringContent(currentUser.id), "Assignment.TeacherId");
            content.Add(new StringContent(""), "Assignment.FilePath");
            content.Add(new StringContent(""), "Assignment.FileUrl");
            content.Add(new StringContent(assignmentTitle), "Assignment.Title");
            content.Add(new StringContent(assignmentDefinition), "Assignment.Definition");
            content.Add(new StringContent(assignmentDescription), "Assignment.Description");
            foreach (var data in dataList)
            {
                var stringContent = new StringContent(data);
                content.Add(stringContent, "Assignment.Requriments");
            }
            content.Add(new StringContent(jsonData, Encoding.UTF8, "application/json"), "Assignment.UploadedHomeworks");

            DateTime tempDate = DateTime.ParseExact(DateTime.Now.ToString(), "dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture);
            string iso8601Date = tempDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

            DateTime tempDateDeadline = DateTime.ParseExact(assignmentDeadline.ToString(), "dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture);
            string iso8601DateDeadline = tempDateDeadline.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            content.Add(new StringContent(iso8601DateDeadline), "Assignment.Deadline");
            content.Add(new StringContent(iso8601Date), "Assignment.Date");

            string idAssignment = Guid.NewGuid().ToString();

            content.Add(new StringContent(idAssignment), "Assignment.Id");


            if (file != null)
            {

                content.Add(new StreamContent(file.OpenReadStream()), "File", ExtensionGetter.GetExtension(file.FileName));
            }
            else
            {
                content.Add(new StringContent(""), "File", ".null");
            }


            PostRequests.PostOnFormData(content, "Assignment", "create/" + classId, Request.Cookies["Token"].ToString());

            OnGet();


        }

    }
}