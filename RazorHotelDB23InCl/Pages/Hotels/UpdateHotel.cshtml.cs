using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Interfaces;

namespace RazorHotelDB23InCl.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
    {
        [BindProperty]
        public Hotel HotelToUpdate { get; set; }

        private IHotelService _hotelService;

        public UpdateHotelModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task OnGetAsync(int hotelnr)
        {
            HotelToUpdate = await _hotelService.GetHotelFromIdAsync(hotelnr);
        }

        public async Task<IActionResult> OnPostAsync(int hotelnr)
        {
            bool ok = await _hotelService.UpdateHotelAsync(HotelToUpdate, hotelnr);
            if (ok)
            {
                return RedirectToPage("GetAllHotels");
            }
            else
                return Page();
        }

    }
}
