using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Day
{
    public int DayId { get; set; }

    public DateOnly Date { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
