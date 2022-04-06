using AdvantTestTask.Grpc;
using Google.Protobuf.Collections;
using System.Threading.Tasks;

namespace AdvantTest.Infrastructure.Services
{
    public interface IEmployeeServices
    {
        Task<bool> Update(Employee employee);
        Task<RepeatedField<Employee>> GetAll();
        Task<bool> Delete(int id);
        Task<AddEmployeeReply> Create(EmployeeForCreation employee);
    }
}
