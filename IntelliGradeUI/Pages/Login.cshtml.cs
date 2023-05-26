using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntelliGradeUI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            LoginUser loginUser = new LoginUser();
            loginUser.email = email;
            loginUser.password = password;

            string result = PostRequests.Post(loginUser, "user", "login").message;

            Console.WriteLine(result);
            if (result.Equals("{\"statusCode\":404,\"message\":\"Kullan�c� bulunamad�\"}"))
                Console.WriteLine("Kullan�c� bulunamad�");
            else
            {
                Response.Cookies.Append("", result);
                Response.Redirect("/Index");
            }

        }

    }
}
