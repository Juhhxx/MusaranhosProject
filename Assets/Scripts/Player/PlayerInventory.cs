using System;
using System.Collections.Generic;
using Map;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour
    {
        private Dictionary<Item, int> inventory;
        private List<Letter> letters;

        private void Start()
        {
            inventory = new Dictionary<Item, int>();
            letters = new List<Letter>();
            AddItem(Item.None);
        }

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

        public void AddLetter(Letter letter)
        {
            letters.Add(letter);
        }
    }
}