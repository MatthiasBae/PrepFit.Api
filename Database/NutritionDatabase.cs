using Api.Dto;
using Api.Nutrition.Plans;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class NutritionDatabase {

    private readonly FitnessContext _FitnessContext;
    
    public NutritionDatabase(FitnessContext fitnessContext) {
        this._FitnessContext = fitnessContext;
    }
    
    public async Task<List<NutritionPlan>> GetNutritionPlansAsync() {
        return await this._FitnessContext.NutritionPlans.ToListAsync();
    }

    public async Task<NutritionPlan?> GetNutritionPlanAsync(int nutritionPlanId) {
        return await this._FitnessContext.NutritionPlans
            .FirstOrDefaultAsync(plan => plan.NutritionPlanId == nutritionPlanId);
    }

    public async Task<List<DtoNutritionPlanMeal>> GetNutritionPlanMealsInclNutrientsAsync(int nutritionPlanId) {
        return await this._FitnessContext.NutritionPlanMeals
            .Where(meal => meal.NutritionPlanId == nutritionPlanId)
            .Select(dtoMeal => new DtoNutritionPlanMeal {
                NutritionPlanMealId = dtoMeal.NutritionPlanMealId,
                NutritionPlanId = dtoMeal.NutritionPlanId,
                Name = dtoMeal.Name,
                Order = dtoMeal.Order,
                Time = dtoMeal.Time,
                FoodCount = dtoMeal.Foods.Count,
                TotalCalories = dtoMeal.Foods.Sum(food => food.Calories),
                TotalProtein = dtoMeal.Foods.Sum(food => food.Protein),
                TotalCarbs = dtoMeal.Foods.Sum(food => food.Carbs),
                TotalSugar = dtoMeal.Foods.Sum(food => food.Sugar),
                TotalFat = dtoMeal.Foods.Sum(food => food.Fat),
                TotalFiber = dtoMeal.Foods.Sum(food => food.Fiber)
            })
            .ToListAsync();
    }

    public async Task<DtoNutritionPlanMeal?> GetNutritionPlanMealInclNutrientsAsync(int nutritionPlanId, int nutritionPlanMealId) {
        return await this._FitnessContext.NutritionPlanMeals
            .Where(meal => meal.NutritionPlanId == nutritionPlanId && meal.NutritionPlanMealId == nutritionPlanMealId)
            .Select(dtoMeal => new DtoNutritionPlanMeal {
                NutritionPlanMealId = dtoMeal.NutritionPlanMealId,
                NutritionPlanId = dtoMeal.NutritionPlanId,
                Name = dtoMeal.Name,
                Order = dtoMeal.Order,
                Time = dtoMeal.Time,
                FoodCount = dtoMeal.Foods.Count,
                TotalCalories = dtoMeal.Foods.Sum(food => food.Calories),
                TotalProtein = dtoMeal.Foods.Sum(food => food.Protein),
                TotalCarbs = dtoMeal.Foods.Sum(food => food.Carbs),
                TotalSugar = dtoMeal.Foods.Sum(food => food.Sugar),
                TotalFat = dtoMeal.Foods.Sum(food => food.Fat),
                TotalFiber = dtoMeal.Foods.Sum(food => food.Fiber)
            })
            .FirstOrDefaultAsync();
    }
}