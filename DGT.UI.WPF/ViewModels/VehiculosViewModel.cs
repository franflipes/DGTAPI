using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DGT.UI.WPF.APIClient;
using DGT.DTOs;
using System.Windows;
using System.Collections.ObjectModel;
using DGT.UI.WPF.Exceptions;

namespace DGT.UI.WPF.ViewModels
{
    public class VehiculosViewModel:ViewModelBase
    {
        DGTApiClient _webApiClient;

        public VehiculosViewModel()
        {
            _webApiClient = new DGTApiClient();
            GetVehiculos();

        }

        #region Binding Properties
        public ObservableCollection<VehiculoDTO> ListaVehiculos { get; set; }

        private List<MarcaDTO> _listaMarcas;
        public List<MarcaDTO> ListaMarcas
        {
            get { return _listaMarcas; }
            set
            {
                _listaMarcas = value;
                RaisePropertyChanged(() => ListaMarcas);
            }
        }


        private List<ModeloDTO> _listaModelos;
        public List<ModeloDTO> ListaModelos
        {
            get { return _listaModelos; }
            set
            {
                _listaModelos = value;
                RaisePropertyChanged(() => ListaModelos);
            }
        }

        

        private VehiculoDTO _vehiculoSelected;
        public VehiculoDTO VehiculoSelected
        {
            get { return _vehiculoSelected; }
            set
            {
                _vehiculoSelected = value;
                GetMarcasModelos();
                RaisePropertyChanged(() => VehiculoSelected);
            }
        }

        private MarcaDTO _marcaSelected;
        public MarcaDTO MarcaSelected
        {
            get { return _marcaSelected; }
            set
            {
                _marcaSelected = value;
                VehiculoSelected.Marca = _marcaSelected.NombreMarca;
                GetModelos();
                RaisePropertyChanged(() => MarcaSelected);
            }
        }

        private ModeloDTO _modeloSelected;
        public ModeloDTO ModeloSelected
        {
            get { return _modeloSelected; }
            set
            {
                _modeloSelected = value;
                VehiculoSelected.Modelo = _modeloSelected.NombreModelo;
                RaisePropertyChanged(() => ModeloSelected);
            }
        }

        private void GetModelos()
        {
            if (_marcaSelected != null)
            {
                ListaModelos = MarcaSelected.Modelos.ToList();
                ModeloSelected=ListaModelos.Where(x => x.NombreModelo == VehiculoSelected.Modelo).Single();
            }
        }

        #endregion


        #region Methods
        internal async void Nuevo()
        {
            //if (ListaVehiculos.Exists(x => x.Matricula == VehiculoSelected.Matricula))
            //{
            //    MessageBox.Show("Coche existente");
            //}
            //else
            //{
            try
            {
                VehiculoDTO v = await _webApiClient.CreateVehiculo(VehiculoSelected);
                //Si el coche devuelto !=null es porque se creo y lo añadimos a la collection observable
                if (v != null)
                    ListaVehiculos.Add(v);
                else
                    MessageBox.Show("Coche existente");
            }
            catch (WebApiException e)
            {
                MessageBox.Show(e.Message);
            }
            


        }

        private async void GetMarcasModelos()
        {

            var marcas =await _webApiClient.GetMarcasModelos();
            ListaMarcas = marcas.ToList();
            MarcaSelected = ListaMarcas.Where(x => x.NombreMarca == VehiculoSelected.Marca).Single();
        }

        private async void GetVehiculos()
        {
            var vehiculos = await _webApiClient.GetVehiculos();
            ListaVehiculos = new ObservableCollection<VehiculoDTO>( vehiculos.ToList());
        }
        #endregion
    }
}
