using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Nutrition.Plans;

public class NutritionPlan {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NutritionPlanId { get; set; }
    public string Username { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
}