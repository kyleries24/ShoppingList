using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Models
{
    public class ShoppingListEditModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        [Required]
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Can only input color codes")]
        [MinLength(7, ErrorMessage = "Please enter one # symbol followed by your hexadecimal color code.")]
        public string Color { get; set; }
    }
}