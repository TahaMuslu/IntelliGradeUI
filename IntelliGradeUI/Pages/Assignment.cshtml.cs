using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IntelliGradeUI.Models;
using Newtonsoft.Json;
using System.Text;
using IntelliGradeUI.Services;
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
        [BindProperty]
        public string cuser_id { get; set; }

        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            string result = GetRequests.Get("Assignment", "getbyid/" + assignmentId, Request.Cookies["Token"]).message;
            assignment = JsonConvert.DeserializeObject<Assignment>(result);
            currentUser = JsonConvert.DeserializeObject<User>(GetRequests.Get("user", "getuser", Request.Cookies["Token"]).message);
            ToastService.deleteToasts(Response);
        }


        public void OnPostUploadFile(IFormFile file)
        {

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(""), "Homework.UserId");
            formContent.Add(new StringContent(""), "Homework.FilePath");
            formContent.Add(new StringContent(""), "Homework.FileUrl");
            formContent.Add(new StringContent(""), "Homework.FileName");
            formContent.Add(new StringContent(Guid.NewGuid().ToString()), "Homework.Id");
            formContent.Add(new StreamContent(file.OpenReadStream()),"File",ExtensionGetter.GetExtension(file.FileName));
            if(PostRequests.PostOnFormData(formContent, "Assignment", "addhomework/" + assignmentId, Request.Cookies["Token"].ToString()).status== "OK")
                ToastService.createSuccessToast("Dosya baþarýyla yüklendi.", Response);
            else
                ToastService.createErrorToast("Dosya yüklenirken hata oluþtu.", Response);

            if(GetRequests.Get("AI", "getnote?aid=" + assignmentId + "&uid=" + cuser_id, Request.Cookies["Token"]).status== "OK")
                ToastService.createSuccessToast("Dosya notlandýrýldý.", Response);
            else
                ToastService.createErrorToast("Dosya notlandýrýlmasýnda hata oluþtu.", Response);
            Response.Redirect("/Assignment?assignmentId=" + assignmentId);
        }

        public void OnPostDeleteFile()
        {
            if(DeleteRequests.Delete("Assignment", "removehomework/" + assignmentId, Request.Cookies["Token"].ToString()).status== "OK")
                ToastService.createSuccessToast("Dosya baþarýyla silindi.", Response);
            else
                ToastService.createErrorToast("Dosya silinirken hata oluþtu.", Response);
            

            Response.Redirect("/Assignment?assignmentId=" + assignmentId);
        }

        public void OnPostDeleteAssignment()
        {
            if(DeleteRequests.Delete("Assignment", "delete/" + assignmentId + "/" + classId, Request.Cookies["Token"].ToString()).status=="OK")
                ToastService.createSuccessToast("Ödev baþarýyla silindi.", Response);
            else
                ToastService.createErrorToast("Ödev silinirken hata oluþtu.", Response);

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
