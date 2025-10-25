using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public int PersonId { get; set; }

    public long? ZipCode { get; set; }

    public string? City { get; set; }

    public string? StreetAndNumber { get; set; }

    public string? CoverName { get; set; }

    public virtual Person Person { get; set; } = null!;
}
