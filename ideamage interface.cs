using System;
using System.Collections.Generic;

// Interfaces
public interface IDamageable
{
    void TakeDamage(int damage);
    int Health { get; }
}

public interface ICollectible
{
    string Name { get; }
    void Collect();
}

// Abstract Creature (implements IDamageable)
public abstract class Creature : IDamageable
{
    protected string Name { get; set; }
    public int Health { get; protected set; }  // Required by IDamageable
    protected int MaxHealth { get; set; }
    
    public abstract void PerformAction(Creature target);

    protected Creature(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    // Explicit implementation of IDamageable.TakeDamage
    void IDamageable.TakeDamage(int damage) 
    {
        Health = Math.Max(0, Health - damage);
        Console.WriteLine($"{Name} takes {damage} damage. Health: {Health}/{MaxHealth}");
    }

    public void Heal(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
        Console.WriteLine($"{Name} heals {amount} health. Health: {Health}/{MaxHealth}");
    }
}

// Player now implements IDamageable through Creature
public class Player : Creature
{
    public Inventory Inventory { get; } = new Inventory();

    public Player(string name, int maxHealth) : base(name, maxHealth) { }

    public override void PerformAction(Creature target)
    {
        Console.WriteLine($"{Name} interacts with {target.Name}");
    }
}

// Monster implements IDamageable through Creature
public class Monster : Creature
{
    public int AttackPower { get; }

    public Monster(string name, int maxHealth, int attackPower) 
        : base(name, maxHealth) 
    {
        AttackPower = attackPower;
    }

    public override void PerformAction(Creature target)
    {
        Console.WriteLine($"{Name} attacks!");
        ((IDamageable)target).TakeDamage(AttackPower);
    }
}

// Base Item class with ICollectible
public abstract class Item : ICollectible
{
    public string Name { get; protected set; }  // Required by ICollectible
    
    public virtual void Collect()
    {
        Console.WriteLine($"{Name} was collected!");
    }

    public abstract void Use(Creature target);
}

// Updated Weapon (still inherits from Item)
public class Weapon : Item
{
    public int Damage { get; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public override void Use(Creature target)
    {
        Console.WriteLine($"Swinging {Name} at {target}!");
        ((IDamageable)target).TakeDamage(Damage);
    }
}

// Updated Potion (implements both Item and ICollectible)
public class Potion : Item
{
    public int HealingPower { get; }

    public Potion(string name, int healingPower)
    {
        Name = name;
        HealingPower = healingPower;
    }

    public override void Collect() 
    {
        Console.WriteLine($"{Name} collected (restores {HealingPower} HP)");
    }

    public override void Use(Creature target)
    {
        Console.WriteLine($"Drinking {Name}...");
        target.Heal(HealingPower);
    }
}

// Updated Inventory to use interfaces
public class Inventory
{
    private List<ICollectible> items = new List<ICollectible>();

    public void AddItem(ICollectible item)
    {
        items.Add(item);
        item.Collect();  // Notice polymorphic call to Collect()
    }

    public void UseItem(int index, Creature target)
    {
        if (index >= 0 && index < items.Count && items[index] is Item item)
        {
            item.Use(target);
        }
    }
}

// Test case
public class GameTest
{
    public static void Main()
    {
        var player = new Player("Arthas", 120);
        var zombie = new Monster("Zombie", 50, 8);

        player.Inventory.AddItem(new Weapon("Frostmourne", 25));
        player.Inventory.AddItem(new Potion("Elixir of Life", 30));

        Console.WriteLine($"Initial: {player.Health} HP | Zombie: {zombie.Health} HP");

        // Player attacks zombie
        player.Inventory.UseItem(0, zombie);
        
        // Zombie attacks player
        zombie.PerformAction(player);
        
        // Player heals
        player.Inventory.UseItem(1, player);

        Console.WriteLine($"Final: {player.Health} HP | Zombie: {zombie.Health} HP");
    }
}
