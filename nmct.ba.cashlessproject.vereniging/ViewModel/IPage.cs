using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.vereniging.ViewModel
{
    interface IPage
    {
        string Name { get; }
        ICommand RefreshCommand { get; }
    }
}
