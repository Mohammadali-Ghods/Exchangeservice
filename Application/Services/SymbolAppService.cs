using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SymbolAppService : ISymbolAppService
    {
        private readonly IMapper _mapper;
        private ISymbolRepository _symbolRepository;
        private ICacheManagement<Symbol> _cacheManagement;
        private ILogger<SymbolAppService> _logger;
        public SymbolAppService(IMapper mapper,
                                 ISymbolRepository symbolRepository,
                                 ICacheManagement<Symbol> cacheManagement,
                                 ILogger<SymbolAppService> logger)
        {
            _mapper = mapper;
            _symbolRepository = symbolRepository;
            _cacheManagement = cacheManagement;
            _logger = logger;
        }
        public async Task<IEnumerable<SymbolViewModel>> GetAll()
        {
            var symbols = await _cacheManagement.GetListCache("Symbols");
            if (symbols == null)
            {
                _logger.Log(LogLevel.Warning, "No caching found for symbols");
                symbols = _symbolRepository.GetAll();
                _cacheManagement.SetListCache("Symbols", symbols.ToList(), 120);
            }
            return _mapper.Map<List<SymbolViewModel>>(symbols);
        }
    }
}
