using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;

namespace RazorHotelDB23InCl.Pages.Rooms
{
    public class DeleteRoomModel : PageModel
    {
            
        [BindProperty]
        public Room Room { get; set; }

        private IRoomService _rservice;

        public DeleteRoomModel(IRoomService roomservice)
        {
            _rservice = roomservice;
        }

        public async Task OnGetAsync(int roomnr, int hotelnr)
        {

            {
                Room = await _rservice.GetRoomFromIdAsync(roomnr, hotelnr);
            }

        }

        public async Task<IActionResult> OnPostAsync(int roomnr, int hotelnr)
        {
            await _rservice.DeleteRoomAsync(roomnr, hotelnr);
            return RedirectToPage("/Hotels/GetAllHotels");


        }
    }
}

