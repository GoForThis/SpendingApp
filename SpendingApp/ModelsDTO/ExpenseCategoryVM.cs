using System.ComponentModel.DataAnnotations;

namespace SpendingApp.ModelsDTO
{
    public class ExpenseCategoryVM
    {
        [Display(Name = "Category")]
        [Required]
        public string Name { get; set; }
    }
}
