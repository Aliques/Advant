using System;

namespace AdvantTest.Domain.Core.DTO
{
    public class EmployeeForCreationDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public bool HasChildren { get; set; }
    }
}
