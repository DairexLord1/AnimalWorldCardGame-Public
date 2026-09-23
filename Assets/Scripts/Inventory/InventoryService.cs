using System;
using System.Collections.Generic;
using AnimalWorld.Collection;

namespace AnimalWorld.Inventory
{
    public class InventoryService
    {
        private readonly List<Item> ownedItems = new();
        // in future can be used to store for user 
        private readonly HashSet<string> ownedItemIds = new();

        public event Action InventoryChanged;

        public IReadOnlyList<Item> OwnedItems => ownedItems;

        public bool IsOwned(Item item) => ownedItemIds.Contains(item.Id);

        public void AddItem(Item item)
        {
            if (!ownedItemIds.Add(item.Id))
            {
                return;
            }

            ownedItems.Add(item);
            InventoryChanged?.Invoke();
        }
    }
}
