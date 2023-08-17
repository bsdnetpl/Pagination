using Bogus;

namespace Pagination.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public DateTime dateTimeCreate { get; set; }

        }
}
