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
    public class InfraccionesController : ApiController
    {

        private readonly IInfraccionesService _service;
        private readonly MapperService _mapper;

        public InfraccionesController()
        {
            _service = null;
            _mapper = null;
        }

        public InfraccionesController(IInfraccionesService service,MapperService mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/<controller>
        public IEnumerable<InfraccionDTO> Get()
        {
           var infracciones = _service.GetInfracciones();
            return _mapper.ConvertTo<IEnumerable<InfraccionDTO>, IEnumerable<Infraccion>>(infracciones);
            
        }

        [Route("Api/Infracciones/{dni}")]
        public IEnumerable<InfraccionRegistradaDTO> GetInfraccionesConductor([FromUri] String dni)
        {
            IEnumerable<InfraccionRegistrada> infracciones = _service.GetInfraccionesPorDNI(dni);

            return _mapper.Map< IEnumerable<InfraccionRegistradaDTO>, IEnumerable<InfraccionRegistrada>>(infracciones);
        }

        [Route("Api/Infracciones/TopInfracciones")]
        public IEnumerable<InfraccionDTO> GetTopInfracciones()
        {
            var infracciones = _service.GetTopInfracciones();
            return _mapper.ConvertTo<IEnumerable<InfraccionDTO>, IEnumerable<Infraccion>>(infracciones);

        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]InfraccionDTO inf)
        {
            try
            {
                Infraccion ir = _mapper.ConvertTo<Infraccion, InfraccionDTO>(inf);

                _service.NuevaInfraccion(inf.Identificador,inf.Descripcion,inf.PuntosDescontar);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // POST api/<controller>
        [Route("Api/Infracciones/RegistrarInfraccion")]
        public HttpResponseMessage Post([FromBody]InfraccionRegistradaDTO inf)
        {
            try
            {
                Infraccion ir = _mapper.ConvertTo<Infraccion, InfraccionDTO>(inf.infraccion);

                _service.RegistrarInfraccion(ir, inf.vehiculo.Matricula);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}