using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PR.Notifications.Model;
using PR.Notifications.Services;

namespace PR.Patients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController 
        : ControllerBase
    {

        private readonly Model.PrDataContext _context;
        private readonly ServiceBusConsumer _sender;

        public PatientsController(Model.PrDataContext context, ServiceBusConsumer sender)
        {
            _context = context;
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpGet("error")]
        public IActionResult InvalidAction()
        {
            throw new InvalidOperationException("Symulowany problem z aplikacją, po wykonaniu akcji przez użytkownika");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_context.Patients.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(Model.Patients p)
        {
            _context.Patients.Add(p);
            _context.SaveChanges();


            await _sender.SendMessage(new MessagePayload()
            {
                EventName = "NewPatientRegistered",
                EmailAddress = p.Email,
                Title = "COVID-19",
                Message = "Informacja o kwarantannie."
            });
            return Created("Created", p);
        }

    }
}