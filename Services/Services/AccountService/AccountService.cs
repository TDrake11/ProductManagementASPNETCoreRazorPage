using PRN222.lab2.Repositories.Data;
using PRN222.lab2.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Lab2.Services.Services.AccountService
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork _unitOfWork;

		public AccountService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<AccountMember?> GetAccountMember(string email)
		{
			var accountMember = await _unitOfWork.Repository<AccountMember>()
				.GetListAsync(
					filter: x => x.EmailAddress == email
				);
			return accountMember.FirstOrDefault();
		}
	}
}
