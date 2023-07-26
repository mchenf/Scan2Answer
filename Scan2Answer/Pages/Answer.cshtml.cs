using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Scan2Answer.Models;

namespace Scan2Answer.Pages
{
    public class AnswerModel : PageModel
    {
        public QuestionSession? Session { get; set; }
        public void OnGet([FromRoute] string sessionId, [FromServices] QuestionSession session)
        {
            if (sessionId != session.SessionId)
            {
                return;
            }
            Session = session;
        }
    }
}
