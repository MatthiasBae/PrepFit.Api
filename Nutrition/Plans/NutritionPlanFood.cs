using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Nutrition.Plans;

public class NutritionPlanFood {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NutritionPlanFoodId { get; set; }
    public int FoodId { get; set; }
    public int UnitId { get; set; }
    public double Amount { get; set; }
    
    public bool Eaten { get; set; }
    
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Sugar { get; set; }
    public double Fat { get; set; }
    public double Fiber { get; set; }
    
    [ForeignKey("NutritionPlanMeal")]
    public int NutritionPlanMealId { get; set; }
    public NutritionPlanMeal NutritionPlanMeal { get; set; }
}