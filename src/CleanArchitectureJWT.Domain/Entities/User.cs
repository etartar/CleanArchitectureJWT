using CleanArchitectureJWT.Domain.Common;

namespace CleanArchitectureJWT.Domain.Entities
{
    public class User : AuditableEntity<Guid>
    {
        #region Properties

        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Surname { get; set; }
        public string NormalizedSurname { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        #endregion

        #region Virtuals

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        #endregion

        #region Methods

        public string FullName => $"{Name} || {Surname}";

        public User CreateUser(string name, string surname, string email, bool emailConfirmed, string? phoneNumber, bool phoneNumberConfirmed)
        {
            Name = name;
            NormalizedName = name.ToUpper();
            Surname = surname;
            NormalizedSurname = surname.ToUpper();
            Email = email;
            NormalizedEmail = email.ToUpper();
            EmailConfirmed = emailConfirmed;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            return this;
        }

        #endregion
    }
}
