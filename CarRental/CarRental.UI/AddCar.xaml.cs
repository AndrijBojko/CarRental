using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using CarRental.DAL.UnitOfWork;
using CarRental.Entities;

namespace CarRental.UI
{   

    public partial class AddCar : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Car _carToUpdate;
        public bool IsCanceled { get; set; }

        public AddCar(UnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
        }

        public AddCar(UnitOfWork unitOfWork, Car carToUpdate)
        {
            InitializeComponent();

            _unitOfWork = unitOfWork;
            this.Title = "Update Customer";
            _carToUpdate = carToUpdate;
            SetCurrentCar();
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            Car newCar =new Car()
            {
                Make = MakeTb.Text,
                Model = ModelTb.Text,
                Type = CarTypeCombBx.Text,
                Transmission = TransmissionCombBx.Text,
                SeatsNumber = Convert.ToInt32(SeatsNumbTb.Text)
            };


            if (_carToUpdate != null)
            {
                newCar.Id = _carToUpdate.Id;
                UpdateCar(newCar);
            }
            else
            {
                _unitOfWork.CarRepository.AddCar(newCar);
            }

            IsCanceled = false;
            this.Close();

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            IsCanceled = true;
            this.Close();
        }

        private  bool ValidateInput()
        {
            if (string.IsNullOrEmpty(MakeTb.Text) || string.IsNullOrEmpty(ModelTb.Text) || string.IsNullOrEmpty(SeatsNumbTb.Text))
            {
                MessageBox.Show("Fill all fields!");
                return false;
            }

            return true;
        }

        private void SetCurrentCar()
        {
            MakeTb.Text = _carToUpdate.Make;
            ModelTb.Text = _carToUpdate.Model;
            CarTypeCombBx.Text = _carToUpdate.Type;
            TransmissionCombBx.Text = _carToUpdate.Transmission;
            SeatsNumbTb.Text = _carToUpdate.SeatsNumber.ToString();
        }

        private void UpdateCar(Car carToUpdate)
        {
            try
            {
                _unitOfWork.CarRepository.UpdateCar(carToUpdate);
                IsCanceled = false;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                IsCanceled = true;
            }
            finally
            {
                this.Close();
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
