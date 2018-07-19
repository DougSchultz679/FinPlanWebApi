using FinPlanWebAPi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FinPlanWebAPi.Services
{
    public class HouseholdService
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseholdService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetHousehold(int householdId)
        {
            string json = JsonConvert.SerializeObject(_dbContext.GetHousehold(householdId));
            return json; 
        }

        public async Task<int> CreateHousehold(string name)
        {
            return await _dbContext.AddHousehold(name);
        }
    }
}