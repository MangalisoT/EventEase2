using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Event_Ease.Data; 
using Event_Ease.Models; 
using System;
using System.Linq;

namespace Event_Ease.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Event_EaseContext(
                serviceProvider.GetRequiredService<DbContextOptions<Event_EaseContext>>()))
            {
                // Check if any venues exist, if not, seed them
                if (!context.Venue.Any())
                {
                    context.Venue.AddRange(
                        new Venue
                        {
                            VenueName = "The Grand Hall",
                            VenueLocation = "Durban",
                            VenueCapacity = 500,
                            VenueImage = "grandhall.jpg"
                        },
                        new Venue
                        {
                            VenueName = "Botanical Gardens",
                            VenueLocation = "Pretoria",
                            VenueCapacity = 1000,
                            VenueImage = ".jpg"
                        },
                        new Venue
                        {
                            VenueName = "LaLiga Arena",
                            VenueLocation = "Portugal",
                            VenueCapacity = 300,
                            VenueImage = "LaLiga Arena.jpg"
                        }
                    );
                    context.SaveChanges();
                }

                // Check if any events exist, if not, seed them
                if (!context.Event.Any())
                {
                    var venue1 = context.Venue.First(v => v.VenueName == "The Grand Hall");
                    var venue2 = context.Venue.First(v => v.VenueName == "Botanical Gardens");
                    var venue3 = context.Venue.First(v => v.VenueName == "LaLiga Arena");

                    context.Event.AddRange(
                        new Event
                        {
                            EventName = "House Revival",
                            EventDate = DateOnly.Parse("2025-06-15"),
                            EventDescription = "Talking to bruts and suits on how they will fall.",
                        },
                        new Event
                        {
                            EventName = "Wedding",
                            EventDate = DateOnly.Parse("2025-07-20"),
                            EventDescription = "A vow of life long commitment.",
                        },
                        new Event
                        {
                            EventName = "Tyler the Creator Concert",
                            EventDate = DateOnly.Parse("2025-08-10"),
                            EventDescription = "Chromakopia."
                        }
                    );
                    context.SaveChanges();
                }

                // Check if any bookings exist, if not, seed them
                if (!context.Booking.Any())
                {
                    var event1 = context.Event.First(e => e.EventName == "House Revival");
                    var event2 = context.Event.First(e => e.EventName == "Wedding");
                    var event3 = context.Event.First(e => e.EventName == "Tyler the Creator Concert");

                    var venue1 = context.Venue.First(v => v.VenueName == "The Grand Hall");
                    var venue2 = context.Venue.First(v => v.VenueName == "Botanical Gardens");
                    var venue3 = context.Venue.First(v => v.VenueName == "LaLiga Arena");

                    context.Booking.AddRange(
                        new Booking
                        {
                            BookingDate = DateOnly.Parse("2025-05-10"),
                            Event_ID = event1.Event_ID,
                            Venue_ID = venue1.Venue_ID
                        },
                        new Booking
                        {
                            BookingDate = DateOnly.Parse("2025-06-01"),
                            Event_ID = event2.Event_ID,
                            Venue_ID = venue2.Venue_ID
                        },
                        new Booking
                        {
                            BookingDate = DateOnly.Parse("2025-07-01"),
                            Event_ID = event3.Event_ID,
                            Venue_ID = venue3.Venue_ID
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
