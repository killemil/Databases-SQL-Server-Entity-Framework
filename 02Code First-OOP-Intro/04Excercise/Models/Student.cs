class Student
{
    public static int studentsCount;

    public Student()
    {
        studentsCount++;
    }

    public string Name { get; set; }
}