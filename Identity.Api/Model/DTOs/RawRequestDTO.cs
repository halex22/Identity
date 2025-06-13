namespace Identity.Api.Model.DTOs
{
    public class RawRequestDTO
    {
        public DateTime? ExecutedAt { get; set; }
        public bool? Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
