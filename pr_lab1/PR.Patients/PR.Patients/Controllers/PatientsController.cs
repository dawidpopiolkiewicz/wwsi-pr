using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PR.Patients.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly Model.PrDataContext _context;

        public PatientsController(Model.PrDataContext context)
        {
            _context = context;
        }

        public IActionResult GetAll()
        {
            return Ok(_context.Patients.ToList());
        }

        [HttpPost]
        public IActionResult Register(Model.Patients p)
        {
            _context.Patients.Add(p);
            _context.SaveChanges();

            return Created("Created",p);
        }

    }
}