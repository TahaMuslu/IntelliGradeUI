using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class LoginModernModel : PageModel
    {
        [BindProperty]
        public string nameSurname { get; set; }
        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }

        [BindProperty(SupportsGet = true)]
        public string register { get; set; }


        public void OnGet()
        {
        }

        public void OnPostSignup()
        {
            var user = new User();
            user.id = Guid.NewGuid().ToString();
            user.nameSurname = nameSurname;
            user.email = email;
            user.password = password;
            user.teacherLessons = new List<string>();
            user.studentLessons = new List<string>();

            PostRequests.Post(user, "user", "register");

            Response.Redirect("/Login");

        }

        public void OnPostLogin()
        {
            LoginUser loginUser = new LoginUser();
            loginUser.email = email;
            loginUser.password = password;

            string result = PostRequests.Post(loginUser, "user", "login").message;

            if (result.Equals("{\"statusCode\":404,\"message\":\"Kullanıcı bulunamadı\"}"))
                password = "d-block";
            else
            {
                string temp = GetRequests.Get("user", "getuser", result).Result.message;
                User user = JsonConvert.DeserializeObject<User>(temp);
                Response.Cookies.Append("UserName", user.nameSurname);
                Response.Cookies.Append("Token", result);
                Response.Redirect("/Index");
            }
        }

    }
}
