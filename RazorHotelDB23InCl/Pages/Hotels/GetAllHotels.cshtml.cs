using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Services;

namespace RazorHotelDB23InCl.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Models.Hotel> Hotels { get; private set; }

        private IHotelService hService;
        public GetAllHotelsModel(IHotelService hotelService)
        {
            hService = hotelService;
        }
        public async Task OnGetAsync()
        {

            if (!String.IsNullOrEmpty(FilterCriteria))
            {
                Hotels =await hService.GetHotelsByNameAsync(FilterCriteria);
            }
            else
            {
                Hotels = await hService.GetAllHotelAsync();
            }
           
        }

    }
}
