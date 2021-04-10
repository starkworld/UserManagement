﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Api.Filters
{
    public class DelayFilter : IAsyncActionFilter
    {
        private int _delayInMs;
        public DelayFilter(IConfiguration configuration)
        {
            _delayInMs = configuration.GetValue<int>("ApiDelayDuration", 0);
        }

        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Task.Delay(_delayInMs);
            await next();
        }
    }
}
