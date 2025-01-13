using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetVet.Domain.Entities;

namespace NetVet.Domain.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly List<Appointment> _mockAppointments;

        public AppointmentRepository()
        {
            // Reset mock data
            _mockAppointments = GenerateMockAppointments();
        }

        public IQueryable<Appointment> GetAppointments(bool includeDeleted)
        {
            var query = _mockAppointments.AsQueryable();
            if (!includeDeleted)
                query = query.Where(x => !x.DateDeleted.HasValue);

            return query;
        }

        public async Task<IQueryable<Appointment>> GetAppointmentsAsync(bool includeDeleted)
        {
            var query = _mockAppointments.AsQueryable();
            if (!includeDeleted)
                query = query.Where(x => !x.DateDeleted.HasValue);

            return await Task.FromResult(query);
        }

        private List<Appointment> GenerateMockAppointments()
        {
            var random = new Random();
            var mockData = new List<Appointment>();

            for (int i = 1; i <= 10; i++)
            {
                mockData.Add(new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentDateTime = DateTime.Now.AddDays(random.Next(-10, 10)),
                    DateDeleted = i % 3 == 0 ? (DateTime?)DateTime.Now.AddDays(-i) : null,
                    Pet = new Pet
                    {
                        PetId = Guid.NewGuid(),
                        Name = $"Pet{i}",
                        Breed = "Mixed",
                        Owner = new Owner
                        {
                            OwnerId = Guid.NewGuid(),
                            FirstName = "John",
                            LastName = $"Doe{i}"
                        }
                    },
                    Notes = new List<Note>
                    {
                        new Note { NoteId = Guid.NewGuid(), Summary = "Checkup", Detail = "Routine checkup" }
                    }
                });
            }

            return mockData;
        }
    }
}