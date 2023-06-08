using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class NoteListQuizModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string quizId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
        [BindProperty]
        public List<User> teachers { get; set; }
        [BindProperty]
        public List<User> students { get; set; }
        [BindProperty]
        public List<QuizResult> quizResults { get; set; }
        [BindProperty]
        public Dictionary<string, string> quizResultDict { get; set; }

        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            else if (quizId == null || classId == null || classId=="" || quizId=="" || GetRequests.Get("user", "isteacher/" + classId, Request.Cookies["Token"]).message != "true")
            {
                Response.Redirect("/Index");
            }
            else
            {
                string result = GetRequests.Get("Quiz", "getquiznotes/" + quizId, Request.Cookies["Token"]).message;
                quizResults = JsonConvert.DeserializeObject<List<QuizResult>>(result);

                string teachers_str = GetRequests.Get("lesson", "getteachers/" + classId, Request.Cookies["Token"]).message;
                teachers = JsonConvert.DeserializeObject<List<User>>(teachers_str);

                string students_str = GetRequests.Get("lesson", "getstudents/" + classId, Request.Cookies["Token"]).message;
                students = JsonConvert.DeserializeObject<List<User>>(students_str);
                quizResultDict = new Dictionary<string, string>();
                quizResults.ForEach(quizResult => quizResultDict.Add(quizResult.studentName, quizResult.note.ToString()));

            }
            ToastService.deleteToasts(Response);
        }
    }
}
