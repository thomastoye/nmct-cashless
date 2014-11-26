using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.tempui.ViewModel
{
    public class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
        Pages.Add(new PartOneVM());
        Pages.Add(new PartTwoVM());
        Pages.Add(new PartThreeVM());
        CurrentPage = Pages[0];
        }
        private IPage currentPage;
        public IPage CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }
        private List<IPage> pages;
        public List<IPage> Pages
        {
            get
            {
                if (pages == null)
                    pages = new List<IPage>();
                return pages;
            }
        }
        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }
        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }

}
