namespace PeopleLibrary;
public class Person
{
    public decimal Salary { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Person>? Children { get; set; }
    private ulong BankAccount { get; set; }

    //  Encrypted Password
    private string? BanKPassword { get; }
}
