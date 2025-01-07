using Microsoft.AspNetCore.Identity;

namespace zaliczenie.Models
{
    // Klasa ApplicationUser dziedziczy po IdentityUser
    public class ApplicationUser : IdentityUser
    {
        // Dodaj tutaj własne właściwości, jeśli chcesz, np. imię, nazwisko itd.
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
    }
}
