using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
using Syncfusion.UI.Xaml.Charts;
using System.Configuration;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace AMISystemManagemenet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static ObservableCollection<DevClass> devices { get; set; }
        public static ObservableCollection<Merenje> merenja { get; set; }
        public static ObservableCollection<Shared.Agregator> agregators { get; set; }


        public MainWindow()
        {

            InitializeComponent();
            //ServiceHost svc = new ServiceHost(typeof(AgregatorMethods));
            //svc.AddServiceEndpoint(typeof(IAgregator),
            //new NetTcpBinding(),
            //new Uri("net.tcp://localhost:5000/AgregatorMethods"));

            //svc.Open();
            ChannelFactory<IAgregator> factory = new ChannelFactory<IAgregator>(
                    new NetTcpBinding(),
                    new EndpointAddress("net.tcp://localhost:5000/AgregatorMethods"));

            IAgregator kanal = factory.CreateChannel();

            devices = new ObservableCollection<DevClass>();

            Thread.Sleep(10000);

            DataBaseOperations dbo = new DataBaseOperations();
            dbo.Isprazni();
            dbo.IsprazniM();
            dbo.IsprazniD();

            int i = kanal.BazaACnt();

            for (int j = 1; j <= i; j++)
            {
                if (kanal.dobaviAgregator(j, out Shared.Agregator a1))
                {
                    dbo.SaveAgregator(a1);

                }
            }

            List<DevClass> dvcs = new List<DevClass>();

            for (int j = 1; j <= kanal.BazaDCnt(); j++) {
                if (kanal.dobaviDev(j, out DevClass d)) {
                    dvcs.Add(d);
                    dbo.SaveData(d);
                    foreach (Merenje m in d.ListMerenje.Values) {
                        if (kanal.dobaviMer(m.Id, d, out Merenje m1))
                        {
                            dbo.SaveMerenje(m1);
                        }
                    }
                }
            }
            
        }

        private void ButtonDeviceOn_Click(object sender, RoutedEventArgs e)
        {
            DataBaseOperations dbo = new DataBaseOperations();

            //devices = new ObservableCollection<DevClass>();
            merenja = new ObservableCollection<Merenje>();
            List<Merenje> ms = new List<Merenje>();
            agregators = new ObservableCollection<Shared.Agregator>();
            List<DevClass> devs = dbo.LoadDevices();
            List<Shared.Agregator> ags = new List<Shared.Agregator>();
            
            foreach (DevClass dc in devs)
            {
                ags = dbo.LoadAgregators();
                ms = dbo.LoadMerenja();
                devices.Add(dc);
                
            }
            foreach (Merenje m in ms) {
                    merenja.Add(m);
               }
            foreach (Shared.Agregator a in ags) {
                agregators.Add(a);
            }

            DataGridAg.ItemsSource = agregators;
            DataGridDevices.ItemsSource = devices;
            DataGridMerenja.ItemsSource = merenja;
            DataGridDevices.Items.Refresh();
            DataGridMerenja.Items.Refresh();
            DataGridAg.Items.Refresh();
            
        }
        private void GraphBtnClick(object sender, RoutedEventArgs e) {
            merenja = new ObservableCollection<Merenje>();
            DataBaseOperations dbo = new DataBaseOperations();
            List<Merenje> ms = new List<Merenje>();
            List<DevClass> devs = dbo.LoadDevices();
            foreach (DevClass dc in devs)
            {
                
                ms = dbo.LoadMerenja();
                devices.Add(dc);

            }
            foreach (Merenje m in ms)
            {
                merenja.Add(m);
            }

            

        }


    }
}
