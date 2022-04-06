using AdvantTest.Client.ViewModels.Base;
using AdvantTest.Infrastructure.Services;
using AdvantTestTask.Grpc;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdvantTest.Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();
        private readonly IEmployeeServices _employeeServices;
        public Employee CurrentFormEmployee { get; set; } = new Employee();
        public MainViewModel(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
            CreateEmployeeCommand = new RelayCommand(() => CreateEmployee());
            UpdateEmployeeCommand = new RelayCommand(() => UpdateEmployee());
            DeleteEmployeeCommand = new RelayParameterizedCommand((object id) => DeleteEmployee(id));
        }

        private void CreateEmployee()
        {
            var a = CurrentFormEmployee.IsHaveChild;
        }
        private void UpdateEmployee()
        {

        }
        private void DeleteEmployee(object id)
        {

        }

        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
    }
}
