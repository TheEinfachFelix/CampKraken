using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendTest.Models;

public partial class address
{
    [Key]
    public int addressId { get; set; }

    public int personId { get; set; }

    public long? zipCode { get; set; }

    public string? city { get; set; }

    public string? streetAndNumber { get; set; }

    public string? coverName { get; set; }

    [ForeignKey("personId")]
    [InverseProperty("addresses")]
    public virtual person person { get; set; } = null!;
}
