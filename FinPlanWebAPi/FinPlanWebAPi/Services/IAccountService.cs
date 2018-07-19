using threading = System.Threading.Tasks;

namespace FinPlanWebAPi.Services
{
    public interface IAccountService
    {
        threading.Task<string> GetHouseholdAccounts(int HouseholdId);
        threading.Task<string> GetPersonalAccount(int AccountId);
        threading.Task<string> GetTransaction(int Transactionid);
        threading.Task<decimal> GetAccountBalance(int AccountId);
        threading.Task<int> AddTransaction(int accountId, string description, decimal amount, bool trxType, int categoryId, string userId, bool isVoid, decimal recBalance, bool reconciled, bool isDeleted);
    }
}