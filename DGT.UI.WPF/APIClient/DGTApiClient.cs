using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Configuration;
using DGT.DTOs;
using DGT.UI.WPF.Exceptions;
using DGT.UI.WPF.Properties;


namespace DGT.UI.WPF.APIClient
{
    public class DGTApiClient
    {
        private HttpClient _client;

        public DGTApiClient()
        {
            _client = GetClient();
        }

        private HttpClient GetClient()
        {
            try
            {
                if (_client == null)
                {
                    _client = new HttpClient();
                    string webApiUri = Settings.Default.WebApiAddreess;
                        //ConfigurationManager.AppSettings.Get("WebApiAddreess");
                    _client.BaseAddress = new Uri(webApiUri);
                    _client.DefaultRequestHeaders.Accept.Clear();
                    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                }
                return _client;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        internal async Task<ConductorDTO> CreateConductor(ConductorDTO conductor)
        {
            try
            {
                HttpClient client = GetClient();

                string jfdJson = JsonConvert.SerializeObject(conductor);
                var jfdBuffer = Encoding.UTF8.GetBytes(jfdJson);
                var jfdByteContent = new ByteArrayContent(jfdBuffer);
                jfdByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("api/Conductores", jfdByteContent);
                if (response.IsSuccessStatusCode)
                {
                    var conductorReturn = await response.Content.ReadAsAsync<ConductorDTO>();
                    return conductorReturn;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<bool> RegistrarInfraccion(InfraccionRegistradaDTO ir)
        {
            try
            {
                HttpClient client = GetClient();

                string jfdJson = JsonConvert.SerializeObject(ir);
                var jfdBuffer = Encoding.UTF8.GetBytes(jfdJson);
                var jfdByteContent = new ByteArrayContent(jfdBuffer);
                jfdByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("Api/Infracciones/RegistrarInfraccion", jfdByteContent);
                if (response.IsSuccessStatusCode)
                {
                    
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<bool> CreateInfraccion(InfraccionDTO ir)
        {
            try
            {
                HttpClient client = GetClient();

                string jfdJson = JsonConvert.SerializeObject(ir);
                var jfdBuffer = Encoding.UTF8.GetBytes(jfdJson);
                var jfdByteContent = new ByteArrayContent(jfdBuffer);
                jfdByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("Api/Infracciones", jfdByteContent);
                if (response.IsSuccessStatusCode)
                {

                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal async Task<IEnumerable<MarcaDTO>> GetMarcasModelos()
        {
            try
            {
                HttpClient client = GetClient();
                HttpResponseMessage response = client.GetAsync("api/Vehiculos/Marcas").Result;
                if (response.IsSuccessStatusCode)
                {
                    var marcas = await response.Content.ReadAsAsync<IEnumerable<MarcaDTO>>();
                    return marcas;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<VehiculoDTO>> GetVehiculos()
        {
            try
            {
                HttpClient client = GetClient();
                HttpResponseMessage response = client.GetAsync("api/Vehiculos").Result;
                if (response.IsSuccessStatusCode)
                {
                    var vehiculos = await response.Content.ReadAsAsync<IEnumerable<VehiculoDTO>>();
                    return vehiculos;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<ConductorDTO> GetConductores()
        {
            try
            {
                HttpClient client = GetClient();
                HttpResponseMessage response = client.GetAsync("api/Conductores").Result;
                if (response.IsSuccessStatusCode)
                {
                    var conductores = response.Content.ReadAsAsync<IEnumerable<ConductorDTO>>().Result;
                    return conductores ;
                    ;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public IEnumerable<InfraccionDTO> GetInfracciones()
        {
            try
            {
                HttpClient client = GetClient();
                HttpResponseMessage response = client.GetAsync("api/Infracciones").Result;
                if (response.IsSuccessStatusCode)
                {
                    var infracciones = response.Content.ReadAsAsync<IEnumerable<InfraccionDTO>>().Result;
                    return infracciones;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<VehiculoDTO> CreateVehiculo(VehiculoDTO vehiculo)
        {
            try
            {
                HttpClient client = GetClient();

                string jfdJson = JsonConvert.SerializeObject(vehiculo);
                var jfdBuffer = Encoding.UTF8.GetBytes(jfdJson);
                var jfdByteContent = new ByteArrayContent(jfdBuffer);
                jfdByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("api/Vehiculos", jfdByteContent);
                if (response.IsSuccessStatusCode)
                {
                    var vehiculoReturn = await response.Content.ReadAsAsync<VehiculoDTO>();
                    return vehiculoReturn;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new WebApiException(response.ReasonPhrase);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

   
}
