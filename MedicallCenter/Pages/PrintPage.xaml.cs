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

namespace MedicallCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для PrintPage.xaml
    /// </summary>
    public partial class PrintPage : Page
    {

        public PrintPage(Service currentService, BitmapImage image)
        {
            InitializeComponent();

            IdService.Text = currentService.id.ToString();
            TitleService.Text = currentService.service1.ToString();
            PriceService.Text = currentService.price.ToString();
            ImageService.Source = image;
        }
    }
}
