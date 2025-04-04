﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Event_Ease.Data;
using Event_Ease.Models;

namespace Event_Ease.Controllers
{
    public class VenuesController : Controller
    {
        private readonly Event_EaseContext _context;

        public VenuesController(Event_EaseContext context)
        {
            _context = context;
        }

        // GET: Venues
       // GET: Venues
public async Task<IActionResult> Index(string venueLocation, string searchString)
        {
            if (_context.Venue == null)
            {
                return Problem("Entity set 'Event_EaseContext.Venue' is null.");
            }

            IQueryable<string> locationQuery = from v in _context.Venue
                                               orderby v.VenueLocation
                                               select v.VenueLocation;

            var venues = from v in _context.Venue
                         select v;

            if (!string.IsNullOrEmpty(searchString))
            {
                venues = venues.Where(v => v.VenueName!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(venueLocation))
            {
                venues = venues.Where(v => v.VenueLocation == venueLocation);
            }

            var venueLocationVM = new VenueLocationViewModel
            {
                Locations = new SelectList(await locationQuery.Distinct().ToListAsync()), 
                Venues = await venues.ToListAsync(),
                SearchString = searchString, 
                VenueLocation = venueLocation 
            };

            return View(venueLocationVM);
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue
                .FirstOrDefaultAsync(m => m.Venue_ID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Venues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Venue_ID,VenueName,VenueLocation,VenueCapacity,VenueImage")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }


        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }
            return View(venue);
        }

        // POST: Venues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Venue_ID,VenueName,VenueLocation,VenueCapacity,VenueImage")] Venue venue)
        {
            if (id != venue.Venue_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.Venue_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue
                .FirstOrDefaultAsync(m => m.Venue_ID == id);
            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venue.FindAsync(id);
            if (venue != null)
            {
                _context.Venue.Remove(venue);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.Venue_ID == id);
        }
    }
}
