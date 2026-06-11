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
    [Route("api/matriculas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MatriculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MatriculasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult<List<Matricula>>> Get()
        {
            var matriculas = await context.Matriculas.AsNoTracking().ToListAsync();
            return Ok(matriculas);
        }

        [HttpPost]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult> Post(MatriculaCreacionDto dto)
        {
            // Validar personal
            var existePersonal = await context.Personal.AnyAsync(x => x.ci_p == dto.ci_p);
            if (!existePersonal)
            {
                return BadRequest($"El personal con cédula {dto.ci_p} no existe.");
            }

            var matricula = mapper.Map<Matricula>(dto);
            context.Matriculas.Add(matricula);
            await context.SaveChangesAsync();
            return Ok(matricula);
        }
    }
}
