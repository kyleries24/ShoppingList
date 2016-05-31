using System.Collections.Generic;
using System.Linq;
using ShoppingListApp.Data;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
    public class ShoppingListItemAppService
    { 
        public ShoppingListItemAppService()
        {
        }

        public IEnumerable<ShoppingListItemModel> GetItems(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .ShoppingListItem
                    .Where(e => e.ShoppingListId == id)
                    .Select(
                        e =>
                            new ShoppingListItemModel
                            {
                                Id = e.Id,
                                ShoppingListId = e.ShoppingListId,
                                Contents = e.Contents,
                                IsChecked = e.IsChecked,
                                Priority = (ShoppingListApp.Models.ShoppingListItemModel.PriorityMessage)e.Priority
                            })
                        .ToArray();
            }
        }
        public ShoppingListItemModel GetItemById(int Eid, int Sid)
        {
            ShoppingListItem entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                        .ShoppingListItem
                        .SingleOrDefault(e => e.Id == Eid && e.ShoppingListId == Sid);
            }

            return
                new ShoppingListItemModel
                {
                    Id = entity.Id,
                    ShoppingListId = entity.ShoppingListId,
                    Contents = entity.Contents,
                    IsChecked = entity.IsChecked,
                    Priority = (ShoppingListApp.Models.ShoppingListItemModel.PriorityMessage)entity.Priority
                };
        }
        public bool CreateItem(ShoppingListItemCreateModel vm, int Id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingListItem
                    {
                        ShoppingListId = Id,
                        Contents = vm.Contents,
                        IsChecked = vm.IsChecked,
                        Priority = (ShoppingListApp.Data.ShoppingListItem.PriorityMessage)vm.Priority
                    };

                ctx.ShoppingListItem.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int Eid, int Sid)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .ShoppingListItem
                        .Single(e => e.ShoppingListId == Sid && e.Id == Eid);

                ctx.ShoppingListItem.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAllItems()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach (ShoppingListItem Sli in ctx.ShoppingListItem)
                {
                    ctx.ShoppingListItem.Remove(Sli);
                }

                return ctx.SaveChanges() == 1;
            }
        }
    }
}