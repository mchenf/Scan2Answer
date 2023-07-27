using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using Scan2Answer.Models;
using SimpleBase;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace Scan2Answer.Pages
{
    public class QuestionModel : PageModel
    {
        public string? SessionID { get; private set; }
        public int Id { get; private set; }
        public Question? Question { get; private set; }
        [BindProperty]
        public string qrCodeImageData { get; set; } = string.Empty;
        public void OnGet([FromRoute]int id, [FromServices]MockQuestionSet questions, [FromServices]QuestionSession session)
        {
            Id = id;
            Question = questions[id];
            Guid guid = Guid.NewGuid();
            SessionID = Base58.Bitcoin.Encode(guid.ToByteArray());
            session.Question = Question;
            session.SessionId = SessionID;

            GetQRCode(string.Join('/', @"https://s2a.azurewebsites.net", "answer", SessionID));
        }

        public void GetQRCode(string absoluteUri)
        {
            using (QRCodeGenerator qrGen = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGen.CreateQrCode(
                absoluteUri, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrcode = new PngByteQRCode(qrCodeData))
            {
                string base64 = Convert.ToBase64String(qrcode.GetGraphic(4));
                qrCodeImageData = "data:image/png;base64," + base64;
            }
        }
    }
}
