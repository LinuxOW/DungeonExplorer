namespace DungeonExplorer
﻿using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    public class Room
    {
        private string description;
        public string RoomItem { get; set; }
        public Room North { get; set; }
        public Room East { get; set; }
        public Room South { get; set; }
        public Room West { get; set; }

        public Room(string description)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(description), "Room description cannot be empty.");
            this.description = description;
        }

        public void CreateItem(string item)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(item), "Item name cannot be empty.");
            RoomItem = item;
        }

        public string GetDescription()
        {
            return description;
        }

        public void RemoveItem()
        {
            Debug.Assert(RoomItem != null, "RemoveItem called, but no item exists in the room right now.");
            RoomItem = null;
        }
    }  