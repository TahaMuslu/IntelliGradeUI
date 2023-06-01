using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IntelliGradeUI.Models;
using Newtonsoft.Json;
using System.Text;
using Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;

namespace IntelliGradeUI.Pages
{
    public class AssignmentModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string assignmentId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }

        [BindProperty]
        public Assignment assignment { get; set; }

        [BindProperty]
        public User currentUser { get; set; }

        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            string result = GetRequests.Get("Assignment", "getbyid/" + assignmentId, Request.Cookies["Token"]).Result.message;
            assignment = JsonConvert.DeserializeObject<Assignment>(result);
            currentUser = JsonConvert.DeserializeObject<User>(GetRequests.Get("user", "getuser", Request.Cookies["Token"]).Result.message);
        }


        public void OnPostUploadFile(IFormFile file)
        {

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(""), "Homework.UserId");
            formContent.Add(new StringContent(""), "Homework.FilePath");
            formContent.Add(new StringContent(""), "Homework.FileUrl");
            formContent.Add(new StringContent(Guid.NewGuid().ToString()), "Homework.Id");
            formContent.Add(new StreamContent(file.OpenReadStream()),"File",ExtensionGetter.GetExtension(file.FileName));

            PostRequests.PostOnFormData(formContent, "Assignment", "addhomework/"+assignmentId , Request.Cookies["Token"].ToString());

            Response.Redirect("/Assignment?assignmentId=" + assignmentId);
        }

        public void OnPostDeleteFile()
        {

            DeleteRequests.Delete("Assignment", "removehomework/" + assignmentId, Request.Cookies["Token"].ToString());

            Response.Redirect("/Assignment?assignmentId=" + assignmentId);
        }

        public void OnPostDeleteAssignment()
        {
            DeleteRequests.Delete("Assignment", "delete/" + assignmentId+"/"+classId, Request.Cookies["Token"].ToString());
            Response.Redirect("/Classroom?classId=" + classId);
        }


        public string findStudentFile()
        {
            string file = "";
            assignment.uploadedHomeworks.ForEach(homework =>
            {
                if (homework.userId == currentUser.id)
                {
                    file = homework.fileUrl;
                }
            });
            return file;
        }


    }
}
