using System.ComponentModel.DataAnnotations;

namespace SpendingApp.Entities
{
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
