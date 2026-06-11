using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiJBA.DTOs;
using ApiJBA.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiJBA.Controllers
{
    [ApiController]
    [Route("api/alumnos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AlumnosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AlumnosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult<List<Alumno>>> Get()
        {
            var alumnos = await context.Alumnos.AsNoTracking().ToListAsync();
            return Ok(alumnos);
        }

        [HttpPost]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult> Post(AlumnoCreacionDto dto)
        {
            // Validar si ya existe el alumno con la misma cédula
            var existe = await context.Alumnos.AnyAsync(x => x.ci_alumno == dto.ci_alumno);
            if (existe)
            {
                return BadRequest($"Ya existe un alumno registrado con la cédula: {dto.ci_alumno}");
            }

            var alumno = mapper.Map<Alumno>(dto);
            context.Alumnos.Add(alumno);
            await context.SaveChangesAsync();
            return Ok(alumno);
        }
    }
}
