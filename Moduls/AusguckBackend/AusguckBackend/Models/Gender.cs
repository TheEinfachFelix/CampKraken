using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Gender
{
    public int GenderId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
