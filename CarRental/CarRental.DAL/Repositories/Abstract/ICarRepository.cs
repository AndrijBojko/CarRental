using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.DAL.Context;
using CarRental.Entities;

namespace CarRental.DAL.Repositories.Abstract
{
    public interface ICarRepository
    {
        void AddCar(string make, string model, string type, string transmission, int seatsNumber );
        void AddCar(Car newCar);
        void UpdateCarById(int id, string make, string model, string type, string transmission, int seatsNumber);
        void UpdateCar(Car carToUpdate);
        void DeleteCarById(int id);
        void DeleteCar(Car carToDelete);
        List<Car> GetAllCars();
        List<Car> GetAvailableCars();
        List<Car> GetAvailableCarsByType(string type);
    }
}
