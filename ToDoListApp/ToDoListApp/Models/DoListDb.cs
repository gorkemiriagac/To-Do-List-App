using System.ComponentModel.DataAnnotations;

namespace ToDoListApp.Models
{
    public class DoListDb
    {
        [Key]
        public int id { get; set; }
        public required string Username { get; set; }
        public required string Container { get; set; }

        public int durum { get; set; }
    }
}
