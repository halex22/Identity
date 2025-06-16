using Identity.Model;

namespace Identity.Api.Model
{
    public class Role
    {
        // campi da decidere 
        // user puo avere piu ruoli, ma un ruolo puo avere piu utenti
        public int Id { get; set; }
        public string Type { get; set; } 
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
