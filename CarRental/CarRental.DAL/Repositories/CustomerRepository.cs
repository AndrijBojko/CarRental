using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Context;
using CarRental.DAL.Repositories.Abstract;
using CarRental.Entities;

namespace CarRental.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalContext _context;


        public CustomerRepository(CarRentalContext context)
        {
            _context = context;
        }



        public void AddCustomer(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress)
        {
                _context.Customers.Add(new Customer()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    PhoneNumber = phoneNumber,
                    Adress = adress
                });

                _context.SaveChanges();     
        }

        public void AddCustomer(Customer newCustomer)
        {
            if (newCustomer == null)
            {
                throw new ArgumentException("Trying to add null reference customer entity");
            }
                _context.Customers.Add(newCustomer);
                _context.SaveChanges();
        }



        public void UpdateCustomerById(int id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string adress)
        {
            var customerToUpdate = _context.Customers.Find(id);

            if (customerToUpdate == null)
            {
                throw new ArgumentException("No customer with such id");
            }

            customerToUpdate.FirstName = firstName;
            customerToUpdate.LastName = lastName;
            customerToUpdate.DateOfBirth = dateOfBirth;
            customerToUpdate.PhoneNumber = phoneNumber;
            customerToUpdate.Adress = adress;

            _context.Customers.AddOrUpdate(customerToUpdate);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customerToUpdate)
        {
            if (customerToUpdate == null)
            {
                throw new ArgumentException("Trying to update null reference customer entity");
            }
            _context.Customers.AddOrUpdate(customerToUpdate);
            _context.SaveChanges();
        }

        public void DeleteCustomerById(int id)
        {
            var customerToDelete = _context.Customers.Find(id);

            if (customerToDelete == null) return;

            _context.Customers.Attach(customerToDelete);
            _context.Customers.Remove(customerToDelete);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customerToDelete)
        {
            if (_context.Entry(customerToDelete).State == EntityState.Detached)
            {
                _context.Customers.Attach(customerToDelete);
            }
            _context.Customers.Remove(customerToDelete);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
           
            var customer = _context.Customers.Find(customerId);

            if (customer == null)
            {
                throw new ArgumentException("No customer with such id");
            }
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = _context.Customers.ToList();

            return customers;
        }

    }
}
