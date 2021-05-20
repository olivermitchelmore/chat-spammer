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
using System.Windows.Forms;
using System.Threading;

namespace AutoChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            status.Content = "Status: Stopped\nSent: " + msgsent; ;
        }

        private void activate_Click(object sender, RoutedEventArgs e)
        {
            if (afktoggle)
                anti_Afk();

            else
                send_Text();

            run = true;
        }

        bool run = false;
        int time = 1000;
        bool autoenter = false;
        int afkbot = 1;
        int msgsent;
        bool afktoggle;

        static Random random = new Random();

        public void send_Text()
        {

            string content = text.Text;
            int randomAmount = (int)randomval.Value;

            if (randomize.IsChecked == false)
                randomAmount = 0;

            new Thread(new ThreadStart(() =>
           {
               while (run)
               {
                   try
                   {
                       activate.Dispatcher.Invoke(() => { status.Content = "Status: waiting\nSent: " + msgsent.ToString(); });

                       Thread.Sleep(time);

                       if (randomAmount != 0)
                           Thread.Sleep(random.Next(0, randomAmount));

                       if (!run)
                           break;

                       msgsent++;

                       activate.Dispatcher.Invoke(() => { status.Content = "Status: sending\nSent: " + msgsent.ToString(); });

                       SendKeys.SendWait(content);


                       if (autoenter)
                       {
                           SendKeys.SendWait("{Enter}");
                       }


                   }

                   catch (Exception)
                   {

                   }
               }
           })).Start();
        }

        public void anti_Afk()
        {
            int randomAmount = (int)randomval.Value;

            if (randomize.IsChecked == false)
                randomAmount = 0;

            new Thread(new ThreadStart(() =>
            {
                while (run)
                {

                    activate.Dispatcher.Invoke(() => { status.Content = "Status: sending"; });

                    Thread.Sleep(time);

                    if (randomAmount != 0)
                        Thread.Sleep(random.Next(0, randomAmount));

                    activate.Dispatcher.Invoke(() => { status.Content = "Status: waiting"; });

                    if (afkbot == 5)
                        afkbot = 0;

                    if (afkbot == 1)
                        SendKeys.SendWait("w");

                    Thread.Sleep(500);

                    if (afkbot == 2)
                        SendKeys.SendWait("a");

                    Thread.Sleep(500);

                    if (afkbot == 3)
                        SendKeys.SendWait("s");

                    Thread.Sleep(500);

                    if (afkbot == 4)
                        SendKeys.SendWait("d");

                    Thread.Sleep(500);

                    afkbot++;

                }
            })).Start();
        }
        private void time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (testing != null)
            {
                time = ((int)times.Value);
                string displaytime = ((int)times.Value).ToString();
                testing.Content = ("Time Inbetween Messages: " + displaytime + "ms");
            }


        }

        private void auto_Enter(object sender, RoutedEventArgs e)
        {
            autoenter = true;
        }

        private void auto_Enteroff(object sender, RoutedEventArgs e)
        {
            autoenter = false;
        }

        private void anti_afkOn(object sender, RoutedEventArgs e)
        {
            afktoggle = true;
        }

        private void randomnumber_Checked(object sender, RoutedEventArgs e)
        {

        }

        public void randomval_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (randisplay != null)
            {
                time = ((int)randomval.Value);
                string randisplaynum = ((int)randomval.Value).ToString();
                randisplay.Content = ("Random Offset: " + randisplaynum + "ms");
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            run = false;
            msgsent = 0;
            status.Content = "Status: Stopped\nSent: " + msgsent.ToString();
            afktoggle = false;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            text.Clear();
            afktoggle = false;
        }
    }
}