using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApp.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppAddFullUserName, IdentityRole, string>(options)
    {


        public DbSet<DoListDb> DoListDbs { get; set; }
    }
}