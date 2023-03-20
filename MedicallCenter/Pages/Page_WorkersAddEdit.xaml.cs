using MedicallCenter;
using MedicallCenter.Clasees;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Xml.Linq;

namespace MedicalCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>

    public partial class Page_WorkersAddEdit : Page
    {
        private Worker curerentworker;
        public Page_WorkersAddEdit(Worker worker)
        {
            InitializeComponent();
            ComboDolgnost.ItemsSource = CurrentData.db.Type.ToList();
            curerentworker = worker;
            if (curerentworker.id != 0)
            {
                InputData();
                btnEditWorker.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Workers());
        }

        private void InputData()
        {
            tbNameWorker.Text = curerentworker.name;
            tbLoginWorker.Text = curerentworker.login;
            tbPasswordWorker.Text = curerentworker.password;
            tbIpWorker.Text = curerentworker.ip;
            tbLasneterWorker.Text = curerentworker.lastenter;
            JArray json = JArray.Parse(curerentworker.services);
            List<Service> cur_services = new List<Service>();
            foreach (JObject jobj in json)
            {
                cur_services.Add(CurrentData.services.FirstOrDefault(n => n.kod_service == (int)jobj["code"]));
            }
            ComboService.ItemsSource = cur_services;
            ComboDolgnost.Text = curerentworker.Type1.role;
        }

        private void btnAddWorker_Click(object sender, RoutedEventArgs e)
        {
            if (GetData())
            {
                CurrentData.db.Worker.Add(curerentworker);
                CurrentData.db.SaveChanges();
                MessageBox.Show("Добавлено");
                Manager.frame.Navigate(new Page_Workers());
            }
        }

        private bool GetData()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (tbNameWorker.Text == "")
                stringBuilder.Append("Заполните поле имя: пусто\n");
            if (tbLoginWorker.Text == "")
                stringBuilder.Append("Заполните поле логин: пусто\n");
            if (tbPasswordWorker.Text == "")
                stringBuilder.Append("Заполните поле пароль: пусто\n");
            if (tbIpWorker.Text == "")
                stringBuilder.Append("Заполните поле ip: пусто\n");
            if (tbLasneterWorker.Text == "")
                stringBuilder.Append("Заполните поле время: пусто\n");
            if (ComboService.ItemsSource == null)
                stringBuilder.Append("Заполните поле услуги: пусто\n");
            if (ComboDolgnost.SelectedItem  == null)
                stringBuilder.Append("Заполните поле должность: пусто\n");
            if (stringBuilder.ToString() == "")
            {
                curerentworker.name = tbNameWorker.Text;
                curerentworker.login = tbLoginWorker.Text;
                curerentworker.password = tbPasswordWorker.Text;
                curerentworker.ip = tbIpWorker.Text;
                curerentworker.lastenter = tbLasneterWorker.Text;
                curerentworker.services = GetServises(ComboService.ItemsSource as List<Service>);
                curerentworker.type = CurrentData.db.Type.ToList().Where(x => x.role == ComboDolgnost.Text).FirstOrDefault().id;
                return true;
            }
            MessageBox.Show(stringBuilder.ToString());
            return false;
        }

        string GetServises(List<Service> list)
        {
            JArray myArray = new JArray();
            foreach (var item in list)
            {
                JObject obj = new JObject();
                obj.Add("code", item.kod_service);
                myArray.Add(obj);
            }
            return myArray.ToString();
        }

        private void btnEditWorker_Click(object sender, RoutedEventArgs e)
        {
            if (GetData())
            {
                CurrentData.db.Worker.AddOrUpdate(curerentworker);
                CurrentData.db.SaveChanges();
                MessageBox.Show("Запись успешно изменена");
                Manager.frame.Navigate(new Page_Workers());
            }
        }
    }
}
