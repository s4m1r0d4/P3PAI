namespace PeopleLibrary;

public class Person : IEquatable<Person>
{
    public decimal Salary { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Person>? Children { get; set; }
    public int BankAccount { get; set; }
    public string? BankPassword { get; set; }

    public Person()
    { }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }
        Person other = obj as Person;
        return Equals(other);
    }

    public override int GetHashCode()
    {
        return BankAccount;
    }

    public bool Equals(Person other)
    {
        if (other == null) return false;
        return this.BankAccount.Equals(other.BankAccount);
    }
}
