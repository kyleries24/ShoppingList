using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Models
{
    public class ShoppingListCreateModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }
    }
}