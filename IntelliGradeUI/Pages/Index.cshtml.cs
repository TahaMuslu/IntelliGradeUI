using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using IntelliGradeUI.Services;
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
        public List<Lesson> result { get; set; }


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }
            result = JsonConvert.DeserializeObject<List<Lesson>>(GetRequests.Get("user", "getclasses", Request.Cookies["Token"]).message);
            ToastService.deleteToasts(Response);
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

            if (PostRequests.Post(model, "lesson", "create", Request.Cookies["Token"]).status == "Created")
                ToastService.createSuccessToast("Sınıf başarıyla oluşturuldu.", Response);
            else
                ToastService.createErrorToast("Sınıf oluşturulamadı.", Response);


            Response.Redirect("/Index");
        }


        public void OnPostJoinClass()
        {
            if(PutRequests.Put("lesson", "joinclass/" + classCode.Trim(), Request.Cookies["Token"]).status=="OK")
                ToastService.createSuccessToast("Sınıfa katıldınız.", Response);
            else
                ToastService.createErrorToast("Sınıfa katılınamadı.", Response);

            Response.Redirect("/Index");
        }


        public void OnPostDeleteClass(string id)
        {
            if (DeleteRequests.Delete("lesson", "delete/" + id, Request.Cookies["Token"]).status == "OK")
                ToastService.createSuccessToast("Sınıf başarıyla silindi.", Response);
            else
                ToastService.createErrorToast("Sınıf silinemedi", Response);

            Response.Redirect("/Index");
        }







    }
}