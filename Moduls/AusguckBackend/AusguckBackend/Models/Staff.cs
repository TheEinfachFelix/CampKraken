using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public int PersonId { get; set; }

    public string? Password { get; set; }

    public string? UserName { get; set; }

    public virtual Person Person { get; set; } = null!;
}
