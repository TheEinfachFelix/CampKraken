using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class schoolType
{
    [Key]
    public int schoolTypeId { get; set; }

    public string name { get; set; } = null!;

    [InverseProperty("schoolType")]
    public virtual ICollection<participantsPrivate> participantsPrivates { get; set; } = new List<participantsPrivate>();
}
