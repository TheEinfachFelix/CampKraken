using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class discountCode
{
    [Key]
    public int discountCodeId { get; set; }

    public string name { get; set; } = null!;

    [InverseProperty("discountCode")]
    public virtual ICollection<participant> participants { get; set; } = new List<participant>();
}
