
using System.Collections.Generic;
using System.Linq;

public class Family
{
    private List<Person> members;

    public Family()
    {
        this.Members = new List<Person>();
    }

    public List<Person> Members { get; set; }

    public void AddMember(Person member)
    {
        this.Members.Add(member);
    }

    public Person GetOldestMember()
    {
        return this.Members.OrderByDescending(m => m.Age).FirstOrDefault();
    }
}
