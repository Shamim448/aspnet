using CSEData.Application;
using CSEData.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEData.Infrastructure.Services
{
    public class PriceService:IPriceService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public PriceService(IApplicationUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
    }
}
