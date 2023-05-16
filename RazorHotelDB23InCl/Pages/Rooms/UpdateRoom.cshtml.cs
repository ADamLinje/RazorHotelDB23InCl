using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;

namespace RazorHotelDB23InCl.Pages.Rooms
{
    public class UpdateRoomModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; }

        private IRoomService _rService;

        public UpdateRoomModel(IRoomService roomService)
        {
            _rService = roomService;
        }
        public async Task OnGetAsync(int roomnr, int hotelnr)
        {
            Room = await _rService.GetRoomFromIdAsync(roomnr, hotelnr);
        }

        public async Task<IActionResult> OnPostAsync(int roomnr, int hotelnr)
        {
            await _rService.UpdateRoomAsync(Room, roomnr, hotelnr);
            return RedirectToPage("/Hotels/GetAllHotels");
            
        }
    }
}
