using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class shirtSize
{
    [Key]
    public int shirtSizeId { get; set; }

    public string name { get; set; } = null!;

    [InverseProperty("shirtSize")]
    public virtual ICollection<participant> participants { get; set; } = new List<participant>();
}
