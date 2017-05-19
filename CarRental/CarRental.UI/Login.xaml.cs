using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CarRental.DAL.Repositories;
using CarRental.DAL.UnitOfWork;
using CarRental.Entities;
using CarRental.UI.Code;

namespace CarRental.UI
{
    public partial class Login : Window
    {
        private readonly UnitOfWork _unitOfWork;

        public Login()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
        }

        private void Login_Btn(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text;
            string password = Encrypt.GetHash(passwordTb.Password);

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Login or(and) password is null or empty");
            }

            Manager manager = _unitOfWork.ManagerRepository.GetManagerByLogin(login, password);

            if (manager == null)
            {
                MessageBox.Show("Invalid login or password!");
                return;
            }
            
                MainWindow window = new MainWindow(manager);
                window.Show();
                this.Close();
            
        }

        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
