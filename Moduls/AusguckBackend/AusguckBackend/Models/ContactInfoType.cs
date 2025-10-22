using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class ContactInfoType
{
    public int ContactInfoTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
}
