using Api.Database;
using Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/nutrition/plans")]
public class NutritionController : Controller {
    
    private readonly NutritionDatabase _NutritionDatabase;
    
    public NutritionController(NutritionDatabase nutritionDatabase) {
        this._NutritionDatabase = nutritionDatabase;
    }
    
    [HttpGet("{nutritionPlanId:int}")]
    public async Task<IActionResult> GetNutritionPlanAsync(int nutritionPlanId) {
        var plans = await this._NutritionDatabase.GetNutritionPlanAsync(nutritionPlanId);
        return this.Ok(plans);
    }
    
    [HttpGet("{nutritionPlanId:int}/meals")]
    public async Task<IActionResult> GetNutritionPlanMealsInclNutrients(int nutritionPlanId) {
        var plans = await this._NutritionDatabase.GetNutritionPlanMealsInclNutrientsAsync(nutritionPlanId);
        return this.Ok(plans);
    }
    
    [HttpGet("{nutritionPlanId:int}/meals/{nutritionPlanMealId:int}")]
    public async Task<IActionResult> GetNutritionPlanMealAsync(int nutritionPlanId, int nutritionPlanMealId) {
        var planMeal = await this._NutritionDatabase.GetNutritionPlanMealInclNutrientsAsync(nutritionPlanId, nutritionPlanMealId);
        if (planMeal is null) {
            return this.NotFound();
        }
        return this.Ok(planMeal);
    }
    
}