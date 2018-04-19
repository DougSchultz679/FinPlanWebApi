using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using static FinPlanWebAPi.Models.DataModels;

namespace FinPlanWebAPi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // SQL READ tasks

        public async Task<List<PersonalAccount>> GetAccountByHouseholdId(int hhId)
        {
            return await Database.SqlQuery<PersonalAccount>("GetAllYears @hhId",
                new SqlParameter("hhId", hhId)).ToListAsync();
        }

        public async Task<PersonalAccount> GetAccountDetail(int acctId)
        {
            return await Database.SqlQuery<PersonalAccount>("GetAccountDetail @acctId",
                new SqlParameter("acctId", acctId)).FirstOrDefaultAsync();
        }

        public async Task<Budget> GetBudgetDetails(int bgtId)
        {
            return await Database.SqlQuery<Budget>("GetBudgetDetails @bdtId",
                new SqlParameter("bgtId", bgtId)).FirstOrDefaultAsync();
        }

        public async Task<List<Budget>> GetBudgets(int hhId)
        {
            return await Database.SqlQuery<Budget>("GetBudgets @hhId",
                new SqlParameter("hhId", hhId)).ToListAsync();
        }

        public async Task<Household> GetHousehold(int hhId)
        {
            return await Database.SqlQuery<Household>("GetHousehold @hhId",
                new SqlParameter("hhId", hhId)).FirstOrDefaultAsync();
        }

        public async Task<Transaction> GetTransactionDetail(int trId)
        {
            return await Database.SqlQuery<Transaction>("GetTransactionDetail @trId",
                new SqlParameter("trId", trId)).FirstOrDefaultAsync();
        }

        public async Task<string> GetBudgetBalance(int bgtId)
        {
            return await Database.SqlQuery<string>("GetBudgetBalance @bgtId",
                new SqlParameter("bgtId", bgtId)).FirstOrDefaultAsync();
        }

        public async Task<string> GetAccountBalance(int AccountId)
        {
            return await Database.SqlQuery<string>("GetAccountBalance @AccountId",
                new SqlParameter("AccountId", AccountId)).FirstOrDefaultAsync();
        }

        // SQL CREATE tasks
        public int AddBudget(string name, int hhId)
        {
            return Database.ExecuteSqlCommand("AddBudget @name @hhId",
                new SqlParameter("name", name),
                new SqlParameter("hhId", hhId));
        }

        public int AddBudgetItem(int catId, int bdtId, decimal amnt)
        {
            return Database.ExecuteSqlCommand("AddBudgetItme @catId @bdtId @amnt",
                new SqlParameter("catId", catId),
                new SqlParameter("bdtId", bdtId),
                new SqlParameter("amnt", amnt));
        }

        public int AddHousehold(string name)
        {
            return Database.ExecuteSqlCommand("AddHousehold @name",
                new SqlParameter("name", name));
        }

        public int AddTransaction(int accId, string desc, decimal amt, bool typ, bool isVoid, int catId, string entById, bool recond, decimal recondAmt, bool isDel)
        {
            return Database.ExecuteSqlCommand("AddTransaction @accId @desc @crAt @amt @typ @isVoid @catId @entById, @recond, @recondAmt, @isDel",
                new SqlParameter("accId", accId),
                new SqlParameter("desc", desc),
                new SqlParameter("crAt", DateTimeOffset.Now),
                new SqlParameter("amt", amt),
                new SqlParameter("typ", typ),
                new SqlParameter("isVoid", isVoid),
                new SqlParameter("catId", catId),
                new SqlParameter("entById", entById),
                new SqlParameter("recond", recond),
                new SqlParameter("recondAmt", recondAmt),
                new SqlParameter("isDel", isDel));
        }
    }
}