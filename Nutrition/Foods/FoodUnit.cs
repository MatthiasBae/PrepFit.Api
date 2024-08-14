using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Nutrition.Foods;

[Table("FoodUnits")]
public class FoodUnit {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FoodUnitId { get; set; }
    [Required]
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    [Required]
    public double Calories { get; set; }
    [Required]
    public double Protein { get; set; }
    [Required]
    public double Carbs { get; set; }
    [Required]
    public double Fat { get; set; }
    public double Sugar { get; set; }
    public double Fiber { get; set; }
    
    
    [ForeignKey("Food")]
    public int FoodId { get; set; }
    public Food Food { get; set; }
}