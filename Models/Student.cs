namespace mvcrud.Models
{
    public class Student
    {
        public int Id { get; set; }                // maps to Students.Id
        public string Name { get; set; } = "";     // maps to Students.Name
        public int Age { get; set; }               // maps to Students.Age
        public string? Grade { get; set; }         // maps to Students.Grade
    }
}
