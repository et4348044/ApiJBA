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
    [Route("api/traslados")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrasladosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TrasladosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult<List<Traslado>>> Get()
        {
            var traslados = await context.Traslados.AsNoTracking().ToListAsync();
            return Ok(traslados);
        }

        [HttpPost]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult> Post(TrasladoCreacionDto dto)
        {
            // Validar dependencias
            var existeOrigen = await context.Depositos.AnyAsync(x => x.id_deposito == dto.id_dep_origen);
            if (!existeOrigen)
            {
                return BadRequest($"El depósito de origen con ID {dto.id_dep_origen} no existe.");
            }

            var existeDestino = await context.Depositos.AnyAsync(x => x.id_deposito == dto.id_dep_destino);
            if (!existeDestino)
            {
                return BadRequest($"El depósito de destino con ID {dto.id_dep_destino} no existe.");
            }

            var existeProducto = await context.Productos.AnyAsync(x => x.id_producto == dto.id_producto);
            if (!existeProducto)
            {
                return BadRequest($"El producto con ID {dto.id_producto} no existe.");
            }

            var existePersonal = await context.Personal.AnyAsync(x => x.ci_p == dto.ci_p);
            if (!existePersonal)
            {
                return BadRequest($"El personal con cédula {dto.ci_p} no existe.");
            }

            var traslado = mapper.Map<Traslado>(dto);
            context.Traslados.Add(traslado);
            await context.SaveChangesAsync();
            return Ok(traslado);
        }
    }
}
