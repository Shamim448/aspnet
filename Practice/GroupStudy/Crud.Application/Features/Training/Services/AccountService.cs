using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crud.Application.Features.Training.Repositories;

namespace Crud.Application.Features.Training.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        public AccountService(IAccountRepository accountRepository, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
        }
        public void CreateAccount(string username, string password)
        {
            if(username == null || password == null)
                throw new InvalidDataException();
             
            if(username.Length > 30)
                username = username.Substring(0, 30);
            //code to create account
            _accountRepository.CreateAccount(username, password);
            _emailService.SendAccountCreationEmail(username);
        }
    }
}
