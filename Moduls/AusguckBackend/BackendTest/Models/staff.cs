using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class staff
{
    [Key]
    public int staffId { get; set; }

    public int personId { get; set; }

    public string? password { get; set; }

    public string? userName { get; set; }

    [ForeignKey("personId")]
    [InverseProperty("staff")]
    public virtual person person { get; set; } = null!;
}
