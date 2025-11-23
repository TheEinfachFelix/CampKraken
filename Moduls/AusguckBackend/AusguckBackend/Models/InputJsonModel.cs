using System.Text.Json.Serialization;

namespace AusguckBackend.Models
{
    public class Contact
    {
        public string? number { get; set; }
        public string? who { get; set; }
    }

    public class InParticipant
    {
        public string? lastName { get; set; }
        public string? firstName { get; set; }
        public DateOnly? dateOfBirth { get; set; }
        public string? gender { get; set; }
        public string? streetAndNumber { get; set; }
        public int? zipCode { get; set; }
        public string? city { get; set; }
        public string? coverName { get; set; }
        public List<Contact>? contacts { get; set; }
        public string? email { get; set; }
        public string? schoolType { get; set; }
        public string? shirtSize { get; set; }
        public bool? hasLiabilityInsurance { get; set; }
        public string? perms { get; set; }
        public bool? swimmer { get; set; }

        [JsonPropertyName("selectedSlot")]
        public string? selectedSlot { get; set; }

        [JsonPropertyName("start-date")]
        public DateOnly? startDate { get; set; }

        [JsonPropertyName("end-date")]
        public DateOnly? endDate { get; set; }

        public int? daysInCamp { get; set; }
        public string? userDiscountCode { get; set; }
        public List<string>? nutrition { get; set; }
        public string? intolerances { get; set; }
        public bool? isHealthy { get; set; }
        public bool? needsMedication { get; set; }
        public string? healthInfo { get; set; }
        public string? doctor { get; set; }
        public string? healthInsuranceName { get; set; }
        public string? insuredBy { get; set; }
        public bool? picturesAllowed { get; set; }
        public List<string>? question1 { get; set; }
        public string? specialInfos { get; set; }
    }
}

