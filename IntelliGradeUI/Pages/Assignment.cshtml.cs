using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace IntelliGradeUI.Pages
{
    public class AssignmentModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string assignmentId { get; set; }

        [BindProperty]
        public Assignment assignment { get; set; }

        //[BindProperty]
        //public IFormFile file { get; set; }
        public void OnGet()
        {
            string result = GetRequests.Get("Assignment", "getbyid/" + assignmentId, Request.Cookies["Token"]).Result.message;
            assignment = JsonConvert.DeserializeObject<Assignment>(result);

        }


        public async void OnPostUploadFile(IFormFile file)
        {

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(""), "Homework.UserId");
            formContent.Add(new StringContent(""), "Homework.FilePath");
            formContent.Add(new StringContent(""), "Homework.FileUrl");
            formContent.Add(new StringContent("2345423"), "Homework.Id");
            formContent.Add(new StreamContent(file.OpenReadStream()),"File","PCR.pdf");

            PostRequests.PostOnFormData(formContent, "Assignment", "addhomework/123456", Request.Cookies["Token"].ToString());

        }


    }
}
