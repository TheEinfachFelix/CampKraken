using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

[Table("person")]
public partial class person
{
    [Key]
    public int personId { get; set; }

    public string? lastName { get; set; }

    public string? firstName { get; set; }

    public DateOnly? dateOfBirth { get; set; }

    public int? genderId { get; set; }

    [InverseProperty("person")]
    public virtual ICollection<address> addresses { get; set; } = new List<address>();

    [InverseProperty("person")]
    public virtual ICollection<contactInfo> contactInfos { get; set; } = new List<contactInfo>();

    [ForeignKey("genderId")]
    [InverseProperty("people")]
    public virtual gender? gender { get; set; }

    [InverseProperty("person")]
    public virtual ICollection<participant> participants { get; set; } = new List<participant>();

    [InverseProperty("person")]
    public virtual ICollection<staff> staff { get; set; } = new List<staff>();

    [ForeignKey("personId")]
    [InverseProperty("people")]
    public virtual ICollection<day> days { get; set; } = new List<day>();
}
