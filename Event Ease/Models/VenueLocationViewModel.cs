using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Event_Ease.Models
{
    public class VenueLocationViewModel
    {
        public List<Venue>? Venues { get; set; } // List of filtered venues
        public SelectList? Locations { get; set; } // List of distinct venue locations for filtering
        public string? VenueLocation { get; set; } // The selected venue location filter
        public string? SearchString { get; set; } // The search string for filtering by venue name
    }
}
