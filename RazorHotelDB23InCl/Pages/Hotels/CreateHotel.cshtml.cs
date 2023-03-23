using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Interfaces;

namespace RazorHotelDB23InCl.Pages.Hotels
{
    public class CreateHotelModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }

        private IHotelService hservice;
        public CreateHotelModel(IHotelService hotelService)
        {
            hservice = hotelService;
        }
        
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            try             
            {
                await hservice.CreateHotelAsync(Hotel);
                return RedirectToPage("GetAllHotels");
            }
            catch(Exception ex)
            {

                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }
    }
}
