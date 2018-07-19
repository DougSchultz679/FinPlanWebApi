using FinPlanWebAPi.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FinPlanWebAPi.Services
{
    public class AccountService : IAccountService
    {

        private readonly ApplicationDbContext _dbContext;

        public AccountService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetHouseholdAccounts(int HouseholdId)
        {
            string json = JsonConvert.SerializeObject(await _dbContext.GetAccountByHouseholdId(HouseholdId));
            return json;
        }

        public async Task<string> GetPersonalAccount(int AccountId)
        {
            string json = JsonConvert.SerializeObject(await _dbContext.GetAccountDetail(AccountId));
            return json;
        }

        public async Task<string> GetTransaction(int TransactionId)
        {
            string json = JsonConvert.SerializeObject(await _dbContext.GetTransactionDetail(TransactionId));
            return json;
        }

        public async Task<decimal> GetAccountBalance(int AccountId)
        {
            return await _dbContext.GetAccountBalance(AccountId);
        }

        public async Task<int> AddTransaction(int accountId, string description, decimal amount, bool trxType,  int categoryId, string userId, bool isVoid=false, decimal recBalance = 0.0m, bool reconciled = false, bool isDeleted = false)
        {
            return await _dbContext.AddTransaction(accountId, description, amount, trxType, isVoid, categoryId, userId, reconciled, recBalance, isDeleted);
        }

    }
}