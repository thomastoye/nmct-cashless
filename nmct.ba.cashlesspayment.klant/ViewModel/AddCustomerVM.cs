using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlesspayment.klant.ViewModel
{
    class AddCustomerVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Nieuwe klant"; }
        }
    }
}
