using Application.Interfaces;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class SymbolController : ApiController
    {
        private readonly ISymbolAppService _symbolAppService;

        public SymbolController(ISymbolAppService symbolAppService
            )
        {
            _symbolAppService = symbolAppService;
        }

        [HttpGet("symbol")]
        [Authorize]
        public async Task<IEnumerable<SymbolViewModel>> Get()
        {
            return await _symbolAppService.GetAll();
        }
    }
}
