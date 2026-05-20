using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiJBA.DTOs;
using ApiJBA.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiJBA.Controllers
{
    [ApiController]
    [Route("api/personal")]
    public class PersonalController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PersonalController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // 1. GET: api/personal - Obtener todo el personal
        [HttpGet]
        public async Task<ActionResult<List<CreacionDePersonal_Get_DTO>>> Get()
        {
            var personalList = await context.Personal.ToListAsync();
            var dto = mapper.Map<List<CreacionDePersonal_Get_DTO>>(personalList);
            return Ok(dto);
        }

        // 1b. GET: api/personal/ids - Obtener solo los IDs del personal creado
        [HttpGet("ids")]
        public async Task<ActionResult<List<PersonalIdDto>>> GetIds()
        {
            var personalList = await context.Personal.ToListAsync();
            var dto = mapper.Map<List<PersonalIdDto>>(personalList);
            return Ok(dto);
        }

        // 2. GET: api/personal/{ci} - Obtener personal por Cédula (PK)
        [HttpGet("{ci}")]
        public async Task<ActionResult<CreacionDePersonal_Get_DTO>> GetByCi(string ci)
        {
            var personal = await context.Personal.FirstOrDefaultAsync(x => x.ci_p == ci);
            if (personal == null)
            {
                return NotFound($"No se encontró personal con la cédula: {ci}");
            }
            var dto = mapper.Map<CreacionDePersonal_Get_DTO>(personal);
            return Ok(dto);
        }

        // 3. POST: api/personal - Registrar un nuevo personal
        [HttpPost]
        public async Task<ActionResult> Post(CreacionDePersonal_Post_DTO creacionDePersonal_Post_DTO)
        {
            // Validar si ya existe un registro con la misma Cédula (PK)
            var existe = await context.Personal.AnyAsync(x => x.ci_p == creacionDePersonal_Post_DTO.ci_p);
            if (existe)
            {
                return BadRequest($"Ya existe un personal registrado con la cédula: {creacionDePersonal_Post_DTO.ci_p}");
            }

            var mapeo = mapper.Map<Personal>(creacionDePersonal_Post_DTO);

            // Asignación automática de cargo según el nivel si no se especificó un cargo personalizado
            if (string.IsNullOrWhiteSpace(mapeo.cargo))
            {
                mapeo.cargo = ObtenerCargoPorNivel(mapeo.nivel);
            }

            context.Add(mapeo);
            await context.SaveChangesAsync();

            var getDto = mapper.Map<CreacionDePersonal_Get_DTO>(mapeo);
            return CreatedAtAction(nameof(GetByCi), new { ci = mapeo.ci_p }, getDto);
        }

        // 4. PUT: api/personal/{ci} - Actualizar datos del personal
        [HttpPut("{ci}")]
        public async Task<ActionResult> Put(string ci, CreacionDePersonal_Get_DTO creacionDePersonal_Get_DTO)
        {
            if (ci != creacionDePersonal_Get_DTO.ci_p)
            {
                return BadRequest("La cédula proporcionada no coincide con el cuerpo de la solicitud.");
            }

            var existe = await context.Personal.AnyAsync(x => x.ci_p == ci);
            if (!existe)
            {
                return NotFound($"La cédula: {ci} no existe.");
            }

            var mapeo = mapper.Map<Personal>(creacionDePersonal_Get_DTO);

            // Asignación automática de cargo según el nivel por seguridad si viene vacío
            if (string.IsNullOrWhiteSpace(mapeo.cargo))
            {
                mapeo.cargo = ObtenerCargoPorNivel(mapeo.nivel);
            }

            context.Update(mapeo);
            await context.SaveChangesAsync();
            return Ok(creacionDePersonal_Get_DTO);
        }

        // 5. POST: api/personal/login - Iniciar sesión validando el nivel de rango (nivel >= 7)
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var personal = await context.Personal.FirstOrDefaultAsync(x => x.ci_p == loginDto.ci_p);
            if (personal == null)
            {
                return NotFound($"No se encontró personal registrado con la cédula: {loginDto.ci_p}");
            }

            // Validar nivel de rango: si es inferior a 7, denegar el acceso al sistema
            if (personal.nivel < 7)
            {
                return BadRequest($"Acceso denegado. Tu nivel de rango ({personal.nivel}) es inferior al mínimo requerido (nivel 7) para utilizar el sistema.");
            }

            var dto = mapper.Map<CreacionDePersonal_Get_DTO>(personal);
            return Ok(new
            {
                mensaje = "Inicio de sesión exitoso. Bienvenido al sistema.",
                usuario = dto
            });
        }

        // Método auxiliar para la asignación automática del cargo por nivel
        private string ObtenerCargoPorNivel(int nivel)
        {
            return nivel switch
            {
                10 => "Sistemas",
                9 => "Director",
                8 => "Subdirector",
                7 => "Secretaria",
                6 => "Vocero",
                _ => "Personal General"
            };
        }
    }
}
