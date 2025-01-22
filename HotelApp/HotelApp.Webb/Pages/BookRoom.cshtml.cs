using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Globalization;

namespace HotelApp.Webb.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDatabaseData _db;

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        public RoomTypeModel RoomType { get; set; }

        public BookRoomModel(IDatabaseData db)
        {
            _db = db;
        }

        //we do NOT get proper dates populated here, they get flipped to murican format
        //if day is beyond 12th of month we get 1.1.0001
        //when we send them from roomSearch page, they are correct, here they are not
        //need to parse the time here, so that it can be recognized OnGet()
        //format syntax sample: DateTime.ParseExact(stringToParse, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //changing windows time format to murican is a valid work-around, that will have to be utilized IF this shit can't get sorted
        public void OnGet()
        {
            if (RoomTypeId > 0)
            {
                RoomType = _db.GetRoomTypeById(RoomTypeId);
            }
        }

        public IActionResult OnPost()
        {
            _db.BookGuest(FirstName, LastName, StartDate, EndDate, RoomTypeId);

            return RedirectToPage("/Index");
        }
    }
}
