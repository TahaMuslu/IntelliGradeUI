using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class StudentsModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string classId { get; set; }
 
        [BindProperty]
        public List<User> classTeachers { get; set; }

        [BindProperty]
        public List<User> classStudents { get; set; }

        public void OnGet()
        {
          

            string strTeachers = GetRequests.Get("lesson", "getteachers/" + classId, Request.Cookies["Token"]).Result.message;
            classTeachers = JsonConvert.DeserializeObject<List<User>>(strTeachers);



            string strStudents = GetRequests.Get("lesson", "getstudents/" + classId, Request.Cookies["Token"]).Result.message;
            classStudents = JsonConvert.DeserializeObject<List<User>>(strStudents);

        }
    }
}
