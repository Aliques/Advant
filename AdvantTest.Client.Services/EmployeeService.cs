using AdvantTest.Infrastructure.Services;
using AdvantTestTask.Grpc;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AdvantTest.Client.Services
{
    public class EmployeeService : IEmployeeServices
    {
        private readonly IConfiguration _configuration;
        private readonly GrpcChannel _channel;
        private readonly Employees.EmployeesClient _client;

        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _channel = CreateGrpcChannel();
            _client = CreateEmployeesClient();
        }

        public async Task<AddEmployeeReply> Create(EmployeeForCreation employee)
        {
            var result = await _client.AddEmployeeAsync(employee);

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _client.DeleteEmployeeAsync(new DeleteEmployeeRequest
             {
                 Id = id,
             });

            return result.Succeed;
        }

        public async Task<RepeatedField<Employee>> GetAll()
        {
            var reply = await _client.GetEmployeesAsync(new Empty());

            return reply.Employees;
        }

        public async Task<bool> Update(Employee employee)
        {
            var reply = await _client.UpdateEmployeeAsync(employee);

            return reply.Succeed;
        }

        private GrpcChannel CreateGrpcChannel()
        {
            return GrpcChannel.ForAddress(_configuration.GetSection("ServerEndpoint").Value);
        }
        private Employees.EmployeesClient CreateEmployeesClient()
        {
            return new Employees.EmployeesClient(_channel);
        }
    }
}
