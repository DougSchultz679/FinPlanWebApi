using FinPlanWebAPi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FinPlanWebAPi.Services
{
    public class BudgetService: IBudgetService
    {
        private readonly ApplicationDbContext _dbContext;

        public BudgetService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetBudgetDetails(int BudgetId)
        {
            string json = JsonConvert.SerializeObject(await _dbContext.GetBudgetDetails(BudgetId));
            return json;
        }

        public async Task<string> GetBudgets(int HouseholdId)
        {
            string json = JsonConvert.SerializeObject(await _dbContext.GetBudgets(HouseholdId));
            return json;
        }

        public async Task<decimal> GetBudgetBalance(int BudgetId)
        {
            return await _dbContext.GetBudgetBalance(BudgetId);
        }

        public async Task<int> AddBudget(string name, int householdId)
        {
            return await _dbContext.AddBudget(name, householdId);
        }

        public async Task<int> AddBudgetItem(int categoryId, int budgetId, decimal amount)
        {
            return await _dbContext.AddBudgetItem(categoryId, budgetId, amount);
        }
    }
}