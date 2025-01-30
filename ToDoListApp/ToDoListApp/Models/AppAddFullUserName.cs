using Microsoft.AspNetCore.Identity;

namespace ToDoListApp.Models
{
    public class AppAddFullUserName:IdentityUser
    {
        public string FullName { get; set; }


    }
}
