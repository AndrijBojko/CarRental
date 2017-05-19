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
    public partial class AddCustomer : Window
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Customer _customerToUpdate;
        public bool IsCanceled { get; set; }
        public AddCustomer(UnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
        }

        public AddCustomer(UnitOfWork unitOfWork, Customer customerToUpdate)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            this.Title = "Update Customer";
            _customerToUpdate = customerToUpdate;
            SetCurrentCustomer();
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            Customer newCustomer = new Customer()
            {
                FirstName = FNameTb.Text,
                LastName = LNameTb.Text,
                DateOfBirth = (DateTime)BirthPicker.SelectedDate,
                PhoneNumber = Regex.Replace(PhoneNumbTb.Text, @"(\d{3})(\d{7})", "($1)$2"),
                Adress = AdressTb.Text
            };


            if (_customerToUpdate != null)
            {
                newCustomer.Id = _customerToUpdate.Id;
                UpdateCustomer(newCustomer);
            }
            else
            {
                _unitOfWork.CustomerRepository.AddCustomer(newCustomer);
            }

            IsCanceled = false;
            this.Close();

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            IsCanceled = true;
            this.Close();
        }


        void SetCurrentCustomer()
        {
            FNameTb.Text = _customerToUpdate.FirstName;
            LNameTb.Text = _customerToUpdate.LastName;
            BirthPicker.SelectedDate = _customerToUpdate.DateOfBirth;
            PhoneNumbTb.Text = Regex.Replace(_customerToUpdate.PhoneNumber, @"\D", "");
            AdressTb.Text = _customerToUpdate.Adress;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(FNameTb.Text) || 
                string.IsNullOrEmpty(LNameTb.Text) ||
                string.IsNullOrEmpty(PhoneNumbTb.Text) ||
                string.IsNullOrEmpty(AdressTb.Text))
            {
                MessageBox.Show("Fill all fields!");
                return false;
            }

            if (BirthPicker.SelectedDate == null)
            {
                MessageBox.Show("Please, select date!");
                return false;
            }

            if (BirthPicker.SelectedDate > (DateTime.Today.AddYears(-18)))
            {
                MessageBox.Show("Customer can`t drive car due to his age!");
                return false;
            }

            if (PhoneNumbTb.Text.Length != 10 )
            {
                MessageBox.Show("Phone number must have 10 numbers!");
                return false;
            }

            return true;
        }

        private void UpdateCustomer(Customer updatedCustomer)
        {
            try
            {
                _unitOfWork.CustomerRepository.UpdateCustomer(updatedCustomer);

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
