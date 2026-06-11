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
    [Route("api/stockdepositos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StockDepositosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public StockDepositosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult<List<StockDeposito>>> Get()
        {
            var stock = await context.StockDepositos.AsNoTracking().ToListAsync();
            return Ok(stock);
        }

        [HttpPost]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult> Post(StockDepositoCreacionDto dto)
        {
            // Validar dependencias
            var existeDeposito = await context.Depositos.AnyAsync(x => x.id_deposito == dto.id_deposito);
            if (!existeDeposito)
            {
                return BadRequest($"El depósito con ID {dto.id_deposito} no existe.");
            }

            var existeProducto = await context.Productos.AnyAsync(x => x.id_producto == dto.id_producto);
            if (!existeProducto)
            {
                return BadRequest($"El producto con ID {dto.id_producto} no existe.");
            }

            var stock = mapper.Map<StockDeposito>(dto);
            context.StockDepositos.Add(stock);
            await context.SaveChangesAsync();
            return Ok(stock);
        }
    }
}
