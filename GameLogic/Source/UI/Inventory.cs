using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;



namespace BulletGame
{
    public class Inventory
    {
        private List<Item> items;
        private int Capacity { get; set; } = 30;
        public Inventory()
        {
            items = new List<Item>();
        }

        public bool AddItem(Item item)
        {
            if (items.Count < Capacity)
            {
                items.Add(item);
                return true;
            }
            return false;
        }
        public bool ContainsItem(Item item)
        {
            return items.Contains(item);
        }
        public bool RemoveItem(Item item)
        {
            return items.Remove(item);
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Item(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}