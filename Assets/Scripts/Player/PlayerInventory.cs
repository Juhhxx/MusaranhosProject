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
        public List<Letter> Letters => letters;
        public Item NewItem {get; private set;}

        public event EventHandler OnItemAdded;
        public event EventHandler OnItemRemoved;
        public event EventHandler OnLetterAdded;

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
                NewItem = item;
                if(item is not (Item.None or Item.Shiv or Item.Block)) OnItemAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool ContainsItem(Item item)
        {
            int temp;
            inventory.TryGetValue(item, out temp);
            return inventory.ContainsKey(item) && temp > 0;
        }

        public void RemoveItem(Item item)
        {
            if (ContainsItem(item))
            {
                inventory[item] -= 1;
                OnItemRemoved?.Invoke(this, EventArgs.Empty);
            }
        }

        public void AddLetter(Letter letter)
        {
            letters.Add(letter);
            OnLetterAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}