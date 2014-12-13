using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.vereniging.ViewModel
{
    class MedewerkersVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Medewerkers"; }
        }

        public MedewerkersVM()
        {
            GetEmployees();
        }
        private async void GetEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
                client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employee");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
            }
        }
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("Employees"); }
        }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("SelectedEmployee"); }
        }
        public ICommand AddEmployeeCommand
        {
            get { return new RelayCommand(AddEmployee); }
        }
        public ICommand DeleteEmployeeCommand
        {
            get { return new RelayCommand(DeleteEmployee); }
        }
        public ICommand SaveEmployeeCommand
        {
            get { return new RelayCommand(SaveEmployee); }
        }
        public ICommand RefreshEmployeesCommand
        {
            get { return new RelayCommand(GetEmployees); }
        }
        public async void AddEmployee()
        {
            Employee newEmployee = new Employee();
            using (HttpClient client = new HttpClient())
            {
                string employee = JsonConvert.SerializeObject(newEmployee);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employee", new StringContent(employee, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employee emp = JsonConvert.DeserializeObject<Employee>(json);
                    if (emp != null)
                    {
                        Employees.Add(emp);
                        SelectedEmployee = emp;
                    }

                }
            }
        }
        public async void DeleteEmployee()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employee/" + SelectedEmployee.ID);
                if (response.IsSuccessStatusCode)
                {
                    Employees.Remove(SelectedEmployee);
                }
            }
        }
        public async void SaveEmployee()
        {
            using (HttpClient client = new HttpClient())
            {
                string Employee = JsonConvert.SerializeObject(SelectedEmployee);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employee/" + SelectedEmployee.ID, new StringContent(Employee, Encoding.UTF8, "application/json"));
            }
        }
    }
}
