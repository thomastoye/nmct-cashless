using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.vereniging.ViewModel
{
    class AccountVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Account"; }
        }
    }
}
