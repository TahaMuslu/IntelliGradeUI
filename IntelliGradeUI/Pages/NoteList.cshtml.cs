using IntelliGradeUI.Models;
using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
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
        public string homeworksUrl { get; set; }
        [BindProperty]
        public List<AssignmentResult> grades { get; set; }
        [BindProperty]
        public List<User> teachers { get; set; }


        public void OnGet()
        {
            ToastService.deleteToasts(Response);
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Login");
            }else if(assignmentId == null || classId==null || assignmentId=="" || classId=="" || GetRequests.Get("user","isteacher/"+classId, Request.Cookies["Token"]).message!="true")
            {
                Response.Redirect("/Index");
            }
            else
            {
                try
                {
                homeworksUrl = Request.Cookies["homeworksUrl"].ToString();
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Response.Cookies.Delete("homeworksUrl");

                Response response1 = GetRequests.Get("Assignment", "getnotelist/" + classId+"/"+assignmentId, Request.Cookies["Token"]);
                Response response3 = GetRequests.Get("lesson", "getteachers/" + classId, Request.Cookies["Token"]);
                if (response1.status != "OK" || response3.status!="OK")
                {
                    ToastService.createErrorToast("Ödevler getirilemedi.", Response);
                    Response.Redirect("/Classroom?classId"+classId);
                }
                else
                {
                    grades = JsonConvert.DeserializeObject<List<AssignmentResult>>(response1.message);
                    teachers = JsonConvert.DeserializeObject<List<User>>(response3.message);
                }

            }
        }

        public void OnPostDownloadAllHomeworks()
        {
            Response response = new Response();
            response = GetRequests.Get("AI", "getnotelistfile/" + classId + "/" + assignmentId, Request.Cookies["Token"]);
            homeworksUrl = response.message;
            if(response.status=="OK")
                ToastService.createSuccessToast("Ödevler indirildi", Response);
            else
                ToastService.createErrorToast("Ödevler indirilemedi.", Response);
           
           Response.Cookies.Append("homeworksUrl", homeworksUrl);
           Response.Redirect("/NoteList?assignmentId="+assignmentId+"&classId="+classId);
        }


    }
}
