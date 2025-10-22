using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class DiscountCode
{
    public int DiscountCodeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
