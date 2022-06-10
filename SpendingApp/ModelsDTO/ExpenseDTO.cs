using SpendingApp.Entities;
using SpendingApp.Entities.Enums;

namespace SpendingApp.ModelsDTO
{
    public class ExpenseDTO
    {
        public string Name { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
    }
}
