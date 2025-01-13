using System;

namespace NetVet.Domain.Entities
{
    public class Animal
    {
        public Guid AnimalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public AnimalSizes Size { get; set; }
    }
}
