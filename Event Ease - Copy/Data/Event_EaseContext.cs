using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Event_Ease.Models;

namespace Event_Ease.Data
{
    public class Event_EaseContext : DbContext
    {
        public Event_EaseContext (DbContextOptions<Event_EaseContext> options)
            : base(options)
        {
        }

        public DbSet<Event_Ease.Models.Venue> Venue { get; set; } = default!;
        public DbSet<Event_Ease.Models.Event> Event { get; set; } = default!;
        public DbSet<Event_Ease.Models.Booking> Booking { get; set; } = default!;
    }
}
