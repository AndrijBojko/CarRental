using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarRental.DAL.UnitOfWork;
using CarRental.Entities;
using CarRental.Entities.HelpClass;
using CarRental.UI.Code;


namespace CarRental.UI
{
    public partial class MainWindow : Window
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        private List<Customer> _customersList = new List<Customer>();
        private List<Car> _carsList = new List<Car>();
        private List<Car> _availableCarsList = new List<Car>();
        private List<ActiveOrder> _activeOrders = new List<ActiveOrder>();

        private Manager _currentManager;
        private const int FirstIndex = 0;

        public MainWindow(Manager currentManager)
        {
            InitializeComponent();

            SetCurrentManager(currentManager);
            RemoveFinishedOrdersFromDb();
            LoadDataToElements();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateRegastrationInput())
            {
                return;
            }

            DateTime finishOfRentDateTime = DateTimeConverter.ConvertFromString(DatePicker.Text, ExpiryHoursTb.Text, ExpityMinutesTb.Text);

            if (finishOfRentDateTime < DateTime.Now)
            {
                MessageBox.Show("Expiry date and time can`t be earlier then now !");
                return;
            }

            int carId = (AvailableCarsComboBx.SelectedItem as Car).Id;
            int customerId = (CustomerSelectComboBx.SelectedItem as Customer).Id;

            _unitOfWork.OrderRepository.AddOrder(_currentManager.Id, carId, customerId, DateTime.Now, finishOfRentDateTime);
            
            RefreshAvailableCars();
            MessageBox.Show("Registered successfully!");

        }

        private void AddNewCarBtn_Click(object sender, RoutedEventArgs e)
        {
            var addNewCarDialog = new AddCar(_unitOfWork);
            addNewCarDialog.ShowDialog();

            if (addNewCarDialog.IsCanceled)
            {
                return;
            }

            RefreshAllCars();
            RefreshAvailableCars();
        }


        private void UpdateCarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AllCarsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select car");
                return;
            }
            var carToUpdate = AllCarsDataGrid.SelectedItem as Car;
            var updateCarDialog = new AddCar(_unitOfWork, carToUpdate);
            updateCarDialog.ShowDialog();

            if (updateCarDialog.IsCanceled)
            {
                return;
            }

            RefreshAllCars();
            RefreshAvailableCars();
        }


        private void DeleteCarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AllCarsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select customer");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            var carToDelete = AllCarsDataGrid.SelectedItem as Car;
            _unitOfWork.CarRepository.DeleteCar(carToDelete);

            RefreshAllCars();
            RefreshAvailableCars();
        }

        private void AddNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var addNewCustomerDialog = new AddCustomer(_unitOfWork);
            addNewCustomerDialog.ShowDialog();

            if (addNewCustomerDialog.IsCanceled)
            {
                return;
            }

            RefreshAllCustomers();
        }

        private void UpdateCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AllCustomersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select customer");
                return;
            }
            var customerToUpdate = AllCustomersDataGrid.SelectedItem as Customer;
            var updateCustomerDialog = new AddCustomer(_unitOfWork, customerToUpdate);
            updateCustomerDialog.ShowDialog();

            if (updateCustomerDialog.IsCanceled)
            {
                return;
            }

            RefreshAllCustomers();
        }

        private void DeleteCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AllCustomersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select customer");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            var customerDelete = AllCustomersDataGrid.SelectedItem as Customer;
            _unitOfWork.CustomerRepository.DeleteCustomer(customerDelete);

            RefreshAllCustomers();
        }

        private void TypeCheckBx_Checked(object sender, RoutedEventArgs e)
        {
            CarTypeLbl.IsEnabled = true;
            CarTypeCombBx.IsEnabled = true;
        }

        private void TypeCheckBx_Unchecked(object sender, RoutedEventArgs e)
        {
            CarTypeLbl.IsEnabled = false;
            CarTypeCombBx.IsEnabled = false;
            LoadAvailableCars();
        }

        private void CarTypeCombBx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem carTypeItem = (ComboBoxItem)CarTypeCombBx.SelectedItem;
            string carType = carTypeItem.Content.ToString();
            LoadAvailableCars(carType);
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            var logInDialog = new Login();
            logInDialog.Show();

            this.Close();
        }


        #region InitializationMethods

        public void LoadDataToElements()
        {
            LoadCustomers();
            LoadAllCars();
            LoadAvailableCars();
            LoadActiveOrders();
        }

        public void LoadCustomers()
        {

            _customersList = _unitOfWork.CustomerRepository.GetAllCustomers();

            AllCustomersDataGrid.ItemsSource = _customersList;

            CustomerSelectComboBx.ItemsSource = _customersList;

        }

        public void LoadAllCars()
        {
            _carsList = _unitOfWork.CarRepository.GetAllCars();

            AllCarsDataGrid.ItemsSource = _carsList;
        }

        public void LoadAvailableCars(string carType = "")
        {
            if (string.IsNullOrEmpty(carType))
            {
                _availableCarsList = _unitOfWork.CarRepository.GetAvailableCars();
            }
            else
            {
                _availableCarsList = _unitOfWork.CarRepository.GetAvailableCarsByType(carType);
            }

            AvailableCarsComboBx.ItemsSource = _availableCarsList;

            AvailableCarsComboBx.SelectedIndex = FirstIndex;
        }

        private void LoadActiveOrders()
        {
            _activeOrders = _unitOfWork.OrderRepository.GetAllActiveOrders();

            ActiveOrdersDataGrid.ItemsSource = _activeOrders;
        }

        public void SetCurrentManager(Manager currentManager)
        {
            _currentManager = currentManager;

            CurrentManagerLbl.Content = $"Manager: {_currentManager.FirstName} {_currentManager.LastName}";

        }

        private void RemoveFinishedOrdersFromDb()
        {
            _unitOfWork.OrderRepository.RemoveFinishedOrders();
        }

        #endregion

        #region ValidationMethods

        private bool ValidateRegastrationInput()
        {

            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please, select date!");
                return false;
            }

            if (string.IsNullOrEmpty(ExpiryHoursTb.Text) || string.IsNullOrEmpty(ExpityMinutesTb.Text))
            {
                MessageBox.Show("Time fields can`t be empty!");
                return false;
            }

            int expiryHours = Convert.ToInt32(ExpiryHoursTb.Text);
            if (expiryHours < 0 || expiryHours > 23)
            {
                MessageBox.Show("Invalide hours input!");
                return false;
            }

            int expiryMinutes = Convert.ToInt32(ExpiryHoursTb.Text);
            if (expiryMinutes < 0 || expiryMinutes > 59)
            {
                MessageBox.Show("Invalide minutes input!");
                return false;
            }

            if (AvailableCarsComboBx.SelectedItem == null)
            {
                MessageBox.Show("No cars available!");
                return false;
            }

            if (CustomerSelectComboBx.SelectedItem == null)
            {
                MessageBox.Show("No customer selected!");
                return false;
            }

            return true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region RefreshMethods

        private void RefreshAllCars()
        {
            LoadAllCars();
        }
        private void RefreshAvailableCars()
        {
            LoadAvailableCars();
        }

        private void RefreshAllCustomers()
        {
            LoadCustomers();
        }

        #endregion

    }
}
