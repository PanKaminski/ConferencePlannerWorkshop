using System.Threading.Tasks;
using ConferenceDTO;
using ConferencePlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConferencePlanner.Pages
{
    public class SpeakerModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public SpeakerModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public SpeakerResponse Speaker { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Speaker = await _apiClient.GetSpeakerAsync(id);

            if (Speaker == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
