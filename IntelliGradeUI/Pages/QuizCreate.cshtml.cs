using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class QuizCreateModel : PageModel
    {

        [BindProperty]
        public List<string> questions { get; set; }

        [BindProperty]
        public List<List<String>> answers { get; set; }

        [BindProperty]
        public List<string> correctAnswers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string quizId { get; set; }
        [BindProperty]
        public Quiz currentQuiz { get; set; }


        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            ToastService.deleteToasts(Response);

            if (quizId != null && classId != null)
            {
                Response response = GetRequests.Get("Quiz", "getbyid" + "/" + quizId, Request.Cookies["Token"]);
                if (response.status == "OK")
                {
                    currentQuiz = JsonConvert.DeserializeObject<Quiz>(response.message);
                }
                else
                {
                    Response.Redirect("/Index");
                }
            }
            else
            {
                ToastService.createErrorToast("Sýnýf bulunamadý", Response);
                Response.Redirect("/Index");
            }

        }

        public void OnPostUpdateQuiz()
        {
            if (questions == null || answers == null || correctAnswers == null || questions.Count == 0 || answers.Count == 0 || correctAnswers.Count == 0)
            {
                ToastService.createErrorToast("Quiz oluþturulamadý", Response);
                //Response.Redirect("/QuizCreate?classId=" + classId + "&quizId=" + quizId);
                Response.Redirect("/Index");
            }
            else
            {
                List<Question> questionData = new List<Question>();
                for (int i = 0; i < questions.Count; i++)
                {
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine(questions[i]);
                    for (int j = 0; j < answers[i].Count; j++)
                    {
                        Console.WriteLine(answers[i][j]);
                    }
                    try
                    {
                        Console.WriteLine("\n" + answers[i][int.Parse(correctAnswers[i])]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n" + "No correct answer");
                    }
                    Console.WriteLine("-------------------------------------------------------------------------------");

                    questionData.Add(new Question()
                    {
                        id = Guid.NewGuid().ToString(),
                        questionText = questions[i],
                        answers = answers[i],
                        correctAnswer = answers[i][int.Parse(correctAnswers[i])]
                    });
                }
                if (PutRequests.Put(questionData, "Quiz", "updatequiz/" + quizId, Request.Cookies["Token"]).status == "OK")
                {
                    ToastService.createSuccessToast("Quiz oluþturuldu", Response);
                }
                else
                {
                    ToastService.createErrorToast("Quiz oluþturulamadý", Response);
                }
                Response.Redirect("/Classroom?classId=" + classId);
            }
        }
    }
}
