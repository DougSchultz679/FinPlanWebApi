using FinPlanWebAPi.Models;
using FinPlanWebAPi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using static FinPlanWebAPi.Models.DataModels;

namespace FinPlanWebAPi.Controllers
{
    [RoutePrefix("api/$Planner")]
    public class DataController : ApiController
    {
        private readonly IAccountService _accService;
        private readonly IBudgetService _budgetService;
        private readonly IHouseholdService _hhService;
        private readonly ISecurityService _securityService;
        private readonly ILoggingService _logService;

        //DI in action
        public DataController(IAccountService AccService, IBudgetService BudgetService, IHouseholdService HouseholdService, ISecurityService SecurityService, ILoggingService LoggingService)
        {
            _accService = AccService ?? throw new ArgumentNullException(nameof(AccService));
            _budgetService = BudgetService ?? throw new ArgumentNullException(nameof(BudgetService));
            _hhService = HouseholdService ?? throw new ArgumentNullException(nameof(HouseholdService));
            _securityService = SecurityService ?? throw new ArgumentNullException(nameof(SecurityService));
            _logService = LoggingService ?? throw new ArgumentNullException(nameof(LoggingService));
        }

        /// <summary>
        /// Returns all data for a given account in Json format.
        /// </summary>
        /// <param name="AccountId">The primary key for the given account.</param>
        /// <returns></returns>
        [Route("Account")]
        public async Task<IHttpActionResult> GetPersonalAccount(int AccountId)
        {
            try
            {
                return Ok(await _accService.GetPersonalAccount(AccountId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Returns data in Json format for all accounts in a given household.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Accounts")]
        public async Task<IHttpActionResult> GetHouseholdAccounts(int HouseholdId)
        {
            try
            {
                return Ok(await _accService.GetHouseholdAccounts(HouseholdId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Return the sum of all transactions that have taken place for a given account, counting debits as negative and credits as positive.
        /// </summary>
        /// <param name="AccountId">The primary key for the given account.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("AccountBalance")]
        public async Task<IHttpActionResult> GetAccountBalance(int AccountId)
        {
            try
            {
                return Ok(await _accService.GetAccountBalance(AccountId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Return all data for a given transaction in Json format.
        /// </summary>
        /// <param name="TransactionId">The primary key for the given transaction.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Transaction")]
        public async Task<IHttpActionResult> GetTransactionDetail(int TransactionId)
        {
            try
            {
                return Ok(await _accService.GetTransaction(TransactionId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
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
        [Route("Transaction")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddTransaction(int accountId, string description, decimal amount, bool trxType, bool isVoid, int categoryId, string userId, bool reconciled, decimal recBalance, bool isDeleted)
        {
            try
            {
                return Ok(await _accService.AddTransaction(accountId, description, amount, trxType, categoryId, userId, isVoid, recBalance, reconciled, isDeleted));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Return all data for a given budget in Json format.
        /// </summary>
        /// <param name="BudgetId">The primary key for the given budget.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Budget")]
        public async Task<IHttpActionResult> GetBudget(int BudgetId)
        {
            try
            {
                return Ok(await _budgetService.GetBudgetDetails(BudgetId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a new budget for a given household using the provided name.
        /// </summary>
        /// <param name="name">String value of household name.</param>
        /// <param name="householdId">The primary key for the given household.</param>
        /// <returns></returns>
        [Route("Budget")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddBudget(string name, int householdId)
        {
            try
            {
                return Ok(await _budgetService.AddBudget(name, householdId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Return data for all budgets for a given household in json format.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Budgets")]
        public async Task<IHttpActionResult> GetBudgets(int HouseholdId)
        {
            try
            {
                return Ok(await _budgetService.GetBudgets(HouseholdId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a new budget item for a given budget, including fields for the budget category and the amount.
        /// </summary>
        /// <param name="categoryId">Foreign key for the category that will be entered for the new budget item.</param>
        /// <param name="budgetId">The primary key for the given budget under which this budget item will live.</param>
        /// <param name="amount">Decimal value for the budget album, which is considered positive and is an expense.</param>
        /// <returns></returns>
        [Route("BudgetItem")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddBudgetItem(int categoryId, int budgetId, decimal amount)
        {
            try
            {
                return Ok(await _budgetService.AddBudgetItem(categoryId, budgetId, amount));

            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Return the sum of all budget items for a given budget.
        /// </summary>
        /// <param name="BudgetId">The primary key for the given budget.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("BudgetBalance")]
        public async Task<IHttpActionResult> GetBudgetBalance(int BudgetId)
        {
            try
            {
                return Ok(await _budgetService.GetBudgetBalance(BudgetId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Return data for a given household object.
        /// </summary>
        /// <param name="HouseholdId">The primary key for the given household.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Household")]
        public async Task<IHttpActionResult> GetHousehold(int HouseholdId)
        {
            try
            {
                return Ok(await _hhService.GetHousehold(HouseholdId));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a new household with the given name. 
        /// </summary>
        /// <param name="name">Name for the new household.</param>
        /// <returns></returns>
        [Route("Household")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddHousehold(string name)
        {
            try
            {
                return Ok(await _hhService.AddHousehold(name));
            } catch (Exception ex)
            {
                _logService.AddErrorWinEventLog(ex);
                _securityService.HideErrorTime();

                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }
}

