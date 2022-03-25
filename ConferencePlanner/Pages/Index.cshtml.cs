using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConferenceDTO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConferencePlanner.Services;

namespace ConferencePlanner.Pages
{
    public class IndexModel : PageModel
    {
        protected readonly IApiClient apiClient;
        public bool IsAdmin { get; set; }

        public IndexModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public IEnumerable<IGrouping<DateTimeOffset?, SessionResponse>> Sessions { get; set; }

        public IEnumerable<(int Offset, DayOfWeek? DayofWeek)> DayOffsets { get; set; }

        public int CurrentDayOffset { get; set; }

        public async Task OnGet(int day = 0)
        {
            IsAdmin = this.User.IsAdmin();

            var sessions = await this.apiClient.GetSessionsAsync();

            var startDate = sessions.Min(s => s.StartTime?.Date);

            DayOffsets = sessions.Select(s => s.StartTime?.Date)
                .Distinct()
                .OrderBy(d => d)
                .Select(day => ((int)Math.Floor((day.Value - startDate)?.TotalDays ?? 0),
                    day?.DayOfWeek))
                .ToList();

            var filterDate = startDate?.AddDays(day);

            Sessions = sessions.Where(s => s.StartTime?.Date == filterDate)
                .OrderBy(s => s.TrackId)
                .GroupBy(s => s.StartTime)
                .OrderBy(g => g.Key);
        }
    }
}
