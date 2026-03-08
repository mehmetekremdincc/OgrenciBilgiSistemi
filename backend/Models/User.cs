namespace OgrenciBilgiSistemiProject.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public Student? Student { get; set; }

        public Teacher? Teacher { get; set; }
    }
}