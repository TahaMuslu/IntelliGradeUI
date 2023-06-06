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
        [BindProperty]
        public Quiz currentQuiz { get; set; }

        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Base");
            }
            else if (quizId == null || quizId=="")
            {
                Response.Redirect("/Index");
            }
            else
            {
                Response response = GetRequests.Get("Quiz","getbyid/"+quizId, Request.Cookies["Token"]);
                if (response.status != "OK")
                {
                    ToastService.createErrorToast("Quize girilemedi", Response);
                }else
                {
                    currentQuiz = JsonConvert.DeserializeObject<Quiz>(response.message);
                }
            }
        }
    }
}
