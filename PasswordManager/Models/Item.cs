namespace PasswordManager.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}
