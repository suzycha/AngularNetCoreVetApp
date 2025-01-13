using Microsoft.AspNetCore.Mvc;
using NetVet.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetVet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointments(string date = null, string petName = null)
        {
            var appointments = await _appointmentRepository.GetAppointmentsAsync(false);

            if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParse(date, out var parsedDate))
            {
                appointments = appointments.Where(a => a.AppointmentDateTime.Date == parsedDate.Date);
            }

            if (!string.IsNullOrWhiteSpace(petName))
            {
                appointments = appointments.Where(a =>
                    a.Pet != null && 
                    a.Pet.Name != null && 
                    a.Pet.Name.ToLower().Contains(petName.ToLower()));
            }

            // Transform to DTO
            var appointmentDtos = appointments
                .Select(a => new AppointmentDto
                {
                    AppointmentDateTime = a.AppointmentDateTime,
                    PetName = a.Pet != null ? a.Pet.Name : "Unknown Pet",
                    OwnerName = a.Pet != null && a.Pet.Owner != null
                        ? $"{a.Pet.Owner.FirstName} {a.Pet.Owner.LastName}"
                        : "Unknown Owner",
                    ContactInfo = a.Pet != null && a.Pet.Owner != null &&
                                  a.Pet.Owner.Contacts.FirstOrDefault() != null
                        ? a.Pet.Owner.Contacts.FirstOrDefault().ContactData
                        : "N/A"
                })
                .ToList(); // Ensure projection is done in memory, not in LINQ-to-Entities

            return Ok(appointmentDtos.OrderBy(a => a.AppointmentDateTime));
        }
    }

    public class AppointmentDto
    {
        public DateTime AppointmentDateTime { get; set; }
        public string PetName { get; set; } = "Unknown Pet";
        public string OwnerName { get; set; } = "Unknown Owner";
        public string ContactInfo { get; set; } = "N/A";
    }
}
