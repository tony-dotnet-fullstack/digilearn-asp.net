using Tony.Web.Infrastructure.Services;
using Tony.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tony.Web.Pages
{
    public class IndexModel : PageModel
    {
        private IHomePageService _service;

        public IndexModel(IHomePageService service)
        {
            _service = service;
        }

        public HomePageViewModel HomePageData { get; set; }
        public async Task OnGet()
        {
            HomePageData = await _service.GetData();
        }
    }
}