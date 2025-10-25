using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class SchoolType
{
    public int SchoolTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ParticipantsPrivate> ParticipantsPrivates { get; set; } = new List<ParticipantsPrivate>();
}
