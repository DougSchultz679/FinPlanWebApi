using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPlanWebAPi.Models
{
    public class DataModels
    {
        public class Budget
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public int HouseholdId { get; set; }
        }

        public class BudgetItem
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public int CategoryId { get; set; }
            [Required]
            public int BudgetId { get; set; }
            [Required]
            public decimal Amount { get; set; }
        }

        public class Category
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
        }

        public class Household
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
        }

        public class Invite
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public int HouseholdId { get; set; }
            [Required]
            public string Email { get; set; }
            public Guid HHToken { get; set; }
            public DateTimeOffset InviteDate { get; set; }
            public string InvitedById { get; set; }
            [Required]
            public bool HasBeenUsed { get; set; }
        }

        public class InviteSent
        {
            [Key]
            public int Id { get; set; }
            public int InviteId { get; set; }
            public ApplicationUser User { get; set; }
            public int IsValid { get; set; }
        }

        public class PersonalAccount
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public int HouseholdId { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public decimal Balance { get; set; }
            [Display(Name = "Reconciled Balance")]
            public decimal ReconciledBalance { get; set; }
            public string CreatedById { get; set; }
            [Display(Name = "Deleted")]
            public bool IsDeleted { get; set; }
        }

        public class Transaction
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public int AccountId { get; set; }
            public string Description { get; set; }
            [Required]
            public DateTimeOffset Date { get; set; }
            [Required]
            public decimal Amount { get; set; }
            [Required]
            public bool Type { get; set; }
            public int CategoryId { get; set; }
            [Required]
            [Display(Name = "Entered By")]
            public string EnteredById { get; set; }
            public bool Reconciled { get; set; }
            public decimal ReconciledAmount { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}