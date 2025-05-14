using System.Collections.Generic;
using Map;

namespace Player
{
    public class PlayerInventory
    {
        private Dictionary<Item, int> inventory;

        public void AddItem(Item item)
        {
            if (!inventory.TryAdd(item, 1))
            {
                inventory[item] += 1;
            }
        }

        public bool ContainsItem(Item item)
        {
            return inventory.ContainsKey(item);
        }

        public void RemoveItem(Item item)
        {
            if(ContainsItem(item))
                inventory[item] -= 1;
        }
    }
}