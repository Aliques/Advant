using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AdvantTest.Client.Commands
{
    class CreateEmployeeCommand : ICommand
    {
        private CreateEmployeeWindow _employeeWindow;
        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _employeeWindow == null;
        }

        public void Execute(object parameter)
        {
            var window = new CreateEmployeeWindow
            {
                Owner = Application.Current.MainWindow
            };
            _employeeWindow = window;

            window.Closed += OnWindowClosed;

            window.ShowDialog();
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= OnWindowClosed;
            _employeeWindow = null;
        }
    }
}
