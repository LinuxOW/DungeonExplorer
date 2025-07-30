using System;
using System.Media;

namespace DungeonExplorer
{
@@ -10,17 +9,102 @@ internal class Game

        public Game()
        {
            // Initialize the game with one room and one player
            // Initialize rooms
            Room room1 = new Room("A damp, cold room with seemingly little light... You can see a light towards the South.");
            Room room2 = new Room("An old prison cell with a monster and exit tunnel to the East.");
            Room room3 = new Room("A portal chamber containing a treasure chest.");

            // Connect rooms
            room1.South = room2;
            room2.North = room1;
            room3.West = room2;
            room2.East = room3;

            // Create Player
            Console.Write("Enter player name: ");
            string playerName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                playerName = "Adventurer";
            }

            player = new Player(playerName, 100);
            player.EnterRoom(room1);

            currentRoom = room1;

            // Add items in rooms
            room1.CreateItem("Stick");
            room3.CreateItem("Gold");
        }

        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = false;
            while (playing)
            while (player.Health > 0)
            {
                // Code your playing logic here
                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1 - View room description");
                Console.WriteLine("2 - Move to another room");
                Console.WriteLine("3 - View player stats");
                Console.WriteLine("4 - Pick up an item");
                Console.WriteLine("5 - Exit game");

                string action = Console.ReadLine()?.Trim();

                if (action == "1")
                {
                    Console.WriteLine(player.CurrentRoom.GetDescription());
                }
                else if (action == "2")
                {
                    Console.WriteLine("Which way do you want to go?");
                    Console.WriteLine("1 - North");
                    Console.WriteLine("2 - East");
                    Console.WriteLine("3 - South");
                    Console.WriteLine("4 - West");
                    string direction = Console.ReadLine()?.Trim();

                    switch (direction)
                    {
                        case "1": player.Move("North"); break;
                        case "2": player.Move("East"); break;
                        case "3": player.Move("South"); break;
                        case "4": player.Move("West"); break;
                        default: Console.WriteLine("Invalid direction! Choose 1, 2, 3 or 4."); break;
                    }
                }
                else if (action == "3")
                {
                    player.DisplayStatus();
                }
                else if (action == "4")
                {
                    if (!string.IsNullOrEmpty(player.CurrentRoom.RoomItem))
                    {
                        player.PickUpItem(player.CurrentRoom.RoomItem);
                        Console.WriteLine($"You picked up: {player.CurrentRoom.RoomItem}");
                        player.CurrentRoom.RemoveItem(); // Proper removal
                    }
                    else
                    {
                        Console.WriteLine("There's nothing to pick up.");
                    }
                }
                else if (action == "5")
                {
                    Console.WriteLine("Exiting the game...");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Choose between options 1-5.");
                }
            }
            Console.WriteLine("Game over.");
        }
    }