using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class participant
{
    [Key]
    public int participantId { get; set; }

    public int personId { get; set; }

    public int discountCodeId { get; set; }

    public string? userDiscountCode { get; set; }

    public int shirtSizeId { get; set; }

    public string? selectedSlot { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime registrationDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? confirmationDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? reminderDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? cancelationDate { get; set; }

    [ForeignKey("discountCodeId")]
    [InverseProperty("participants")]
    public virtual discountCode discountCode { get; set; } = null!;

    [InverseProperty("participant")]
    public virtual participantsPrivate? participantsPrivate { get; set; }

    [ForeignKey("personId")]
    [InverseProperty("participants")]
    public virtual person person { get; set; } = null!;

    [ForeignKey("shirtSizeId")]
    [InverseProperty("participants")]
    public virtual shirtSize shirtSize { get; set; } = null!;

    [ForeignKey("participantId")]
    [InverseProperty("participants")]
    public virtual ICollection<tag> tags { get; set; } = new List<tag>();
}
