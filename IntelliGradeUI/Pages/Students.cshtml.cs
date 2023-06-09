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
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            else if (classId == null ||classId=="")
            {
                ToastService.createErrorToast("S�n�f bulunamad�", Response);
                Response.Redirect("/Index");
            }
            else { 


            ToastService.deleteToasts(Response);
            // teacher list

            string strTeachers = GetRequests.Get("lesson", "getteachers/" + classId, Request.Cookies["Token"]).message;
            classTeachers = JsonConvert.DeserializeObject<List<User>>(strTeachers);


            // student list

            string strStudents = GetRequests.Get("lesson", "getstudents/" + classId, Request.Cookies["Token"]).message;
            classStudents = JsonConvert.DeserializeObject<List<User>>(strStudents);
            }
        }

        public bool isTeacher()
        {
            string result = GetRequests.Get("user", "getuser", Request.Cookies["Token"]).message;
            User currentUser = JsonConvert.DeserializeObject<User>(result);
            bool isTeacher = false;
            if (classTeachers != null)
            {
            classTeachers.ForEach(teacher =>
            {
                if (currentUser.id == teacher.id)
                {
                    isTeacher = true;
                }
            });
            }
            return isTeacher;
        }

        public void OnPostInviteTeacher()
        {
            SendInvite sendInvite = new SendInvite();
            sendInvite.receiverEmail = teacherMail;
            sendInvite.classId = classId;
            if (PostRequests.Post(sendInvite, "Invite", "sendinvite", Request.Cookies["Token"]).status == "Created")
            {
                ToastService.createSuccessToast("��retmen ba�ar�yla davet edildi.",Response);

            }
            else
            {
                ToastService.createErrorToast("��retmen davet edilirken bir hata olu�tu.",Response);

            }
            Response.Redirect("/Students?classId=" + classId);
        }
    }
}
