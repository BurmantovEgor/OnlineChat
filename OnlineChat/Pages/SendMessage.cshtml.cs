using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineChat.Pages
{
    public class SendMessageModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Message))
            {
                ModelState.AddModelError(string.Empty, "Message cannot be empty.");
                return Page();
            }


            return RedirectToPage("ReceiveMessage"); 
        }
    }
}
