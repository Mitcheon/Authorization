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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace UserApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();

            DoubleAnimation btnAnimation= new DoubleAnimation();
            btnAnimation.From = 0;
            btnAnimation.To = 450;
            btnAnimation.Duration = TimeSpan.FromSeconds(1);
            regButton.BeginAnimation(Button.WidthProperty, btnAnimation);

        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim(); // .Trim() — строковая функция, которая удаляет начальные и конечные пробелы
            string pass = passBox.Password.Trim();
            string pass_2 = passBox_2.Password.Trim();
            string email = textBoxEmail.Text.Trim().ToLower(); // .ToLower() преобразует прописные (большие) букв в строчные (маленькие).

            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Это поле введено некорректно"; // .ToolTip() Подсказка — элемент графического интерфейса, служит дополнительным средством обучения пользователя.
                textBoxLogin.Background = Brushes.DarkRed; // Brushes Определяет объекты, которые используются для заливки графических объектов. В классах, производных от Brush, описывается, как выполняется заливка области.
            } 
            else if(pass.Length < 5)
            {
                passBox.ToolTip = "Это поле введено некорректно";
                passBox.Background = Brushes.DarkRed;
            }
            else if (pass != pass_2)
            {
                passBox_2.ToolTip = "Это поле введено некорректно";
                passBox_2.Background = Brushes.DarkRed;
            }
            else if (email.Length < 5 || !email.Contains("@") || !email.Contains(".")) // .Contains() - Возвращает значение, указывающее, встречается ли указанный символ внутри этой строки, используя указанные правила сравнения
            {
                passBox_2.ToolTip = "Это поле введено некорректно";
                passBox_2.Background = Brushes.DarkRed;
            }
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Transparent;

                passBox.ToolTip = "";
                passBox.Background = Brushes.Transparent;

                passBox_2.ToolTip = "";
                passBox_2.Background = Brushes.Transparent;

                textBoxEmail.ToolTip = "";
                textBoxEmail.Background = Brushes.Transparent;

                MessageBox.Show("Всё успешно"); // Всплывающее окно

                User user = new User(login,email,pass);

                db.Users.Add(user);
                db.SaveChanges();

                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                Hide();
            }
        }

        private void Button_Window_Auth_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }
    }
}
