using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class ContactInfo
{
    public int ContactInfoId { get; set; }

    public int PersonId { get; set; }

    public int ContactInfoTypeId { get; set; }

    public string Info { get; set; } = null!;

    public string? Details { get; set; }

    public virtual ContactInfoType ContactInfoType { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
