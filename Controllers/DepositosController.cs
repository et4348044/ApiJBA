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
    [Route("api/depositos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepositosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DepositosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult<List<Deposito>>> Get()
        {
            var depositos = await context.Depositos.AsNoTracking().ToListAsync();
            return Ok(depositos);
        }

        [HttpPost]
        [Authorize(Policy = "NivelOperativo")]
        public async Task<ActionResult> Post(DepositoCreacionDto dto)
        {
            var deposito = mapper.Map<Deposito>(dto);
            context.Depositos.Add(deposito);
            await context.SaveChangesAsync();
            return Ok(deposito);
        }
    }
}
