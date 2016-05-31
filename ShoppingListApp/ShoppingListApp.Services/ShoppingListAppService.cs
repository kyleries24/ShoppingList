using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingListApp.Data;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
    public class ShoppingListAppService
    {
        private ShoppingListItemAppService svc = new ShoppingListItemAppService();

        private readonly Guid _userId;
        public ShoppingListAppService(Guid userId)
        {
            _userId = userId;
        }

        public IEnumerable<ShoppingListModel> GetList()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                return
                    ctx
                    .ShoppingList
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                            new ShoppingListModel
                            {
                                Id = e.Id,
                                Name = e.Name,
                                Color = e.Color
                            })
                        .ToArray();
            }
        }
        public ShoppingListModel GetListById(int id)
        {
            ShoppingList entity;
            using (var ctx = new ShoppingListDbContext())
            {
                entity =
                    ctx
                        .ShoppingList
                        .SingleOrDefault(e => e.UserId == _userId && e.Id == id);    
            }

            return
                new ShoppingListModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
        }
        public bool CreateList(ShoppingListCreateModel vm)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    new ShoppingList
                    {
                        UserId = _userId,
                        Name = vm.Name,
                        Color = vm.Color
                    };
                ctx.ShoppingList.Add(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteList(int id)
        {
            using (var ctx = new ShoppingListDbContext())
            {
                var entity =
                    ctx
                        .ShoppingList
                        .SingleOrDefault(e => e.UserId == _userId && e.Id == id);

                foreach(ShoppingListItem Sli in ctx.ShoppingListItem)
                {
                    if(Sli.ShoppingListId == entity.Id)
                        ctx.ShoppingListItem.Remove(Sli);
                }

                ctx.ShoppingList.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteAllLists()
        {
            using (var ctx = new ShoppingListDbContext())
            {
                foreach (ShoppingList Sl in ctx.ShoppingList)
                {
                    ctx.ShoppingList.Remove(Sl);
                }

                return ctx.SaveChanges() == 1;
            }
        }

    }
}