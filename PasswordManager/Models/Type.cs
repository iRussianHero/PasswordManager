namespace PasswordManager.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Type> Types { get; set; }
    }
}
