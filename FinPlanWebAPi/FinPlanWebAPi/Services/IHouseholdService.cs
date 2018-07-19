using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using threading = System.Threading.Tasks;

namespace FinPlanWebAPi.Services
{
    public interface IHouseholdService
    {
        threading.Task<string> GetHousehold(int HouseholdId);
        threading.Task<int> AddHousehold(string name);
    }
}