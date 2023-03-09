﻿using System;
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
using MedicalCenter.Pages;
using MedicallCenter;
using MedicallCenter.Clasees;


namespace MedicalCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Page_Home : Page
    {
        public Page_Home(Worker worker)
        {
            InitializeComponent();
            GetInfoWorker(worker);
        }

        private void button_service_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Servicee());
        }

        private void button_worker_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Workers());
        }

        private void button_user_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Users());
        }

        private void button_result_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Result());
        }

        private void GetInfoWorker(Worker worker)
        {
            tbName.Text = worker.name;
            if (worker.type == 1)
                tbRole.Text = "Администратор";
            if (worker.type == 2)
                tbRole.Text = "Пользователь";
            if (worker.type == 3)
                tbRole.Text = "Лаборант";
        }
    }
}
