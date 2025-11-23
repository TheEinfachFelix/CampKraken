using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

[PrimaryKey("nutritionId", "participantId")]
[Table("nutritionsToPrivate")]
[Index("nutritionId", Name = "nutritionsToPrivate_nutritionId_key", IsUnique = true)]
public partial class nutritionsToPrivate
{
    [Key]
    public int nutritionId { get; set; }

    [Key]
    public int participantId { get; set; }

    [ForeignKey("nutritionId")]
    [InverseProperty("nutritionsToPrivate")]
    public virtual nutrition nutrition { get; set; } = null!;

    [ForeignKey("participantId")]
    [InverseProperty("nutritionsToPrivates")]
    public virtual participantsPrivate participant { get; set; } = null!;
}
