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

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public void OnPostCreateClass()
        {
            var model = new Lesson();
            model.id = Guid.NewGuid().ToString();
            model.className = className;
            model.createdBy = "";
            model.createdDate = "";
            model.classCode = "";
            model.teacherIds = new List<string>();
            model.studentIds = new List<string>();
            model.assignmentIds = new List<string>();

        }

    }
}