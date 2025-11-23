using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class gender
{
    [Key]
    public int genderId { get; set; }

    public string name { get; set; } = null!;

    [InverseProperty("gender")]
    public virtual ICollection<person> people { get; set; } = new List<person>();
}
