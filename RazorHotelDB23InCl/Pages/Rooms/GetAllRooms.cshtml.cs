using Microsoft.AspNetCore.Mvc;
using RazorHotelDB23InCl.Models;
using RazorHotelDB23InCl.Services;
using RazorHotelDB23InCl.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorHotelDB23InCl.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public List<Models.Room> Rooms { get; private set; }

        private IRoomService rService;
        public GetAllRoomsModel(IRoomService roomService)
        {
            rService = roomService;
        }
        public async Task OnGetAsync(int hotelnr)
        {           
                Rooms = await rService.GetAllRoomAsync(hotelnr);
            
        }
    }
  
}
