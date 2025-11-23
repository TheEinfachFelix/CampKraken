using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class contactInfoType
{
    [Key]
    public int contactInfoTypeId { get; set; }

    public string name { get; set; } = null!;

    [InverseProperty("contactInfoType")]
    public virtual ICollection<contactInfo> contactInfos { get; set; } = new List<contactInfo>();
}
