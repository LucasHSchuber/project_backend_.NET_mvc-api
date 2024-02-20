using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using project_webbservice.Models;

namespace projekt_webbservice.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> User { get; set; } // Represents the User entity
    public DbSet<Audio> Audio { get; set; } // Represents the Audio entity
    public DbSet<Category> Category { get; set; } // Represents the Category entity
    public DbSet<UserList> UserList { get; set; } // Represents the UserList entity
    public DbSet<UserListAudio> UserListAudio { get; set; } // Represents the UserListAudio entity
    public DbSet<Like> Like { get; set; } // Represents the Like entity

public DbSet<ApplicationUser> ApplicationUser { get; set; } = default!;
}
