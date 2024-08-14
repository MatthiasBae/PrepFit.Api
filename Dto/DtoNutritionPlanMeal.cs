namespace Api.Dto;

public class DtoNutritionPlanMeal {
    
    public int NutritionPlanMealId { get; set; }
    public int NutritionPlanId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public DateTime Time { get; set; }
    public int FoodCount { get; set; }
    public double TotalCalories { get; set; }
    public double TotalProtein { get; set; }
    public double TotalCarbs { get; set; }
    public double TotalSugar { get; set; }
    public double TotalFat { get; set; }
    public double TotalFiber { get; set; }
}