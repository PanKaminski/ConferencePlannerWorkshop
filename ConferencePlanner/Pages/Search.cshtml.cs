using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDTO;
using ConferencePlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConferencePlanner.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IApiClient apiClient;

        public SearchModel(IApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public string Term { get; set; }

        public List<SearchResult> SearchResults { get; set; }

        public async Task OnGetAsync(string term)
        {
            Term = term;
            SearchResults = await this.apiClient.SearchAsync(term);
        }
    }
}
