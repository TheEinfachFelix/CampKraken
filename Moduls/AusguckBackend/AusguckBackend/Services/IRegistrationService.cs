using System.Text.Json;

namespace AusguckBackend.Services
{
    public interface IRegistrationService
    {
        Task ProcessIncomingDataAsync(JsonElement data);
    }
}
