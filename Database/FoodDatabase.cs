using Api.Nutrition.Foods;
using Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class FoodDatabase {
    private readonly FitnessContext _Context;
    
    public FoodDatabase(FitnessContext context) {
        this._Context = context;
    }
    
    public async Task<List<Food>> GetFoods() {
        return await this._Context.Foods
            .ToListAsync();
    }
    
    public async Task<Food?> GetFoodAsync(int foodId) {
        return await this._Context.Foods
            .FirstOrDefaultAsync(food => food.FoodId == foodId);
    }
    
    public async Task<Food?> GetFoodInclUnitsAsync(int foodId) {
        return await this._Context.Foods
            .Include(food => food.Units)
            .FirstOrDefaultAsync(food => food.FoodId == foodId);
    }
    
    public async Task<List<Food>> GetFoodsAsync(string name, string brand) {
        return await this._Context.Foods
            .Where(food => food.Name.Contains(name) && food.Brand.Contains(brand))
            .ToListAsync();
    }
}