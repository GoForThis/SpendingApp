using SpendingApp.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpendingApp.Entities
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public int? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public virtual ExpenseCategory ExpenseCategory { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
