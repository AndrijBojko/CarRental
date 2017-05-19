using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Entities;

namespace CarRental.DAL.Repositories.Abstract
{
    public interface ICustomerRepository
    {
        void AddCustomer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress);
        void AddCustomer(Customer newCustomer);
        void UpdateCustomerById(int id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress);
        void UpdateCustomer(Customer cutomerToUpdate);
        void DeleteCustomerById(int id);
        void DeleteCustomer(Customer customerToDelete);
        Customer GetCustomerById(int customerId );
        List<Customer> GetAllCustomers();
    }
}
