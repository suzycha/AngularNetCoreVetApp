using System.Collections.Generic;
using NetVet.Domain.Entities;

namespace NetVet.Domain
{
    /// <summary>
    /// Mock DbContext for demonstrating Repository and Unit of Work patterns.
    /// </summary>
    public class NetVetDbContext
    {
        public List<Appointment> Appointments { get; set; } = new();
    }
}