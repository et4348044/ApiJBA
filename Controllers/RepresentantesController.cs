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
    [Route("api/representantes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RepresentantesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepresentantesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult<List<Representante>>> Get()
        {
            var representantes = await context.Representantes.AsNoTracking().ToListAsync();
            return Ok(representantes);
        }

        [HttpPost]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult> Post(RepresentanteCreacionDto dto)
        {
            // Validar si ya existe
            var existe = await context.Representantes.AnyAsync(x => x.ci_representante == dto.ci_representante);
            if (existe)
            {
                return BadRequest($"Ya existe un representante registrado con la cédula: {dto.ci_representante}");
            }

            var representante = mapper.Map<Representante>(dto);
            context.Representantes.Add(representante);
            await context.SaveChangesAsync();
            return Ok(representante);
        }
    }
}
