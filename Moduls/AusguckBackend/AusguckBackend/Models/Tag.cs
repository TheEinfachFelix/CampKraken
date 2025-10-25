using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
