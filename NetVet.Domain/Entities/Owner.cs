using System;
using System.Collections.Generic;

namespace NetVet.Domain.Entities
{
    public class Owner
    {
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PreferredName { get; set; }
        public bool IsOptInForNotifications { get; set; }


        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Pet> Pets { get; set; } = new List<Pet>();
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
