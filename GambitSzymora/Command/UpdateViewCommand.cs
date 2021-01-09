using GambitSzymora.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GambitSzymora.Command
{
    class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "GameHistory")
            {
                viewModel.SelectedViewModel = new HistoryViewModel();
            }
            else if(parameter.ToString() == "Settings")
            {
                viewModel.SelectedViewModel = new SettingsViewModel();
            }
        }
    }
}
