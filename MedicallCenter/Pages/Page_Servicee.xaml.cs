using MedicallCenter;
using MedicallCenter.Clasees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;

namespace MedicalCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для Servicee.xaml
    /// </summary>
    public partial class Page_Servicee : Page
    {
        private Service currentServis = new Service();
        private int pageNumber = 0;
        private int maxpage = 0;
        private int pageSize = 20;
        List<Serv> services = new List<Serv>();
        List<Serv> cur_services = new List<Serv>();
        List<DataGridRow> rows = new List<DataGridRow>();

        public Page_Servicee()
        {
            InitializeComponent();
            if (CurrentData.worker.Type1.id != 1)
            {
                btnDeleteService.Visibility = Visibility.Hidden;
                btnAddeditService.Visibility = Visibility.Hidden;
            }

            foreach (var item in CurrentData.db.Service.ToList())
            {
                services.Add(new Serv(item));
            }

            cur_services = new List<Serv>(services);
            DisplayDataInGrid();
        }

        class Serv
        {

            public BitmapImage image { get; set; }
            public int id { get; set; }
            public string service1 { get; set; }
            public Nullable<double> price { get; set; }
            public Nullable<int> kod_service { get; set; }
            public Serv(Service service)
            {
                this.id = service.id;
                this.service1 = service.service1;
                this.price = service.price;
                this.kod_service = service.kod_service;
            }
            public static List<Service> ToServiceList(List<Serv> servList)
            {
                return servList.Select(s => new Service
                {
                    id = s.id,
                    service1 = s.service1,
                    price = s.price,
                    kod_service = s.kod_service
                }).ToList();
            }

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_Home(CurrentData.worker));
        }
        private void DisplayDataInGrid()
        {
            maxpage = services.Count / pageSize;
            var currentPageData = services.Skip(pageNumber * pageSize).Take(pageSize);// отображаем только данные для текущей страницы
            SetImage(currentPageData);
            DataGridService.ItemsSource = currentPageData; // отображаем данные в DataGrid
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber < maxpage)
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
            if (search.Text != "Поиск" && search.Text != "" && DataGridService != null)
            {
                string s_text = search.Text.ToLower();
                services = cur_services;
                var s1 = services.Where(n => n.service1.ToLower().Contains(s_text)).ToList();
                var s2 = services.Where(n => n.kod_service.ToString().ToLower().Contains(s_text)).ToList();
                var s3 = services.Where(n => n.price.ToString().ToLower().Contains(s_text)).ToList();
                services = s1.Concat(s3.Concat(s2)).ToList();
                DisplayDataInGrid();


            }
            else
            {
                if (DataGridService != null)
                {
                    services = cur_services;
                    DisplayDataInGrid();
                }

            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentData.worker.Type1.id == 1)
            {
                Serv service = DataGridService.SelectedValue as Serv;
                Manager.frame.Navigate(new Page_ServiceeAddEdit(service.id, service.image));
            }
        }


        private void bntDeleteService_Click(object sender, RoutedEventArgs e)
        {
            var serviceForDelete = DataGridService.SelectedItems.Cast<Serv>().ToList();
            List<Service> serviceList = serviceForDelete.Select(s => CurrentData.db.Service.Find(s.id)).ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {serviceForDelete} записи", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CurrentData.db.Service.RemoveRange(serviceList);
                    CurrentData.db.SaveChanges();
                    services.Clear();
                    foreach (var item in CurrentData.db.Service.ToList())
                    {
                        services.Add(new Serv(item));
                    }
                    cur_services = new List<Serv>(services);

                    DisplayDataInGrid();
                    MessageBox.Show("Записи успешно удалены");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }


        private void btn_AddEditService_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Page_ServiceeAddEdit(0, null));
        }

        private void SetImage(IEnumerable<Serv> list)
        {
            foreach (var item in list)
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 100,
                        Width = 200
                    }
                };
                var result = writer.Write(item.kod_service.ToString());
                System.Drawing.Bitmap barcodeBitmap = null;
                using (var stream = new MemoryStream())
                {
                    result.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    barcodeBitmap = new System.Drawing.Bitmap(stream);
                    stream.Position = 0;
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    item.image = bitmapImage;
                }
            }
        }
    }
}

