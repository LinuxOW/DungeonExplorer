using System;

public class GameExample
{
    public static void Main()
    {
        // Create a player with a maximum health of 100
        Player player = new Player("Hero", 100);
        
        // Create a monster (goblin) with a maximum health of 30 and attack power of 5
        Monster goblin = new Monster("Goblin", 30, 5);
        
        // Add items to the player's inventory
        player.Inventory.AddItem(new Weapon("Sword", 10)); // Weapon with 10 damage
        player.Inventory.AddItem(new Potion("Health Potion", 20)); // Potion that heals 20 HP
        
        // Display initial statuses
        Console.WriteLine(player.GetStatus());
        Console.WriteLine(goblin.GetStatus());
        
        // Player attacks goblin with the sword
        player.Inventory.UseItem(0, goblin); // Use sword on goblin
        
        // Goblin attacks player
        goblin.PerformAction(player); // Goblin attacks player
        
        // Player uses health potion on themselves
        player.Inventory.UseItem(1, player); // Use potion on player
        
        // Display final statuses
        Console.WriteLine(player.GetStatus());
        Console.WriteLine(goblin.GetStatus());
    }
}
