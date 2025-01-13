using System;
using System.Collections.Generic;

namespace NetVet.Domain.Entities
{
    public class Pet
    {
        public Guid PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Breed { get; set; }
        public string? ImageBase64 { get; set; }

        public Owner Owner { get; set; } = null!;
        public Animal Animal { get; set; } = null!;
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
