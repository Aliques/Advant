using AdvantTest.Client.ViewModels.Base;
using AdvantTest.Infrastructure.Services;
using AdvantTestTask.Grpc;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdvantTest.Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }
        private Employee selectedEmployee;

        public Employee SelectedEmployee { get; set; } = new Employee() {Birthdate=Timestamp.FromDateTime(DateTime.UtcNow) };
        private readonly IEmployeeServices _employeeServices;
        public Employee CurrentFormEmployee { get; set; } = new Employee();
        public MainViewModel(IEmployeeServices employeeServices)
        {
            Task.Run(()=>GetAllEmployee());
            _employeeServices = employeeServices;
            CreateEmployeeCommand = new RelayCommand(async() => await CreateEmployee());
            UpdateEmployeeCommand = new RelayCommand(async() => await UpdateEmployee());
            DeleteEmployeeCommand = new RelayParameterizedCommand(async(object id) => await DeleteEmployee(id));
            SelectedItemChangedCommand = new RelayParameterizedCommand((object item)=> SelectedItemChanged(item));
        }

        private async Task CreateEmployee()
        {
            if(SelectedEmployee is null) { return; }
            if (CheckingFields()) 
            { 
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK);
                return;
            }

            var employee = new EmployeeForCreation
            {
                Name = SelectedEmployee.Name,
                Surname = SelectedEmployee.Surname,
                Birthdate = SelectedEmployee.Birthdate,
                Gender = SelectedEmployee.Gender,
                IsHaveChild = SelectedEmployee.IsHaveChild,
                Patronymic = SelectedEmployee.Patronymic
            };
            
            var validResult = MessageBox.Show(
                string.Format("Будет создан новый пользователь:{6} Имя: {0}{6}" +
                " Фамилия: {1}{6} Отчество: {2}{6} Дата рождения: {3}{6} Дети: {4}{6} Пол: {5}",
                employee.Name,
                employee.Surname,
                employee.Patronymic,
                employee.Birthdate.ToDateTime().ToString("dd.MM.yyyy"),
                employee.IsHaveChild ? "Есть" : "Нет",
                GetGenderLoacale(employee.Gender),
                Environment.NewLine),"Внимание", MessageBoxButton.OKCancel);
            
            if(validResult == MessageBoxResult.Cancel)
            {
                return;
            }

            var result = await _employeeServices.Create(employee);

            if(result is null)
            {
                MessageBox.Show("При создании польователя возникла ошибка!", "Ошибка", MessageBoxButton.OK);
                return;
            }
            Employees.Add(result.Employee);
        }
        private async Task UpdateEmployee()   
        {
            if(SelectedEmployee is null || SelectedEmployee.Id == 0)
            { return; }

            var result = await _employeeServices.Update(SelectedEmployee);

            if (result)
            {
                var empId = Employees.IndexOf(Employees.Where(o => o.Id.Equals(SelectedEmployee.Id)).FirstOrDefault());
                Employees[empId] = SelectedEmployee;
            }
        }

        private async Task DeleteEmployee(object id)
        {
            var result = await _employeeServices.Delete((int)id);

            if (!result)
            {
                MessageBox.Show("При попытке удаления возникла ошибка", "Ошибка", MessageBoxButton.OK);
                return;
            }

            Employees.Remove(Employees.First(o=>o.Id.Equals(id)));
        }

        private void SelectedItemChanged(object item)
        {
            if(item is null) { return; }
            selectedEmployee = item as Employee;
            SelectedEmployee.Name = selectedEmployee.Name;
            SelectedEmployee.Surname = selectedEmployee.Surname;
            SelectedEmployee.Patronymic = selectedEmployee.Patronymic;
            SelectedEmployee.Birthdate = selectedEmployee.Birthdate;
            SelectedEmployee.IsHaveChild = selectedEmployee.IsHaveChild;
            SelectedEmployee.Gender = selectedEmployee.Gender;
            SelectedEmployee.Id = selectedEmployee.Id;
            OnPropertyChanged(nameof(SelectedEmployee));
        }
        private async Task GetAllEmployee()
        {
            Employees = new ObservableCollection<Employee>(await _employeeServices.GetAll());
        }

        private bool CheckingFields()
        {
            return string.IsNullOrEmpty(SelectedEmployee.Name) || string.IsNullOrWhiteSpace(SelectedEmployee.Name) ||
            string.IsNullOrWhiteSpace(SelectedEmployee.Surname) || string.IsNullOrEmpty(SelectedEmployee.Surname) ||
            string.IsNullOrWhiteSpace(SelectedEmployee.Patronymic) || string.IsNullOrEmpty(SelectedEmployee.Patronymic);
        }

        private string GetGenderLoacale(Gender gender)
        {
            switch (gender)
            {
                case Gender.None:
                    return "Не указано";
                case Gender.Male:
                    return "Мужской";
                case Gender.Famale:
                    return "Женский";
                default:
                    return "Не указано";
            }
        }

        public ICommand CreateEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand SelectedItemChangedCommand { get; set; }
    }
}
