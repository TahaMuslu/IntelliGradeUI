using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class NoteListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string assignmentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
        [BindProperty]
        public Assignment assignment { get; set; }
        [BindProperty]
        public User teacher { get; set; }
        [BindProperty]
        public List<User> students { get; set; }
        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }else if(assignmentId == null || classId==null || GetRequests.Get("user","isteacher/"+classId, Request.Cookies["Token"]).message!="true")
            {
                Response.Redirect("/Index");
            }
            else
            {
                string result = GetRequests.Get("Assignment", "getbyid/" + assignmentId, Request.Cookies["Token"]).message;
                assignment = JsonConvert.DeserializeObject<Assignment>(result);

                string teacher_str = GetRequests.Get("user", "getbyid/"+assignment.teacherId, Request.Cookies["Token"]).message;
                teacher = JsonConvert.DeserializeObject<User>(teacher_str);

                string tempUsers = GetRequests.Get("user", "getall", Request.Cookies["Token"]).message;
                List<User> allStudents = JsonConvert.DeserializeObject<List<User>>(tempUsers);
                List<string> users_ids = new List<string>();
                foreach(Homework currentHomework in assignment.uploadedHomeworks)
                {
                    users_ids.Add(currentHomework.userId);
                }
                students = new List<User>();
                foreach (User student in allStudents)
                {
                    if (users_ids.Contains(student.id))
                    {
                        students.Add(student);
                    }
                }

            }
        }
    }
}
