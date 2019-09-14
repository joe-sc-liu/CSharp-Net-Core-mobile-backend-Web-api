using System.ComponentModel.DataAnnotations;


namespace AppWebApi.Models
{
    public class ToDoModel
    {
        [Required]
        public string ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Notes { get; set; }

        public bool Done { get; set; }

    }
}
