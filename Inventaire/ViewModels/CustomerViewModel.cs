﻿using BillingManagement.Business;
using BillingManagement.Models;
using BillingManagement.UI.ViewModels.Commands;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace BillingManagement.UI.ViewModels
{
    public class CustomerViewModel : BaseViewModel
    {
        private BillingManagementContext db;

        private ObservableCollection<Customer> customers;
        private Customer selectedCustomer;

        public ObservableCollection<Customer> Customers
        {
            get => customers;
            private set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Customer> DeleteCustomerCommand { get; private set; }


        public CustomerViewModel()
        {
            DeleteCustomerCommand = new RelayCommand<Customer>(DeleteCustomer, CanDeleteCustomer);

            db = new BillingManagementContext();

            InitValues();
        }

        private void InitValues()
        {
            Customers = new ObservableCollection<Customer>(db.Customers.ToList().OrderBy(c => c.LastName));
            Debug.WriteLine(Customers.Count);
        }

        private void DeleteCustomer(Customer c)
        {
            var currentIndex = Customers.IndexOf(c);

            if (currentIndex > 0) currentIndex--;

            SelectedCustomer = Customers[currentIndex];

            Customers.Remove(c);
        }

        private bool CanDeleteCustomer(Customer c)
        {
            if (c == null) return false;

            
            return c.Invoices.Count == 0;
        }

        public void SetCustomers(ObservableCollection<Customer> customers)
        {
            Customers = customers;
        }
    }
}
