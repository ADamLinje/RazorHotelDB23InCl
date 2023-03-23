using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23InCl.Interfaces;
using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Services;

namespace RazorHotelDB23InCl.Pages.Rooms
{
    public class CreateRoomModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; }

        private IRoomService rservice;
        public CreateRoomModel(IRoomService roomService)
        {
            rservice = roomService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync(int hotelnr)
        {
            await rservice.CreateRoomAsync(hotelnr, Room);
            return RedirectToPage("GetAllRooms", Room);
        }
    }
}
