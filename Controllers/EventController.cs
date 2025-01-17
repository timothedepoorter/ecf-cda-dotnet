namespace EcfCdaDotNet.Controllers
{
    using System;
    using System.Threading.Tasks;
    using global::EcfCdaDotNet.Context;
    using global::EcfCdaDotNet.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EventsController : Controller
    {
        private readonly MyDbContext _context;

        public EventsController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newEvent = new Event
                {
                    Id = Guid.NewGuid(),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CreationDate = DateTime.Now,
                    Location = model.Location,
                    Title = model.Title,
                    Description = model.Description
                };

                _context.Add(newEvent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);  
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var eventToDelete = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (eventToDelete == null)
            {
                return NotFound();
            }
            return View(eventToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var eventToDelete = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var eventDetails = await _context.Events
                .Include(e => e.Participations)
                .ThenInclude(p => p.IdParticipantNavigation)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var eventToEdit = await _context.Events.FindAsync(id);
            if (eventToEdit == null)
            {
                return NotFound();
            }

            return View(eventToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Event updatedEvent)
        {
            if (id != updatedEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(updatedEvent.Id))
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
            return View(updatedEvent);
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }

}
