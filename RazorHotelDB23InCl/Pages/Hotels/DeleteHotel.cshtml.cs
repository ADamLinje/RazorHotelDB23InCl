using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Services;

namespace RazorHotelDB23InCl.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        [BindProperty]
        public Hotel HotelToDelete { get; set; }

        private IHotelService _hservice;

        public DeleteHotelModel(IHotelService hotelservice)
        {
            _hservice = hotelservice;
        }

        public async Task OnGetAsync(int hotelnr)
        {
            
            {
                HotelToDelete = await _hservice.GetHotelFromIdAsync(hotelnr);
            }

        }

        public async Task<IActionResult> OnPostAsync(int hotelnr)
        {
            await _hservice.DeleteHotelAsync(hotelnr);
            return RedirectToPage("GetAllHotels");
            
            
        }
    }
}
