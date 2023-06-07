using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class QuizModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string quizId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
        [BindProperty]
        public Quiz currentQuiz { get; set; }

        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Base");
            }
            else if (quizId == null || quizId == "" || classId==null || classId=="" || isTeacher())
            {
                Response.Redirect("/Index");
            }
            else
            {
                Response response = GetRequests.Get("Quiz", "getbyid/" + quizId, Request.Cookies["Token"]);
                if (response.status != "OK")
                {
                    ToastService.createErrorToast("Quize girilemedi.", Response);
                    Response.Redirect("/Index");
                }
                else
                {
                    currentQuiz = JsonConvert.DeserializeObject<Quiz>(response.message);
                    if (PutRequests.Put("Quiz", "enterquiz/" + quizId, Request.Cookies["Token"]).status != "OK")
                    {
                        ToastService.createErrorToast("Quize zaten giriþ yapmýþsýnýz.", Response);
                        Response.Redirect("/Index");
                    }
                }
            }
        }
        public bool isTeacher()
        {
            string result = GetRequests.Get("user", "isteacher/"+ classId, Request.Cookies["Token"]).message;
            return result == "true";
        }
    }
}
