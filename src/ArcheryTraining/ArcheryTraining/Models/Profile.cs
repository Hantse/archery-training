using MongoDB.Bson;
using Realms;
using System.Collections.Generic;
using System.Linq;

namespace ArcheryTraining.Models
{
    public class Profile : RealmObject
    {
        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

        [MapTo("username")]
        [Required]
        public string Username { get; set; }

        [MapTo("email")]
        [Required]
        public string Email { get; set; }

        [MapTo("userId")]
        [Required]
        public string UserId { get; set; }

        [MapTo("avatarUri")]
        public string AvatarUri { get; set; }

        public IList<Session> Sessions { get; }
    }
}
