using ApiAeropuerto.Data;
using ApiAeropuerto.Models;
using ApiAeropuerto.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAeropuerto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        private readonly sistem21_equipo9AeropuertoContext context;

        Repository<Vuelos> vuelosRepository;

        public VuelosController(sistem21_equipo9AeropuertoContext context)
        {
            this.context = context;
            vuelosRepository = new Repository<Vuelos>(context);
        }

        [HttpGet]
        public IActionResult Get() //Ordenados por fecha
        {
            var vuelos = vuelosRepository.Get().OrderBy(v => v.HorarioSalida);
            return Ok(vuelos);
        }

        [HttpGet("id:int")]
        public IActionResult Get(int id)
        {
            var vuelo = vuelosRepository.Get(id);
            return Ok(vuelo);
        }

        [HttpPost]
        public IActionResult Post(Vuelos vuelo)
        {
            if (vuelo == null)
            {
                return BadRequest("Debe proporcionar un vuelo.");
            }

            if (Validate(vuelo, out List<string> errores))
            {
                vuelosRepository.Insert(vuelo);
                return Ok();
            }
            else
            {
                return BadRequest(errores);
            }
        }

        [HttpPut]
        public IActionResult Put(Vuelos vuelo)
        {
            if (vuelo == null)
            {
                return BadRequest("No se indico el vuelo a editar.");
            }

            if (Validate(vuelo, out List<string> errores))
            {
                var entidad =vuelosRepository.Get(vuelo.Id);

                if (entidad == null)
                {
                    return NotFound("Ese vuelo no existe");
                } 
               
                entidad.Estado = vuelo.Estado;
                entidad.Destino = vuelo.Destino;
                entidad.CodigoVuelo = vuelo.CodigoVuelo;
                entidad.PuertaSalida = vuelo.PuertaSalida;
                entidad.HorarioSalida = vuelo.HorarioSalida;
                vuelosRepository.Update(entidad);
            }
            else
            {
                return BadRequest(errores);
            }

           

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entidad = vuelosRepository.Get(id);
            if (entidad == null)
            {
                return NotFound();
            }

            vuelosRepository.Delete(entidad);
            return Ok();
        }
        private bool Validate(Vuelos vuelo, out List<string> errores)
        {
            errores = new List<string>();

            if (vuelosRepository.Get().Any(v=>v.CodigoVuelo==vuelo.CodigoVuelo && v.Id!=vuelo.Id))
            {
                errores.Add("Ese vuelo ya ha sido registrado");
            }

            if (vuelo.CodigoVuelo.Length>6)
            {
                errores.Add("El código tiene como máximo 6 caracteres. ");
            }

            if (string.IsNullOrWhiteSpace(vuelo.CodigoVuelo))
            {
                errores.Add("Especifique el codigo del vuelo.");
            }

            if (vuelo.Destino.Length > 25)
            {
                errores.Add("El destino tiene como máximo 25 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(vuelo.Destino))
            {
                errores.Add("Especifique el destino del vuelo.");
            }

            if (string.IsNullOrWhiteSpace(vuelo.PuertaSalida))
            {
                errores.Add("Especifique la puerta de salida del vuelo.");
            }

            if (vuelo.HorarioSalida<=DateTime.Now.AddMinutes(4))
            {
                errores.Add("Se debe planear un vuelo con minimo 4 minutos de anticipacion");
            }

            return errores.Count == 0;
        }

    }
}
