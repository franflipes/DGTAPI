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
    /// Lógica de interacción para ConductoresView.xaml
    /// </summary>
    public partial class ConductoresView : UserControl
    {
        ConductoresViewModel vm;
        public ConductoresView()
        {
            InitializeComponent();
            vm = new ConductoresViewModel();
            DataContext = vm;
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            vm.Nuevo();
        }
    }
}
