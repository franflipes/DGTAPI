using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DGT.Services;
using DGT.Entities;
using DGT.DTOs;
using DGTWebApi.AutoMapper;

namespace DGTWebApi.Controllers
{
    public class VehiculosController : ApiController
    {

        private readonly IVehiculosService _service;
        private readonly MapperService _mapper;

        public VehiculosController()
        {
            _service = null;
            _mapper = null;
        }

        public VehiculosController(IVehiculosService service, MapperService mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public IEnumerable<VehiculoDTO> Get()
        {
            try
            {
                var vehiculos = _service.GetVehiculos().AsEnumerable();

                //return _mapper.ConvertTo< IEnumerable<VehiculoDTO>, IEnumerable<Vehiculo>>(vehiculos);
                return _mapper.Map<IEnumerable<VehiculoDTO>, IEnumerable<Vehiculo>>(vehiculos);
            }
            catch (Exception e)
            {
                throw e;
            }
            

        }

        // GET api/<controller>/5
        [Route("Api/Vehiculos/{matricula}")]
        public VehiculoDTO GetVehiculo([FromUri] String matricula)
        {
            try
            {
                Vehiculo vehiculo = _service.GetVehiculos((v) => v.Matricula.Equals(matricula));
                
                return _mapper.ConvertTo<VehiculoDTO, Vehiculo>(vehiculo);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // GET api/<controller>/5
        [Route("Api/Vehiculos/Marcas")]
        public IEnumerable<MarcaDTO> GetMarcas()
        {
            try
            {
                var marcas = _service.GetMarcas();
                return _mapper.ConvertTo<MarcaDTO, Marca>(marcas);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]VehiculoDTO vehiculo)
        {
            try
            {
               var v = _service.CrearVehiculo(vehiculo.Matricula,vehiculo.Marca,vehiculo.Modelo,vehiculo.Dni);
                return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<VehiculoDTO, Vehiculo>(v));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,e.Message);
            }

        }
    }
}