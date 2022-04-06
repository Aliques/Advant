using AdvantTest.Domain.Interfaces;
using AdvantTest.Grpc;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvantTest
{
    public class EmployeeService : AdvantTest.Grpc.Employees.EmployeesBase
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
            var createdEmployee = _employeeRepository.Create(_mapper.Map<EmployeeForCreation, Domain.Core.Employee>(request));
            await _employeeRepository.SaveChangesAsync();
            var savedEmployee = await _employeeRepository.GetById(createdEmployee.Id);

            return await Task.FromResult(new AddEmployeeReply
            {
                Succeed = savedEmployee != null,
            });
        }
    }
}
