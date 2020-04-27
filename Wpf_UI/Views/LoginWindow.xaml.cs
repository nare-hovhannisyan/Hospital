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
using System.Data.Entity;
using Wpf_UI.Models;
using Wpf_UI.Views;
using Wpf_UI.DataAccess;
using Wpf_UI.Services;

namespace Wpf_UI.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();           
        }

        User user = new User();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using(HospitalDbContext db = new HospitalDbContext())
            {           
                var userCheck = db.Users.FirstOrDefault(x => x.Login == txtUsername.Text);
                if ((user == null) | (userCheck.Password != HashFunction.ComputeSha256Hash(txtPassword.Password)))
                {
                    MessageBox.Show("Invalid username or password");
                } else
                {
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                }
            }
        }
    }
}
