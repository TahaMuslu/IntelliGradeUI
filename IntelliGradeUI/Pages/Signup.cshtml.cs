using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace IntelliGradeUI.Pages
{
    public class SignupModel : PageModel
    {

        [BindProperty]
        public string nameSurname { get; set; }

        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }



        public void OnGet()
        {
        }



        public void OnPost()
        {


            var user = new User();
            user.id = Guid.NewGuid().ToString();
            user.nameSurname = nameSurname;
            user.email = email;
            user.password = password;
            user.teacherGrades = new List<string>();
            user.studentGrades = new List<string>();

            Console.WriteLine(PostRequests.Post(user,"user","register").status);

            Response.Redirect("/Login");

        }
    }
}
