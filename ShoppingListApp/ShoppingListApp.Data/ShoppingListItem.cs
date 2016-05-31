using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShoppingListApp.Data
{
    public class ShoppingListItem
    {
        [Key]
        public int Id { get; set; }

        public int ShoppingListId { get; set; }

        public string Contents { get; set; }

        public enum PriorityMessage
        {
            ItCanWait = 0,
            NeedItSoon,
            GrabItNow
        }

        public PriorityMessage Priority { get; set; }

        public bool IsChecked { get; set; }

        public DateTimeOffset CreatedUTC { get; set; }

        public DateTimeOffset? ModifiedUTC { get; set; }
    }
}