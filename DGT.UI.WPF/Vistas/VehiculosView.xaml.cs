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
using DGT.UI.WPF.ViewModels;

namespace DGT.UI.WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para VehiculosView.xaml
    /// </summary>
    public partial class VehiculosView : UserControl
    {
        VehiculosViewModel vm;

        public VehiculosView()
        {
            InitializeComponent();
            vm = new VehiculosViewModel();
            DataContext = vm;
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            vm.Nuevo();
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new RegistrarInfraccion(vm.VehiculoSelected);
            w.Show();
        }
    }
}
