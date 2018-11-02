using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DGT.Entities;
using DGT.DTOs;
using DGT.Services;

namespace DGTWebApi.AutoMapper
{
    //public interface IMapperService{}
    public class MapperService
    {
        public MapperService()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                IVehiculosService _vs = new VehiculosService();

                //origen,destino
                cfg.CreateMap<Vehiculo, VehiculoDTO>().ForMember(dest => dest.Dni,
               opts => opts.MapFrom(src => src.ConductoresHabituales.FirstOrDefault().DNI));

                cfg.CreateMap<VehiculoDTO, Vehiculo>();

                cfg.CreateMap<Conductor, ConductorDTO>();
                cfg.CreateMap<ConductorDTO, Conductor>();

                cfg.CreateMap<Infraccion, InfraccionDTO>();
                cfg.CreateMap<InfraccionDTO, Infraccion>();

                cfg.CreateMap<InfraccionRegistrada, InfraccionRegistradaDTO>().ForMember(dest => dest.vehiculo,
                    opts => opts.MapFrom(src => _vs.GetVehiculos((v) => v.Matricula == src.Matricula)));


                cfg.CreateMap<Marca, MarcaDTO>();
                cfg.CreateMap<MarcaDTO, Marca>();

                cfg.CreateMap<Modelo, ModeloDTO>();
                cfg.CreateMap<ModeloDTO, Modelo>();

            });
            
            ServiceMapper = Configuration.CreateMapper();
            
            
        }

        #region private mapping interface
        
        private MapperConfiguration Configuration { get; set; }

        private IMapper ServiceMapper { get; set; }

        #endregion





        public TDestiny Map<TDestiny, TSource>(TSource sourceEntities)
        {
            return ServiceMapper.Map<TDestiny>(sourceEntities);
        }

        #region Mapping and cloning methods

        /// <summary>
        ///     Performs conversion from one type of entity into another
        /// </summary>
        /// <param name="sourceEntities"></param>
        /// <returns></returns>
        public IEnumerable<TDestiny> ConvertTo<TDestiny, TSource>(IEnumerable<TSource> sourceEntities)
            where TDestiny : class
            where TSource : class
        {
            if (sourceEntities == null) return null;
            try
            {
                if (Configuration.FindTypeMapFor<TSource, TDestiny>() == null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                    IMapper customMApper = config.CreateMapper();
                    return sourceEntities.Select(customMApper.Map<TSource, TDestiny>);
                }
                return sourceEntities.Select(ServiceMapper.Map<TSource, TDestiny>);
            }
            catch (Exception e)
            {
                throw new Exception("Error while converting using automapper", e);
            }
        }


        /// <summary>
        ///     Performs conversion from one type of entity into another
        /// </summary>
        /// <param name="sourceEntity"></param>
        /// <returns></returns>
        public TDestiny ConvertTo<TDestiny, TSource>(TSource sourceEntity)
            where TDestiny : class
            where TSource : class
        {
            if (sourceEntity == null) return default(TDestiny);
            try
            {
                if (Configuration.FindTypeMapFor<TSource, TDestiny>() == null)
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMissingTypeMaps = true);
                    IMapper customMApper = config.CreateMapper();
                    return customMApper.Map<TSource, TDestiny>(sourceEntity);
                }
                return ServiceMapper.Map<TSource, TDestiny>(sourceEntity);
            }
            catch (Exception e)
            {
                throw new Exception("Error while converting using automapper", e);
            }
        }

        /// <summary>
        /// Clones an entity
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="sourceEntity">The source entity.</param>
        /// <returns></returns>
        public TSource CloneTo<TSource>(TSource sourceEntity)
            where TSource : class
        {
            return ConvertTo<TSource, TSource>(sourceEntity);
        }
        #endregion
    }
}