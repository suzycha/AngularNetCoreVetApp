using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetVet.Domain.Entities;

namespace NetVet.Domain.Repositories
{
    /// <summary>
    /// This class is provided as a proxy Mock of the appointment repository 
    /// for use in your implementation. It will return a set of sample data for
    /// appointments and their related details.
    /// </summary>
    public class MockAppointmentRepository : IAppointmentRepository
    {
        private readonly Random _random = new Random();

        public IQueryable<Appointment> GetAppointments(bool includeDeleted)
        {
            return GenerateSampleAppointments().AsQueryable();
        }

        public async Task<IQueryable<Appointment>> GetAppointmentsAsync(bool includeDeleted)
        {
            var mockData = GenerateSampleAppointments();
            var query = mockData.AsQueryable();

            if (!includeDeleted)
                query = query.Where(x => !x.DateDeleted.HasValue);

            return await Task.FromResult(query);
        }

        private List<Appointment> GenerateSampleAppointments(int count = 10)
        {
            var appointments = new List<Appointment>();
            for (int counter = 1; counter <= count; counter++)
            {
                var owner = GenerateSampleOwner(counter); 
                var pet = owner.Pets[_random.Next(owner.Pets.Count)]; 

                appointments.Add(new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentDateTime = DateTime.Today.AddHours(8 + _random.Next(8)).AddDays(_random.Next(3)),
                    Pet = pet 
                });
            }
            return appointments;
        }

        private Owner GenerateSampleOwner(int id)
        {
            var owner = new Owner
            {
                OwnerId = Guid.NewGuid(),
                FirstName = $"First{id}",
                LastName = $"Last{id}",
                IsOptInForNotifications = true,
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        ContactId = Guid.NewGuid(),
                        ContactType = ContactTypes.Mobile,
                        ContactData = $"040{id:000000}",
                        IsPreferred = true
                    },
                    new Contact
                    {
                        ContactId = Guid.NewGuid(),
                        ContactType = ContactTypes.EMail,
                        ContactData = $"user{id}@example.com"
                    }
                }
            };

            owner.Pets = GenerateSamplePets(owner);
            return owner;
        }

        private List<Pet> GenerateSamplePets(Owner owner)
        {
            var animals = new List<Animal>
            {
                new Animal { AnimalId = Guid.NewGuid(), Name = "Dog", Size = AnimalSizes.Medium },
                new Animal { AnimalId = Guid.NewGuid(), Name = "Cat", Size = AnimalSizes.Small },
                new Animal { AnimalId = Guid.NewGuid(), Name = "Bird", Size = AnimalSizes.Tiny }
            };

            var pets = new List<Pet>();
            for (int count = 1; count <= _random.Next(1, 4); count++)
            {
                pets.Add(new Pet
                {
                    PetId = Guid.NewGuid(),
                    Name = $"Pet{count}",
                    Age = _random.Next(1, 15),
                    Animal = animals[_random.Next(animals.Count)],
                    Breed = "Unknown",
                    Owner = owner 
                });
            }
            return pets;
        }

        private List<Note> GenerateSampleNotes()
        {
            var notes = new List<Note>();
            var noteCount = _random.Next(1, 5); // 랜덤한 Note 개수 (1~4개)

            for (int i = 0; i < noteCount; i++)
            {
                notes.Add(new Note
                {
                    NoteId = Guid.NewGuid(),
                    DateCreated = DateTime.Now.AddDays(-_random.Next(30)), // 지난 30일 내 생성
                    DateModified = DateTime.Now,
                    Summary = $"Sample Note {i + 1}",
                    Detail = $"This is a detail for note {i + 1}."
                });
            }

            return notes;
        }
    }
}
