using Microsoft.AspNetCore.Identity;

namespace CMR.Domain.DataClass
{
    public class Users : IdentityUser
    {
        public string UserId { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string GovId { get; set; } = string.Empty;

        public DateTime? CreateOnDate { get; set; }
        public string CreateBy { get; set; } = string.Empty;

        public DateTime? ModifyOnDate { get; set; }
        public string ModifyBy { get; set; } = string.Empty;
    }
}