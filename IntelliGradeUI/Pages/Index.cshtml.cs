using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Reflection;
using System.Text;

namespace IntelliGradeUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string className { get; set; }

        [BindProperty]
        public string classCode { get; set; }

        [BindProperty]
        public Response result { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            
            result = await GetRequests.Get("user", "getclasses", Request.Cookies["Token"]);

            return Page();
        }

        public void OnPostCreateClass()
        {
            var model = new Lesson();
            model.id = Guid.NewGuid().ToString();
            model.className = className;
            model.createdBy = "";
            model.createdDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            model.classCode = "";
            model.teacherIds = new List<string>();
            model.studentIds = new List<string>();
            model.assignmentIds = new List<string>();
            Console.WriteLine(model.id);


            Console.WriteLine(PostRequests.Post(model, "lesson", "create", Request.Cookies["Token"]));
            Response.Redirect("/Index");
        }


        public void OnPostJoinClass()
        {
            Console.WriteLine(PutRequests.Put("lesson", "joinclass/" + classCode.Trim(), Request.Cookies["Token"]));

            Response.Redirect("/Index");

        }


        public void OnPostDeleteClass(string id)
        {

            DeleteRequests.Delete("lesson", "delete/" + id, AdminToken.HaciAbiToken);

            Response.Cookies.Append("Token", Request.Cookies["Token"]);
            Response.Redirect("/Index");
        }


        






    }
}