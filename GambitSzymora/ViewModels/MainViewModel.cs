using GambitSzymora.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GambitSzymora.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        private BaseViewModel _selectedViewModel;
        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
        
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel;  }
            set 
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }
    }
}
