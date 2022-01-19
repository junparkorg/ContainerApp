using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContainerApp.Pages
{
    public class APITestModel : PageModel
    {
        public string Message { get; set; }


        public void OnGet()
        {
            Message = "Hello!!";
        }
    }
}
