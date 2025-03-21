using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Services.AccountService
{
	public interface IAccountService
	{
		Task<AccountMember?> GetAccountMember(string email);
	}
}
