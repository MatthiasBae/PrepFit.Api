using Api.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class UserContext : IdentityDbContext<ApplicationUser> {
    
    public UserContext(DbContextOptions<UserContext> options) : base(options) {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }
    
}