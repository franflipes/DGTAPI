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
using DGT.UI.WPF.Vistas;

namespace DGT.UI.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        


       

        private void BtnQueries_Click(object sender, RoutedEventArgs e)
        {
            ContentControlMainView.Content = new VehiculosView();
        }

        private void BtnVehiculos_Click(object sender, RoutedEventArgs e)
        {
            ContentControlMainView.Content = new VehiculosView();
        }

        private void BtnConductores_Click(object sender, RoutedEventArgs e)
        {
            ContentControlMainView.Content = new ConductoresView();
        }

        private void BtnInfracciones_OnClick(object sender, RoutedEventArgs e)
        {
            ContentControlMainView.Content = new VehiculosView();
        }
    }
}
