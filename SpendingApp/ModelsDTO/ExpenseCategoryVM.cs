using System.ComponentModel.DataAnnotations;

namespace SpendingApp.ModelsDTO
{
    public class ExpenseCategoryVM
    {
        [Display(Name = "Category")]
        [Required]
        string Name { get; set; }
    }
}
