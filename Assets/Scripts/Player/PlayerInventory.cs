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
                OnItemAdded?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool ContainsItem(Item item)
        {
            return inventory.ContainsKey(item);
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