using threading = System.Threading.Tasks;

namespace FinPlanWebAPi.Services
{
    public interface IBudgetService
    {
        threading.Task<string> GetBudgetDetails(int BudgetId);
        threading.Task<string> GetBudgets(int HouseholdId);
        threading.Task<decimal> GetBudgetBalance(int BudgetId);
        threading.Task<int> AddBudget(string name, int householdId);
        threading.Task<int> AddBudgetItem(int categoryId, int budgetId, decimal amount);
     }
}