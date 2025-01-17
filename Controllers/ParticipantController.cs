namespace EcfCdaDotNet.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using global::EcfCdaDotNet.Context;
    using global::EcfCdaDotNet.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace EcfCdaDotNet.Controllers
    {
        public class ParticipantsController : Controller
        {
            private readonly MyDbContext _context;

            public ParticipantsController(MyDbContext context)
            {
                _context = context;
            }

            public IActionResult Create()
            {
                ViewBag.Events = _context.Events.ToList();
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(ParticipantCreateViewModel model)
            {
                if (ModelState.IsValid)
                {
                    // Créer le participant
                    var newParticipant = new Participant
                    {
                        Id = Guid.NewGuid(),
                        Lastname = model.Lastname,
                        Firstname = model.Firstname,
                        Age = model.Age,
                        Email = model.Email,
                        Phone = model.Phone,
                        Gender = model.Gender
                    };

                    _context.Participants.Add(newParticipant);
                    await _context.SaveChangesAsync();

                    var newParticipation = new Participation
                    {
                        IdParticipant = newParticipant.Id,
                        IdEvent = model.EventId,
                        RegistrationDate = DateTime.Now
                    };

                    _context.Participations.Add(newParticipation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Events");
                }

                ViewBag.Events = _context.Events.ToList();
                return View(model);
            }
        }
    }

}
