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

        public Quiz currentQuiz { get; set; }


        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            ToastService.deleteToasts(Response);

            if (/*quizId != null && classId!=null*/true)
            {
                //Response response = GetRequests.Get("Quiz", "getquizbyid" + "/" + quizId, Request.Cookies["Token"]);
                //if(response.status == "200")
                //{
                //currentQuiz = JsonConvert.DeserializeObject<Quiz>(response.message);
                //}
                //else
                //{
                //Response.Redirect("/Index");
                //}
            }
            else
            {
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
                        Console.WriteLine("\n" + correctAnswers[i]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n" + "No correct answer");
                    }
                    Console.WriteLine("-------------------------------------------------------------------------------");

                    //currentQuiz.Questions.Add(new Question()
                    //{
                    //    questionText = questions[i],
                    //    answers = answers[i],
                    //    correctAnswer = answers[i][int.Parse(correctAnswers[i])]
                    //});

                    //if(PutRequests.Put(currentQuiz,"Quiz", "updatequiz/"+quizId, Request.Cookies["Token"]).status == "OK")
                    //{
                    //    ToastService.createSuccessToast("Quiz oluþturuldu",Response);
                    //}
                    //else
                    //{
                    //    ToastService.createErrorToast("Quiz oluþturulamadý", Response);
                    //}

                }
                Response.Redirect("/Classroom");
            }


        }

    }
}
