using System;

namespace NetVet.Domain.Entities
{
    public class Note
    {
        public Guid NoteId { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
