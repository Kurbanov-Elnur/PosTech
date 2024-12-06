namespace PosTech.Data.Models;

class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; } = Role.Kassir;
}