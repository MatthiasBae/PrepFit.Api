using Api.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

// [Authorize]
[ApiController]
[Route("api/foods")]
public class FoodController : Controller {
    
    private readonly FoodDatabase _FoodDatabase;
    
    public FoodController(FoodDatabase foodDatabase) {
        this._FoodDatabase = foodDatabase;
    }    
    
    [HttpGet]
    public async Task<IActionResult> GetFoodsAsync() {
        var foods = await this._FoodDatabase.GetFoods();
        return this.Ok(foods);
    }
    
    [HttpGet("{foodId:int}")]
    public async Task<IActionResult> GetFoodInclUnitsAsync(int foodId) {
        var food = await this._FoodDatabase.GetFoodInclUnitsAsync(foodId);
        if (food == null) {
            return this.NotFound();
        }
        return this.Ok(food);
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> GetFoodsAsync(string name, string brand) {
        var foods = await this._FoodDatabase.GetFoodsAsync(name, brand);
        return this.Ok(foods);
    }
}