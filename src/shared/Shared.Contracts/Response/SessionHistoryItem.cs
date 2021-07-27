using Shared.Contracts.Enums;
using System;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Response
{
    public class SessionHistoryItem
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("duration")]
        public TimeSpan? Duration { get; set; }

        [JsonPropertyName("type")]
        public SessionType Type { get; set; }

        [JsonPropertyName("status")]
        public SessionStatus Status { get; set; }

        [JsonPropertyName("set")]
        public int? Set { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("total")]
        public int? Total { get; set; }

        [JsonPropertyName("totalArrow")]
        public int? TotalArrow { get; set; }

        [JsonPropertyName("performance")]
        public Performance Performance { get; set; }

        [JsonPropertyName("ratioArrow")]
        public double? RatioArrow
        {
            get
            {
                if (Total != null && TotalArrow != null)
                {
                    return (double)Total / (double)TotalArrow;
                }

                return 0;
            }
        }
    }
}
