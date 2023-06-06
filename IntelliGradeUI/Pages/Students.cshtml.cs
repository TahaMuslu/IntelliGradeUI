using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class StudentsModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
 
        [BindProperty]
        public List<User> classTeachers { get; set; }

        [BindProperty]
        public List<User> classStudents { get; set; }

        [BindProperty]
        public string teacherMail { get; set; }


        public void OnGet()
        {
            ToastService.deleteToasts(Response);
            // teacher list

            string strTeachers = GetRequests.Get("lesson", "getteachers/" + classId, Request.Cookies["Token"]).message;
            classTeachers = JsonConvert.DeserializeObject<List<User>>(strTeachers);


            // student list

            string strStudents = GetRequests.Get("lesson", "getstudents/" + classId, Request.Cookies["Token"]).message;
            classStudents = JsonConvert.DeserializeObject<List<User>>(strStudents);

        }

        public void OnPostInviteTeacher()
        {
            SendInvite sendInvite = new SendInvite();
            sendInvite.receiverEmail = teacherMail;
            sendInvite.classId = classId;
            if (PostRequests.Post(sendInvite, "Invite", "sendinvite", Request.Cookies["Token"]).status == "Created")
            {
                ToastService.createSuccessToast("Öðretmen baþarýyla davet edildi.",Response);

            }
            else
            {
                ToastService.createErrorToast("Öðretmen davet edilirken bir hata oluþtu.",Response);

            }
            Response.Redirect("/Students?classId=" + classId);
        }
    }
}
