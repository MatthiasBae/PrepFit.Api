using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Nutrition.Plans;

public class NutritionPlanMeal {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NutritionPlanMealId { get; set; }
    [Required]
    public int Order { get; set; }
    public string Name { get; set; }
    public DateTime Time { get; set; }
    public ICollection<NutritionPlanFood> Foods { get; set; }
    
    [ForeignKey("NutritionPlan")]
    public int NutritionPlanId { get; set; }
    public NutritionPlan NutritionPlan { get; set; }
    
}