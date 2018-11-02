using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DGT.Entities;
using DGT.DTOs;
using DGT.Services;
using DGTWebApi.AutoMapper;

namespace DGTWebApi.Controllers
{
    public class ConductoresController : ApiController
    {
        private readonly IConductoresService _service;
        private readonly MapperService _mapper;

        public ConductoresController()
        {
            _service = null;
            _mapper = null;
        }

        public ConductoresController(IConductoresService service, MapperService mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/<controller>
        public IEnumerable<ConductorDTO> Get()
        {
            var conductores = _service.GetConductores().AsEnumerable();

            return _mapper.ConvertTo<IEnumerable<ConductorDTO>, IEnumerable<Conductor>>(conductores);
        }

        // GET api/<controller>/5
        [Route("Api/Conductores/{dni}")]
        public ConductorDTO GetConductor([FromUri] String dni)
        {
            Conductor conductor = _service.GetConductores((c) => c.DNI.Equals(dni));
            return _mapper.ConvertTo<ConductorDTO, Conductor>(conductor);
        }

        [Route("Api/Conductores/Top/{n}")]
        public IEnumerable<ConductorDTO> GetTopConductores([FromUri] int n)
        {
            IEnumerable<Conductor> conductores = _service.GetTopConductores(n);
            return _mapper.ConvertTo<IEnumerable<ConductorDTO>, IEnumerable<Conductor>>(conductores);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]ConductorDTO  conductor)
        {
            try
            {
                Conductor c =_service.CreateConductor(conductor.DNI, conductor.Nombre, conductor.Apellidos);
                return Request.CreateResponse(HttpStatusCode.OK,_mapper.Map<ConductorDTO,Conductor>(c));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            
        }
        
    }
}