using Application.Interfaces;
using Application.ViewModel;
using ExternalApi.ConfigurationModel;
using ExternalApi.HttpModule;
using ExternalApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExternalApi.Api
{
    public class ExchangeApi : IExchangeApi
    {
        #region Fields
        private readonly ExretnalApiModel _exretnalApiModel;
        private ILogger<ExchangeApi> _logger;
        #endregion

        #region Ctor
        public ExchangeApi(IOptionsMonitor<ExretnalApiModel> optionsMonitor,
             ILogger<ExchangeApi> logger)
        {
            _exretnalApiModel = optionsMonitor.CurrentValue;
            _logger = logger;
        }
        #endregion

        public async Task<RateQuery> Convert(string from, string to, float amount)
        {
            try
            {
                var result = await BaseHttp.Get<Exchange>
             (
             new Dictionary<string, string>() { { "apikey", _exretnalApiModel.Token } }
             , _exretnalApiModel.ExchangeApiUrl + $"/convert?to={to}&from={from}&amount={amount}");

                if (result != null)
                    _logger.Log(LogLevel.Information, "Getting data from exchange API done successfully");

                return new RateQuery() { Rate = result.info.rate, FinalValue = result.result };
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                throw;
            }
        }
    }
}
