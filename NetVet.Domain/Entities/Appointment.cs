using System;
using System.Collections.Generic;

namespace NetVet.Domain.Entities
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime? DateDeleted { get; set; }

        public Pet? Pet { get; set; } = null!;
        public List<Note> Notes { get; set; } = new ();
    }
}
