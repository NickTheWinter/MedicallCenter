﻿using MedicallCenter;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Page_Users : Page
    {
        public Page_Users()
        {
            InitializeComponent();
            DataGridUser.ItemsSource = EntitiesMedical.GetEntities().User.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Home(CurrentData.worker));
        }

        private void bntDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var userForDelete = DataGridUser.SelectedItems.Cast<User>().ToList();
            if(MessageBox.Show($"Вы точно хотите удалить следующие {userForDelete} записи","Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.Yes)
            {
                try
                {
                    EntitiesMedical.GetEntities().User.RemoveRange(userForDelete);
                    EntitiesMedical.GetEntities().SaveChanges();
                    DataGridUser.ItemsSource = EntitiesMedical.GetEntities().User.ToList();
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
