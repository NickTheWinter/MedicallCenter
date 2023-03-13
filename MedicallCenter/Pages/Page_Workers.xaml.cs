﻿using MedicallCenter;
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
    public partial class Page_Workers : Page
    {
        private int pageNumber = 0;
        private int maxpage = 0;
        private int pageSize = 20;
        List<Worker> workers = new List<Worker>();

        public Page_Workers(Worker worker)
        {
            InitializeComponent();

            if (worker.Type1.id != 1)
            {
                btn_deleteWorkers.Visibility = Visibility.Hidden;
                btn_addeditWorkers.Visibility = Visibility.Hidden;
                
            }
            workers = CurrentData.workers;
            maxpage = workers.Count / pageSize;
            DisplayDataInGrid();
        }
        private void DisplayDataInGrid()
        {
            maxpage = workers.Count / pageSize;
            var currentPageData = workers.Skip(pageNumber * pageSize).Take(pageSize); // отображаем только данные для текущей страницы
            DataGridWorkers.ItemsSource = currentPageData; // отображаем данные в DataGrid
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if(pageNumber < maxpage)
            {
                pageNumber++; // переход на следующую страницу
                DisplayDataInGrid(); // отображение данных
            }
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber > 0)
            {
                pageNumber--; // переход на предыдущую страницу
                DisplayDataInGrid(); // отображение данных
            }
        }
        private void search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (search.Text == "Поиск")
                search.Text = "";
            else if (search.Text == "")
                search.Text = "Поиск";
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && DataGridWorkers != null)
            {
                workers = workers.Where(n=>n.name.ToLower().Contains(search.Text.ToLower())).ToList();

                DisplayDataInGrid();
            }
            else
            {
                if (DataGridWorkers != null)
                {
                    workers = CurrentData.workers;
                    DisplayDataInGrid();
                }

            }
        }
        private void btnDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            var workerForDelete = DataGridWorkers.SelectedItems.Cast<Worker>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {workerForDelete} записи", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CurrentData.db.Worker.RemoveRange(workerForDelete);
                    CurrentData.db.SaveChanges();
                    DataGridWorkers.ItemsSource = CurrentData.db.Worker.ToList();
                    //DataGridResult.ItemsSource = CurrentData.results;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Worker worker = DataGridWorkers.SelectedValue as Worker;
            if (CurrentData.worker.Type1.id == 1)
                Manager.frame.Navigate(new Page_WorkersAddEdit(worker));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Home(CurrentData.worker));
        }

        private void btnAddEditWorker_Click(object sender, RoutedEventArgs e)
        {
            Worker worker = new Worker();
            Manager.frame.Navigate(new Page_WorkersAddEdit(worker));
        }
    }
}
