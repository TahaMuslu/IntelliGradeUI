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
            else if (quizId == null || quizId == "")
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
                //else
                //{
                //    List<Question> questions = new List<Question>();
                //    questions.Add(new Question() { id = "1", questionText = "Merhaba1", answers = new List<string> { "1", "2", "3", "4" }, correctAnswer = "2" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba2", answers = new List<string> { "5", "6", "7", "8" }, correctAnswer = "4" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba3", answers = new List<string> { "1", "2", "3", "4" }, correctAnswer = "4" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba4", answers = new List<string> { "5", "6", "7", "8" }, correctAnswer = "4" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba5", answers = new List<string> { "1", "2", "3", "4" }, correctAnswer = "4" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba6", answers = new List<string> { "1", "2", "3", "4" }, correctAnswer = "4" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba7", answers = new List<string> { "5", "6", "7", "8" }, correctAnswer = "4" });
                //    questions.Add(new Question() { id = "2", questionText = "Merhaba8", answers = new List<string> { "1", "2", "3", "4" }, correctAnswer = "4" });


                //    currentQuiz = new Quiz() { id = "1", classId = "fa", questions = questions, questionCount = 2 };
                //}
            }
        }
    }
}
}
