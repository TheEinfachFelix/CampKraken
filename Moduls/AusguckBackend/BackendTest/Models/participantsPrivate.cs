using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

[Table("participantsPrivate")]
public partial class participantsPrivate
{
    [Key]
    public int participantId { get; set; }

    public int schoolTypeId { get; set; }

    public string? insuredBy { get; set; }

    public string? specialInfos { get; set; }

    public string? healthInfo { get; set; }

    public string? doctor { get; set; }

    public string? intolerances { get; set; }

    public string? healthInsuranceName { get; set; }

    [ForeignKey("participantId")]
    [InverseProperty("participantsPrivate")]
    public virtual participant participant { get; set; } = null!;

    [ForeignKey("schoolTypeId")]
    [InverseProperty("participantsPrivates")]
    public virtual schoolType schoolType { get; set; } = null!;

    [ForeignKey("participantId")]
    [InverseProperty("participants")]
    public virtual ICollection<nutrition> nutritions { get; set; } = new List<nutrition>();
}
