﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Application.Services
{
    public interface IWebScraperService
    {
        Task LoadAsunc(string url);
    }
}
