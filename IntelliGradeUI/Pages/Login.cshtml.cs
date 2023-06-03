using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }

        [BindProperty]
        public string passwordRegister { get; set; }

        [BindProperty]
        public string nameSurname { get; set; }
        [BindProperty]
        public string emailRegister { get; set; }

        [BindProperty(SupportsGet = true)]
        public string register { get; set; }


        public void OnGet()
        {
            if (register == null)
                register = "false";


            if (Request.Cookies["Token"] != null)
                Response.Redirect("/Index");

            ToastService.deleteToasts(Response);
        }

        public void OnPostRegister()
        {
            Console.WriteLine("Register");
            var user = new User();
            user.id = Guid.NewGuid().ToString();
            user.nameSurname = nameSurname;
            user.email = emailRegister;
            user.password = passwordRegister;
            user.teacherLessons = new List<string>();
            user.studentLessons = new List<string>();

            if(PostRequests.Post(user, "user", "register").status == "Created")
            {
                ToastService.createSuccessToast("Kayýt oldunuz.", Response);
            }
            else
            {
                ToastService.createErrorToast("Kayýt olunamadý.", Response);
            }
            

            Response.Redirect("/Login");

        }

        public void OnPostLogin()
        {
            LoginUser loginUser = new LoginUser();
            loginUser.email = email;
            loginUser.password = password;

            string result = PostRequests.Post(loginUser, "user", "login").message;

            if (result.Equals("{\"statusCode\":404,\"message\":\"Kullanýcý bulunamadý\"}"))
            {
                password = "d-block";
                ToastService.createErrorToast("Kullanýcý bilgileri bulunamadý.", Response);
                Response.Redirect("/Login");
            }
            else
            {
                string user = "";
                user = GetRequests.Get("user", "getuser", result).message;
                string username = JsonConvert.DeserializeObject<User>(user).nameSurname;
                Response.Cookies.Append("UserName", username);
                Response.Cookies.Append("Token", result);
                ToastService.createSuccessToast("Giriþ baþarýlý", Response);
                Response.Redirect("/Index");
            }
        }

    }
}
