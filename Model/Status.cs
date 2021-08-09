using System.Text.Json.Serialization;

namespace BankingServices.Model
{
    /// <summary>
    /// Represents status.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        /// <summary>
        /// Created status.
        /// </summary>
        Created = 0,
    }
}
