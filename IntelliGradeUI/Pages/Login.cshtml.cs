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

            if (result.Equals("{\"statusCode\":404,\"message\":\"Kullanýcý bulunamadý\"}"))
                password = "d-block";
            else
            {
                string username = GetRequests.Get("user", "getusername", result).Result.message;
                Response.Cookies.Append("UserName", username);
                Response.Cookies.Append("Token", result);
                Response.Redirect("/Index");
            }

        }

    }
}
