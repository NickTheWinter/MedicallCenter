using MedicalCenter.Pages;
using MedicallCenter;
using MedicallCenter.Clasees;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MedicalCenter
{
    class Manager
    {
        public static Frame frame { get; set; }

        static bool israbot = true;


        public static void StartTimer()
        {
            Thread thread = new Thread(TimerCallback);
            israbot = true;
            thread.Start();
        }

        public static void KillTimer()
        {
            israbot = false;
        }

       static void TimerCallback()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Timer timer = new Timer((state) =>
            {
                if (stopwatch.Elapsed.Minutes == 45)
                    MessageBox.Show("Время до завершения сессии: " + 15 + " минут");
            }, null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
            while (stopwatch.Elapsed.Hours < 1)
                if (!israbot) return;
            CurrentData.worker = null;
            Application.Current.Dispatcher.Invoke(() =>
            {
                Manager.frame.Navigate(new Page_Authorization());
            });

        }
    }
}
