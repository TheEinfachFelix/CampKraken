using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class tag
{
    [Key]
    public int tagId { get; set; }

    public string name { get; set; } = null!;

    [ForeignKey("tagId")]
    [InverseProperty("tags")]
    public virtual ICollection<participant> participants { get; set; } = new List<participant>();
}
