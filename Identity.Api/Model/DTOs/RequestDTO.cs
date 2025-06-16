using Identity.Model;

namespace Identity.Api.Model.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public bool? Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int UserId { get; set; }

    }
}
