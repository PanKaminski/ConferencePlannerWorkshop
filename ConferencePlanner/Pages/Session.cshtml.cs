using System;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDTO;
using ConferencePlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConferencePlanner.Pages
{
    public class SessionModel : PageModel
    {
        private readonly IApiClient apiClient;

        public SessionModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public SessionResponse Session { get; set; }

        public int? DayOffset { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            Session = await this.apiClient.GetSessionAsync(id);

            if (Session == null)
            {
                return RedirectToPage("/Index");
            }

            var allSessions = await this.apiClient.GetSessionsAsync();

            var startDate = allSessions.Min(s => s.StartTime?.Date);

            DayOffset = Session.StartTime?.Subtract(startDate ?? DateTimeOffset.MinValue).Days;

            return Page();
        }
    }
}
