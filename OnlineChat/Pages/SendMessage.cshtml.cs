using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineChat.Pages
{
    public class SendMessageModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; } = string.Empty;

        public void OnPost()
        {
        }
    }
}
