using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? GenderId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();

    public virtual Gender? Gender { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();

    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();

    public virtual ICollection<Day> Days { get; set; } = new List<Day>();
}
