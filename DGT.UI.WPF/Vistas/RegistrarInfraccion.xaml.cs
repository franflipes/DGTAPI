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
using System.Windows.Shapes;
using DGT.DTOs;
using DGT.UI.WPF.APIClient;

namespace DGT.UI.WPF.Vistas
{
    /// <summary>
    /// Lógica de interacción para RegistrarInfraccion.xaml
    /// </summary>
    public partial class RegistrarInfraccion : Window
    {
        DGTApiClient _webApiClient;
        private VehiculoDTO _v;

        public RegistrarInfraccion(VehiculoDTO v)
        {
            InitializeComponent();

            _v = v;
            _webApiClient= new DGTApiClient(); ;
            labelConductor.Content = v.Dni;
            labelMatricula.Content = v.Matricula;
            comboInfraccion.ItemsSource = _webApiClient.GetInfracciones();
            comboInfraccion.DisplayMemberPath = "Descripcion";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InfraccionRegistradaDTO ir = new InfraccionRegistradaDTO() { infraccion = (InfraccionDTO)comboInfraccion.SelectedItem, vehiculo = _v };
                if (await _webApiClient.RegistrarInfraccion(ir))
                {
                    MessageBox.Show("Infraccion Registrada");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
