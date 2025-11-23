using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

[Table("contactInfo")]
public partial class contactInfo
{
    [Key]
    public int contactInfoId { get; set; }

    public int personId { get; set; }

    public int contactInfoTypeId { get; set; }

    public string info { get; set; } = null!;

    public string? details { get; set; }

    [ForeignKey("contactInfoTypeId")]
    [InverseProperty("contactInfos")]
    public virtual contactInfoType contactInfoType { get; set; } = null!;

    [ForeignKey("personId")]
    [InverseProperty("contactInfos")]
    public virtual person person { get; set; } = null!;
}
