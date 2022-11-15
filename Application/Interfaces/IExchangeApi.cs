﻿using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IExchangeApi
    {
        Task<RateQuery> Convert(string from, string to, float amount);
    }
}
