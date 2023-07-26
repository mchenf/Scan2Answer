using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Scan2Answer.Models;

namespace Scan2Answer.Hubs
{
    public class TeacherSideHub : Hub
    {
        public async Task ReportIn()
        {
            await Clients.All.SendAsync("ReportedIn");
        }
        public async Task Answer(int answerId)
        {
            await Clients.All.SendAsync("AnswerProvided", answerId);
        }
    }
}
