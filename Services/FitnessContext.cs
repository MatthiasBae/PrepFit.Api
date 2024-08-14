using Api.Nutrition.Foods;
using Api.Nutrition.Plans;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class FitnessContext : DbContext {
    private readonly IHttpContextAccessor HttpContextAccessor;
    public string? CurrentUser { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodUnit> FoodUnits { get; set; }
    
    public DbSet<NutritionPlan> NutritionPlans { get; set; }
    public DbSet<NutritionPlanMeal> NutritionPlanMeals { get; set; }
    public DbSet<NutritionPlanFood> NutritionPlanFoods { get; set; }
    
    public FitnessContext(DbContextOptions<FitnessContext> options, IHttpContextAccessor httpContextAccessor) : base(options) {
        this.HttpContextAccessor = httpContextAccessor;
        var user = this.HttpContextAccessor.HttpContext?.User;
        
        if (user != null) {
            this.CurrentUser = user.Identity?.Name;
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
        //Query filter on user
        modelBuilder.Entity<Food>()
            .HasQueryFilter(food => food.CreatedBy == this.CurrentUser || food.CreatedBy == "PrepFit");
        modelBuilder.Entity<FoodUnit>()
            .HasQueryFilter(unit => unit.Food.CreatedBy == this.CurrentUser || unit.Food.CreatedBy == "PrepFit");
        
        modelBuilder.Entity<NutritionPlan>()
            .HasQueryFilter(plan => plan.Username == this.CurrentUser || plan.Username == "PrepFit");
        modelBuilder.Entity<NutritionPlanMeal>()
            .HasQueryFilter(meal => meal.NutritionPlan.Username == this.CurrentUser || meal.NutritionPlan.Username == "PrepFit");
        modelBuilder.Entity<NutritionPlanFood>()
            .HasQueryFilter(food => food.NutritionPlanMeal.NutritionPlan.Username == this.CurrentUser || food.NutritionPlanMeal.NutritionPlan.Username == "PrepFit");
    }
}