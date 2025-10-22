using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class ParticipantsPrivate
{
    public int ParticipantId { get; set; }

    public int NutritionId { get; set; }

    public int SchoolTypeId { get; set; }

    public string? InsuredBy { get; set; }

    public string? SpecialInfos { get; set; }

    public string? HealthInfo { get; set; }

    public string? Doctor { get; set; }

    public string? Intolerances { get; set; }

    public virtual Nutrition Nutrition { get; set; } = null!;

    public virtual Participant Participant { get; set; } = null!;

    public virtual SchoolType SchoolType { get; set; } = null!;
}
