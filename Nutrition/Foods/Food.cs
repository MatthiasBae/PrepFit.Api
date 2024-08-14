using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Nutrition.Foods;

[Table("Foods")]
public class Food {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FoodId { get; set; }

    public string CreatedBy { get; set; } = "PrepFit";
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    [Required]
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Store { get; set; }
    
    public string Url { get; set; }
    
    public bool IsRecipe { get; set; }
    public bool IsVegan { get; set; }
    public bool IsVegetarian { get; set; }
    
    public ICollection<FoodUnit> Units { get; set; }
}