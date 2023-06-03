using IntelliGradeUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Newtonsoft.Json;

namespace IntelliGradeUI.Pages
{
    public class BaseModel : PageModel
    {
        [BindProperty]
        public BaseInfo baseInfos { get; set; }

        public void OnGet()
        {
      
            baseInfos = JsonConvert.DeserializeObject<BaseInfo>(GetRequests.Get("Info", "getinfos").message);

        }

     
    }
}
