using System.ComponentModel.DataAnnotations;

namespace SpendingApp.ModelsDTO
{
    public class CreateExpenseVM
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Payment Method")]
        [Required(ErrorMessage = "Payment Method is required")]
        public PaymentMethod PaymentMethod { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount is required")]
        public int Amount { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Display(Name = "Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Category is required")]
        public int ExpenseCategoryId { get; set; }
    }
}
