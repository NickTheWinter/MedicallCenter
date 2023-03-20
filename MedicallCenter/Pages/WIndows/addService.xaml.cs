using MedicalCenter.Pages;
using MedicallCenter.Clasees;
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

namespace MedicallCenter.Pages.WIndows
{
    /// <summary>
    /// Логика взаимодействия для addService.xaml
    /// </summary>
    public partial class addService : Window
    {
        ComboBox ComboBox;
        public addService(ComboBox comboBox)
        {
            InitializeComponent();
            combobox.ItemsSource = CurrentData.services;
            ComboBox = comboBox;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var listcombo = ComboBox.ItemsSource as List<Service> ?? new List<Service>();
            var service = combobox.SelectedItem as Service;
            listcombo.Add(CurrentData.services.FirstOrDefault(x => x.service1 == service.service1));
            ComboBox.ItemsSource = listcombo;
            ComboBox.Items.Refresh();
            this.Close();
        }

        private void CLose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
