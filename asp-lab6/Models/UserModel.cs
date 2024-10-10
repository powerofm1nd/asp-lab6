namespace asp_lab6.Models;

public class UserModel
{
    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public UserModel(string firstName, string lastName, int age)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentNullException(nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentNullException(nameof(lastName));
        }

        if (age <= 0 || age > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(age));
        }
        
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}