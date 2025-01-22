using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HotelApp.Webb.Pages
{
    public class RoomSearchModel : PageModel
    {
        private readonly IDatabaseData _db;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        //after MVP add feature, that moves endDate to startDate+1, if selected startDate is set to be after selected endDate
        //on this note - good idea to create a list of features to add after MVP and add features to it as they crop up
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        public List<RoomTypeModel> AvailableRoomTypes { get; set; }

        public RoomSearchModel(IDatabaseData db)
        {
            _db = db;
        }

        public void OnGet()
        {
            if (SearchEnabled == true)
            {
                AvailableRoomTypes = _db.GetAvailableRoomTypes(StartDate, EndDate);
            }
        }
        
        public IActionResult OnPost()
        {
            return RedirectToPage(new 
            { 
                SearchEnabled = true,
                //there is a problem with the conversion from retarded us date format here
                //it goes all to shit when they are sent on to other page because it switches the days/months in string and
                //it will show them correctly in date fields, but captures garbage and defaults to 1.1.0001 in the value
                //the problem seems to trigger on RoomSearch.cshtml on get() when they send that on
                //it does not matter if we convert dates to proper string format (it solves the bug observed in video, but not this one, that manifests right after it)
                StartDate = StartDate.ToString("yyyy-MM-dd"),
                EndDate = EndDate.ToString("yyyy-MM-dd")
            });
        }
    }
}
