using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Ease.Models
{
    public class Event
    {
        [Key]
        public int Event_ID { get; set; }

        [Display(Name = "Event Name")] 
        public string? EventName { get; set; }
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateOnly? EventDate { get; set; }
        [Display(Name = "Event Description")] 
        public string? EventDescription { get; set; }

        public List<Booking>? Bookings { get; set; }
    }
}
