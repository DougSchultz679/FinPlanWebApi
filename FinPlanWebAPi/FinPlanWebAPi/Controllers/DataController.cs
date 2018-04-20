using FinPlanWebAPi.Models;
using Newtonsoft.Json;
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
        /// <summary>
        /// Returns data for all accounts in a given household.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("Accounts")]
        public async Task<List<PersonalAccount>> GetAccountByHouseholdId(int HouseholdId)
        {
            return await db.GetAccountByHouseholdId(HouseholdId);
        }

        /// <summary>
        /// Returns data in Json format for all accounts in a given household.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("AccountsJson")]
        public async Task<IHttpActionResult> GetJsonAccountsByHouseholdId(int HouseholdId)
        {
            var json = JsonConvert.SerializeObject(await db.GetAccountByHouseholdId(HouseholdId));
            return Ok(json);
        }

        /// <summary>
        /// Returns all data for a given account.
        /// </summary>
        /// <param name="AccountId">The primary key for the given account.</param>
        /// <returns></returns>
        [Route("AccountDetail")]
        public async Task<PersonalAccount> GetPersonalAccount(int AccountId)
        {
            return await db.GetAccountDetail(AccountId);
        }


        /// <summary>
        /// Returns all data for a given account in Json format.
        /// </summary>
        /// <param name="AccountId">The primary key for the given account.</param>
        /// <returns></returns>
        [Route("AccountDetailJson")]
        public async Task<IHttpActionResult> GetJsonPersonalAccount(int AccountId)
        {
            string json = JsonConvert.SerializeObject(await db.GetAccountDetail(AccountId));
            return Ok(json);
        }

        /// <summary>
        /// Return all data for a given budget.
        /// </summary>
        /// <param name="BudgetId">The primary key for the given budget.</param>
        /// <returns></returns>
        [Route("BudgetDetail")]
        public async Task<Budget> GetBudgetDetails(int BudgetId)
        {
            return await db.GetBudgetDetails(BudgetId);
        }

        /// <summary>
        /// Return all data for a given budget in Json format.
        /// </summary>
        /// <param name="BudgetId">The primary key for the given budget.</param>
        /// <returns></returns>
        [Route("BudgetDetailJson")]
        public async Task<IHttpActionResult> GetJsonBudgetDetails(int BudgetId)
        {
            string json = JsonConvert.SerializeObject(await db.GetBudgetDetails(BudgetId));
            return Ok(json);
        }

        /// <summary>
        /// Return data for all budgets for a given household.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("Budgets")]
        public async Task<List<Budget>> GetBudgets(int HouseholdId)
        {
            return await db.GetBudgets(HouseholdId);
        }

        /// <summary>
        /// Return data for all budgets for a given household in json format.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("BudgetsJson")]
        public async Task<IHttpActionResult> GetJsonBudgets(int HouseholdId)
        {
            string j = JsonConvert.SerializeObject(await db.GetBudgets(HouseholdId));
            return Ok(j);
        }

        /// <summary>
        /// Return data for a given household object.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("Household")]
        public async Task<Household> GetHousehold(int HouseholdId)
        {
            return await db.GetHousehold(HouseholdId);
        }

        /// <summary>
        /// Return all data for a given transaction.
        /// </summary>
        /// <param name="TransactionId">The primary key for the given transaction.</param>
        /// <returns></returns>
        [Route("Transaction")]
        public async Task<Transaction> GetTransactionDetail(int TransactionId)
        {
            return await db.GetTransactionDetail(TransactionId);
        }

        /// <summary>
        /// Return all data for a given transaction in Json format.
        /// </summary>
        /// <param name="TransactionId">The primary key for the given transaction.</param>
        /// <returns></returns>
        [Route("TransactionJson")]
        public async Task<IHttpActionResult> GetJsonTransactionDetail(int TransactionId)
        {
            var j = JsonConvert.SerializeObject(await db.GetTransactionDetail(TransactionId));
            return Ok(j);
        }

        /// <summary>
        /// Return the sum of all budget items for a given budget.
        /// </summary>
        /// <param name="BudgetId">The primary key for the given budget.</param>
        /// <returns></returns>
        [Route("BudgetBalance")]
        public async Task<decimal> GetBudgetBalance(int BudgetId)
        {
            return await db.GetBudgetBalance(BudgetId);
        }

        /// <summary>
        /// Return the sum of all transactions that have taken place for a given account, counting debits as negative and credits as positive.
        /// </summary>
        /// <param name="AccountId">The primary key for the given account.</param>
        /// <returns></returns>
        [Route("AccountBalance")]
        public async Task<decimal> GetAccountBalance(int AccountId)
        {
            return await db.GetAccountBalance(AccountId);
        }

        /// <summary>
        /// Add a new transaction with the properties you have provided.
        /// </summary>
        /// <param name="accountId">The primary key for the given account.</param>
        /// <param name="description">A description of your transaction.</param>
        /// <param name="amount">Decimal quantity for transaction.</param>
        /// <param name="trxType">Boolean value specifying credit(false)/debit(true).</param>
        /// <param name="isVoid">Boolean value specifiying voided(true)/not voided(false).</param>
        /// <param name="categoryId">Integer corresponding to the data type.</param>
        /// <param name="userId">String GUID corresponding to user Id.</param>
        /// <param name="reconciled">Boolean value specifying reconciled(true)/not reconciled (false)</param>
        /// <param name="recBalance">Decimal value showing the amount recorded as reconciled.</param>
        /// <param name="isDeleted">Boolean value showing whether the transaction has been soft deleted.</param>
        /// <returns></returns>
        //Post methods
        [Route("AddTransaction")]
        [AcceptVerbs("GET", "POST")]
        public async Task<int> AddTransaction(int accountId, string description, decimal amount, bool trxType, bool isVoid, int categoryId, string userId, bool reconciled, decimal recBalance, bool isDeleted)
        {
             return await db.AddTransaction(accountId, description, amount, trxType, isVoid, categoryId, userId, reconciled, recBalance, isDeleted);
        }

        /// <summary>
        /// Create a new budget for a given household using the provided name.
        /// </summary>
        /// <param name="name">String value of household name.</param>
        /// <param name="householdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("AddBudget")]
        [AcceptVerbs("GET", "POST")]
        public async Task<int> AddBudget(string name, int householdId)
        {
            return await db.AddBudget(name, householdId);
        }

        /// <summary>
        /// Create a new budget item for a given budget, including fields for the budget category and the amount.
        /// </summary>
        /// <param name="categoryId">Foreign key for the category that will be entered for the new budget item.</param>
        /// <param name="budgetId">The primary key for the given budget under which this budget item will live.</param>
        /// <param name="amount">Decimal value for the budget album, which is considered positive and is an expense.</param>
        /// <returns></returns>
        [Route("AddBudgetItem")]
        [AcceptVerbs("GET", "POST")]
        public async Task<int> AddBudgetItem(int categoryId, int budgetId, decimal amount)
        {
            return await db.AddBudgetItem(categoryId, budgetId, amount);
        }

        /// <summary>
        /// Create a new household with the given name. 
        /// </summary>
        /// <param name="name">Name for the new household.</param>
        /// <returns></returns>
        [Route("AddHousehold")]
        [AcceptVerbs("GET", "POST")]
        public async Task<int> AddHousehold(string name)
        {
            return await db.AddHousehold(name);
        }
    }
}

