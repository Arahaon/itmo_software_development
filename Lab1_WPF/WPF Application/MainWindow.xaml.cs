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

namespace WPF_Application
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.ComponentModel.BackgroundWorker aWorker = new System.ComponentModel.BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            aWorker.WorkerSupportsCancellation = true;
            aWorker.DoWork += aWorker_DoWork;
            aWorker.RunWorkerCompleted += aWorker_RunWorkerCompleted;
        }
        //Запуск фонового процесса
        private void aWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 1; j <= 10000000; j++)
                {
                }
                if (aWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                UpdateDelegate update = new UpdateDelegate(UpdateLabel);
                lblCycles.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Normal, update, i);
            }
        }
        //Инициируется при завершении или отмене фонового процесса
        private void aWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!(e.Cancelled))
                lbl.Content = "Run Completed";
            else
                lbl.Content = "Run Cancelled";
        }
        private delegate void UpdateDelegate(int i);
        private void UpdateLabel(int i)
        {
            lblCycles.Content = "Cycles: " + i.ToString();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            aWorker.CancelAsync();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            aWorker.RunWorkerAsync();
        }
    }
}
