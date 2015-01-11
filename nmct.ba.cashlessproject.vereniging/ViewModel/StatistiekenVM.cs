using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.vereniging.ViewModel
{
    class StatistiekenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Statistieken"; }
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(RefreshStatistieken); }
        }

        private void RefreshStatistieken()
        {
            return;
        }
    }
}
