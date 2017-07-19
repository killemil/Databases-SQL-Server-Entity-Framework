class Person
{
    private string name;
    private int age;
    public Person() : this("No name", 1)
    {
    }
    public Person(int age)
        : this()
    {
        this.Age = age;
    }
    public Person(string name)
        : this()
    {
        this.Name = name;
    }
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public string Name { get; set; }
    public int Age { get; set; }
}
