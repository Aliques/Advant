using System;

namespace AdvantTest.Domain.Core
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public bool HasChildren { get; set; }
    }
    public enum Gender
    {
        None,
        Male,
        Female
    }
}
