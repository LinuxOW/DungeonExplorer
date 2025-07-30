using System.Collections.Generic;
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        private string _name;
        private int _health;
        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        public string Name
        {
            Name = name;
            Health = health;
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Invalid input, player name defaulted to 'Player1'");
                    _name = "Player1";
                }
                else
                {
                    _name = value;
                }
            }
        }
        public void PickUpItem(string item)

        public int Health
        {
            get { return _health; }
            set { _health = Math.Max(0, value); } // Ensures health is never below 0
        }

        public Room CurrentRoom { get; private set; } // externally this is read only however can be set internally

        public Player(string name, int health)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Player1" : name;
            Health = Math.Max(0, health);
        }

        public void EnterRoom(Room room)
        {
            CurrentRoom = room;
            Console.WriteLine($"You have entered: {room.GetDescription()}");
        }

        public void Move(string direction)
        {
            Room nextRoom = null;

            switch (direction.ToLower())
            {
                case "north": nextRoom = CurrentRoom?.North; break;
                case "south": nextRoom = CurrentRoom?.South; break;
                case "east": nextRoom = CurrentRoom?.East; break;
                case "west": nextRoom = CurrentRoom?.West; break;
                default:
                    Console.WriteLine("Invalid direction.");
                    return;
            }

            if (nextRoom != null)
            {
                EnterRoom(nextRoom);
            }
            else
            {
                Console.WriteLine("You can't go that way!");
            }
        }

        public void PickUpItem(string item)
        {
            if (!string.IsNullOrEmpty(item))
            {
                inventory.Add(item);
            }
        }

        public string InventoryContents()
        {
            return string.Join(", ", inventory);
            if (inventory.Count == 0)
                return "Inventory is empty.";

            return string.Join(Environment.NewLine, inventory.Select((x, n) => $"{n + 1}. {x}"));
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Player: {Name}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine("Inventory:");
            Console.WriteLine(InventoryContents());
        }
    }
}