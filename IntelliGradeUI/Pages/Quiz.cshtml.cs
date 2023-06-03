using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IntelliGradeUI.Pages
{
    public class QuizModel : PageModel
    {
        public void OnGet()
        {
            if (Request.Cookies["Token"] == null)
            {
                Response.Redirect("/Base");
            }
        }
    }
}
