using MedicallCenter;
using MedicallCenter.Clasees;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MedicalCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Result.xaml
    /// </summary>
    public partial class Page_ResultAddEdit : Page
    {
        private Result currentresult = new Result();
        public Page_ResultAddEdit(Result currentresult2)
        {
            InitializeComponent();
            currentresult = currentresult2;

            ComboUser112.ItemsSource = CurrentData.db.User.ToList();
            ComboWorker.ItemsSource = CurrentData.db.Worker.ToList();
            ComboService.ItemsSource = CurrentData.db.Service.ToList();
            if (currentresult.id != 0)
            {
                InputData();
                btnEditResult.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Result());
        }


        private void InputData()
        {
            ComboUser112.Text = currentresult.User.name;
            ComboWorker.Text = currentresult.Worker.name;
            ComboService.Text = currentresult.Service.service1;
            ComboResultResult.Text = currentresult.result1;
            tbDateResult.Text = currentresult.date;
        }

        private void btnEditSecodnResult_Click(object sender, RoutedEventArgs e)
        {
            if (GetData())
            {
                CurrentData.db.Result.AddOrUpdate(currentresult);
                SaveChang();
                MessageBox.Show("Запись успешно добавлена");
                btnEditResult.Visibility = Visibility.Visible;
            }
        }

        private bool GetData()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (ComboUser112.SelectedItem == null)
                stringBuilder.Append("Заполните поле Пациент: пусто\n");

            if (ComboWorker.SelectedItem == null)
                stringBuilder.Append("Заполните поле Работник: пусто\n");

            if (ComboService.SelectedItem == null)
                stringBuilder.Append("Заполните поле Услуга: пусто\n");

            if (ComboResultResult.Text == "")
                stringBuilder.Append("Заполните поле Результат: пусто\n");
            if (tbDateResult.Text == "")
                stringBuilder.Append("Заполните поле Дата: пусто\n");

            if (stringBuilder.ToString() == "")
            {
                currentresult.id_user = CurrentData.users.FirstOrDefault(x => x.name == ComboUser112.Text).id;
                currentresult.id_lab = CurrentData.workers.FirstOrDefault(x => x.name == ComboWorker.Text).id;
                currentresult.id_service = CurrentData.services.FirstOrDefault(x => x.service1 == ComboService.Text).id;
                currentresult.result1 = ComboResultResult.Text;
                currentresult.date = tbDateResult.Text;
                return true;
            }
            MessageBox.Show(stringBuilder.ToString());
            return false;
        }
        private void SaveChang()
        {
            CurrentData.db.SaveChanges();
        }

        private void bntAddResult_Click(object sender, RoutedEventArgs e)
        {
            if (GetData())
            {
                CurrentData.db.Result.Add(currentresult);
                SaveChang();
                MessageBox.Show("Запись успешно добавлена");
                Manager.frame.Navigate(new Page_Result());
            }
        }

        private void btnEditResult_Click(object sender, RoutedEventArgs e)
        {
            if (GetData())
            {
                CurrentData.db.Result.AddOrUpdate(currentresult);
                SaveChang();
                MessageBox.Show("Запись успешно отредактирована");
                Manager.frame.Navigate(new Page_Result());
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
