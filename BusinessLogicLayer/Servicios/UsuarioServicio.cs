using AutoMapper;
using BusinessLogicLayer.Dtos;
using DataAccesssLayer.Entidades;
using DataAccesssLayer.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Servicios
{
    public class UsuarioServicio : IUsuariosServicio
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IMapper _mapper;

        public UsuarioServicio(IUsuariosRepositorio usuariosRepositorio, IMapper mapper)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _mapper = mapper;
        }
        public async Task<CustomResponse<UsuarioDto>> ObtenerUsuarioPorIdAsync(int id)
        {
            var respuesta = new CustomResponse<UsuarioDto>();
            // var = vamos a obtener ese usuario
            //= = aqui es donde voy a cominucarme con mi repositorio de datos
            // await = para que que el repositorio me de la informacion por ID
            var usuario = await _usuariosRepositorio.ObtenerUsuarioPorIdAsync(id);
            var validaciones = validar(usuario);
            if(validaciones.EsError)
            {
                return validaciones;
            }
            
            //Mostrar los datos
            respuesta.Data = _mapper.Map<UsuarioDto>(usuario);

            return respuesta;
        }

        private CustomResponse<UsuarioDto> validar(Usuario usuario)
        {
            var respuesta = new CustomResponse<UsuarioDto>();
            if (usuario == null)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Usuario no encontrado";
                return respuesta;
            }
            if (usuario.Edad < 18)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "Usuario menor de edad";
                return respuesta;
            }
            return respuesta;
        }

        public async Task<CustomResponse<List<UsuarioDto>>> ObtenerUsuariosdAsync()
        {
            var respuesta = new CustomResponse<List<UsuarioDto>>();
            var usuarios = await _usuariosRepositorio.ObtenerUsuariosAsync();
            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);
            respuesta.Data = usuariosDto;
            return respuesta;
        }
    }
}
