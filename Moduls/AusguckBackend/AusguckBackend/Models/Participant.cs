using System;
using System.Collections.Generic;

namespace AusguckBackend.Models;

public partial class Participant
{
    public int ParticipantId { get; set; }

    public int PersonId { get; set; }

    public int DiscountCodeId { get; set; }

    public string? UserDiscountCode { get; set; }

    public int ShirtSizeId { get; set; }

    public string? SelectedSlot { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime? ConfirmationDate { get; set; }

    public DateTime? ReminderDate { get; set; }

    public DateTime? CancelationDate { get; set; }

    public virtual DiscountCode DiscountCode { get; set; } = null!;

    public virtual ParticipantsPrivate? ParticipantsPrivate { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual ShirtSize ShirtSize { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
