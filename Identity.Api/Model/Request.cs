using Identity.Model;

namespace Identity.Api.Model
{
    public class Request
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public bool? Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}
