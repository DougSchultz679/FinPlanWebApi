using FinPlanWebAPi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using static FinPlanWebAPi.Models.DataModels;

namespace FinPlanWebAPi.Controllers
{
    [RoutePrefix("api/$Planner")]
    public class DataController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // Get methods

        [Route("Accounts")]
        public async Task<List<PersonalAccount>> GetAccountByHouseholdId(int HouseholdId)
        {
            return await db.GetAccountByHouseholdId(HouseholdId);
        }

        [Route("AccountDetail")]
        public async Task<PersonalAccount> GetPersonalAccount(int AccountId)
        {
            return await db.GetAccountDetail(AccountId);
        }

        [Route("BudgetDetail")]
        public async Task<Budget> GetBudgetDetails(int BudgetId)
        {
            return await db.GetBudgetDetails(BudgetId);
        }

        [Route("Budgets")]
        public async Task<List<Budget>> GetBudgets(int HouseholdId)
        {
            return await db.GetBudgets(HouseholdId);
        }

        [Route("Household")]
        public async Task<Household> GetHousehold(int HouseholdId)
        {
            return await db.GetHousehold(HouseholdId);
        }

        [Route("Transaction")]
        public async Task<Transaction> GetTransactionDetail(int TransactionId)
        {
            return await db.GetTransactionDetail(TransactionId);
        }

        [Route("BudgetBalance")]
        public async Task<string> GetBudgetBalance(int BudgetId)
        {
            return await db.GetBudgetBalance(BudgetId);
        }

        [Route("AccountBalance")]
        public async Task<string> GetAccountBalance(int AccountId)
        {
            return await db.GetAccountBalance(AccountId);
        }

        //Post methods
        [Route("AddTransaction")]
        [AcceptVerbs("GET", "POST")]
        public int AddTransaction(int accountId, string description, decimal amount, bool trxType, bool isVoid, int categoryId, string userId, bool reconciled, decimal recBalance, bool isDeleted)
        {
            return db.AddTransaction(accountId, description, amount, trxType, isVoid, categoryId, userId, reconciled, recBalance, isDeleted);
        }

        [Route("AddBudget")]
        [AcceptVerbs("GET", "POST")]
        public int AddBudget(string name, int householdId)
        {
            return db.AddBudget(name, householdId);
        }

        [Route("AddBudgetItem")]
        [AcceptVerbs("GET", "POST")]
        public int AddBudgetItem(int categoryId, int budgetId, decimal amount)
        {
            return db.AddBudgetItem(categoryId, budgetId, amount);
        }

        [Route("AddHousehold")]
        [AcceptVerbs("GET", "POST")]
        public int AddHousehold(string name)
        {
            return db.AddHousehold(name);
        }
    }
}

