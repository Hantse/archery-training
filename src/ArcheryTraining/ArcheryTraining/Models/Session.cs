using MongoDB.Bson;
using Realms;
using Shared.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTraining.Models
{
    public class Session : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        public string Owner { get; set; }

        public int Order { get; set; }

        public DateTimeOffset Date { get; set; }

        public string DateOf
        {
            get
            {
                return Date.Date.ToString("dd/MM/yyyy");
            }

        }

        public string TimeOf
        {
            get
            {
                return Date.ToString("HH:mm");
            }
        }

        public long? Duration { get; set; }

        public long? Distance { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }

        public int? Set { get; set; }

        public int? Total { get; set; }

        public int? TotalArrow { get; set; }

        public string Performance { get; set; }

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

        [Required]
        public IList<String> Guests { get; } = new List<String>();
    }
}
