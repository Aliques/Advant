using AdvantTest.Domain.Interfaces;
using AdvantTestTask.Grpc;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvantTest
{
    public class EmployeeService : Employees.EmployeesBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public override async Task<GetEmployeeReply> GetEmployees(Empty request, ServerCallContext context)
        {
            var employeesList = await _employeeRepository.GetAll();
            var employees = _mapper.Map<List<Domain.Core.Employee>, List<Employee>>(employeesList.ToList());

            return await Task.FromResult(new GetEmployeeReply
            {
                Employees = { employees }
            });
        }

        public override async Task<AddEmployeeReply> AddEmployee(EmployeeForCreation request, ServerCallContext context)
        {
            var creationEmployee = _employeeRepository.Create(_mapper.Map<EmployeeForCreation, Domain.Core.Employee>(request));
            await _employeeRepository.SaveChangesAsync();
            var savedEmployee = await _employeeRepository.GetById(creationEmployee.Id);

            if(savedEmployee is null)
            {
                return await Task.FromResult(new AddEmployeeReply
                {
                    Succeed = false
                });
            }

            var createdEmp = _mapper.Map<Domain.Core.Employee,Employee>(savedEmployee);

            return await Task.FromResult(new AddEmployeeReply
            {
                Employee = createdEmp,
                Succeed = true
            });
        }
        public override async Task<DeleteEmployeeReply> DeleteEmployee(DeleteEmployeeRequest request, ServerCallContext context)
        {
            var target = await _employeeRepository.GetById(request.Id);

            if (target is null)
            {
                return await Task.FromResult(new DeleteEmployeeReply
                {
                    Succeed = false,
                });
            }

            _employeeRepository.Delete(target);
            await _employeeRepository.SaveChangesAsync();

            return await Task.FromResult(new DeleteEmployeeReply
            {
                Succeed = true,
            });
        }

        public async override Task<UpdateEmployeeReply> UpdateEmployee(Employee request, ServerCallContext context)
        {
            var employee = await _employeeRepository.GetById(request.Id);
            employee.FirstName = request.Name;
            employee.Surname = request.Surname;
            employee.Patronymic = request.Patronymic;
            employee.Gender = (Domain.Core.Gender)((int)request.Gender);
            employee.HasChildren = request.IsHaveChild;
            employee.BirthDate = request.Birthdate.ToDateTime();

            await _employeeRepository.SaveChangesAsync();

            return await Task.FromResult(new UpdateEmployeeReply
            {
                Succeed = true,
            });
        }
    }
}
