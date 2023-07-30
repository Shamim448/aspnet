using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Application.Features.Training.Repositories
{
    public interface IAccountRepository
    {
        public void CreateAccount(string username, string password);
    }
}
