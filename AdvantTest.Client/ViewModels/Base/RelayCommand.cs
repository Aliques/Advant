using System;
using System.Windows.Input;

namespace AdvantTest.Client.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        private Action mAction;
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #region Constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
