using System.Text.Json.Serialization;

namespace AusguckBackend.Services
{
    public class Contact
    {
        public string? Number { get; set; }
        public string? Who { get; set; }
    }

    public class Participant
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? StreetAndNumber { get; set; }
        public int? ZipCode { get; set; }
        public string? City { get; set; }
        public string? CoverName { get; set; }
        public List<Contact>? Contacts { get; set; }
        public string? Email { get; set; }
        public string? SchoolType { get; set; }
        public string? ShirtSize { get; set; }
        public bool? HasLiabilityInsurance { get; set; }
        public string? Perms { get; set; }
        public bool? Swimmer { get; set; }

        [JsonPropertyName("selectedSlot")]
        public string? SelectedSlot { get; set; }

        [JsonPropertyName("start-date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end-date")]
        public DateTime? EndDate { get; set; }

        public int? DaysInCamp { get; set; }
        public string? UserDiscountCode { get; set; }
        public List<string>? Nutrition { get; set; }
        public string? Intolerances { get; set; }
        public bool? IsHealthy { get; set; }
        public bool? NeedsMedication { get; set; }
        public string? HealthInfo { get; set; }
        public string? Doctor { get; set; }
        public string? HealthInsuranceName { get; set; }
        public string? InsuredBy { get; set; }
        public bool? PicturesAllowed { get; set; }
        public List<string>? Question1 { get; set; }
        public string? SpecialInfos { get; set; }
    }
}
