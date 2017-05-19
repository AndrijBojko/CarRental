using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Repositories.Abstract;
using CarRental.DAL.Context;
using CarRental.Entities;

namespace CarRental.DAL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalContext _context;

        public CarRepository(CarRentalContext context)
        {
            _context = context;
        }

        public void AddCar(string make, string model, string type, string transmission, int seatsNumber)
        {
            _context.Cars.Add(new Car()
            {
                Make = make,
                Model = model,
                Type = type,
                Transmission = transmission,
                SeatsNumber = seatsNumber
            });

                _context.SaveChanges();
        }

        public void AddCar(Car newCar)
        {
            if (newCar == null)
            {
                throw new ArgumentException("Trying to add null reference car entity");
            }
            _context.Cars.Add(newCar);
            _context.SaveChanges();
        }

        public void UpdateCarById(int id, string make, string model, string type, string transmission, int seatsNumber)
        {
            var carToUpdate = _context.Cars.Find(id);

            if (carToUpdate == null)
            {
                throw new ArgumentException("No car with such id");
            }

            carToUpdate.Make = make;
            carToUpdate.Model = model;
            carToUpdate.Type = type;
            carToUpdate.Transmission = transmission;
            carToUpdate.SeatsNumber = seatsNumber;

            _context.Cars.AddOrUpdate(carToUpdate);
            _context.SaveChanges();
        }

        public void UpdateCar(Car carToUpdate)
        {
            if (carToUpdate == null)
            {
                throw new ArgumentException("Trying to update null reference car entity");
            }
            
            _context.Cars.AddOrUpdate(carToUpdate);
            _context.SaveChanges();
        }

        public void DeleteCarById(int id)
        {
            var carToDelete = _context.Cars.Find(id);

            if (carToDelete == null) return;

            _context.Cars.Attach(carToDelete);
            _context.Cars.Remove(carToDelete);
            _context.SaveChanges();
        }

        public void DeleteCar(Car carToDelete)
        {
            if (_context.Entry(carToDelete).State == EntityState.Detached)
            {
                _context.Cars.Attach(carToDelete);
            }
            _context.Cars.Remove(carToDelete);
            _context.SaveChanges();
        }

        public List<Car> GetAllCars()
        {
            List<Car> cars = _context.Cars.ToList();

            return cars;
        }

        public List<Car> GetRentedCars()
        {
            var rentedCars = from or in _context.Orders
                                 join cr in _context.Cars on or.CarId equals cr.Id
                                select cr;

            return rentedCars.ToList();
        }


        public List<Car> GetAvailableCars()
        {
            List<Car> allCars = GetAllCars();
            List<Car> rentedCars = GetRentedCars();

            var availableCars = allCars.Except(rentedCars);

            return availableCars.ToList();
        }


        public List<Car> GetAvailableCarsByType(string type)
        {
            List<Car> allAvailableCars = GetAvailableCars();

            var availableByType = from ac in allAvailableCars
                where ac.Type == type
                select ac;

            return availableByType.ToList();
        }
        
    }
}
