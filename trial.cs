using System;
using System.Collections.Generic;

// Abstract base class representing any creature in the game
public abstract class Creature
{
    // Encapsulated fields with protected access
    protected string Name { get; set; }
    protected int Health { get; set; }
    protected int MaxHealth { get; set; }

    // Abstract method (must be implemented by derived classes)
    public abstract void PerformAction(Creature target);

    // Constructor
    protected Creature(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    // Public method with safe health modification
    public void TakeDamage(int damage)
    {
        Health = Math.Max(0, Health - damage);
        Console.WriteLine($"{Name} takes {damage} damage! Health: {Health}/{MaxHealth}");
    }

    public void Heal(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
        Console.WriteLine($"{Name} heals {amount} health! Health: {Health}/{MaxHealth}");
    }

    public string GetStatus()
    {
        return $"{Name} (HP: {Health}/{MaxHealth})";
    }
}

// Player class inheriting from Creature
public class Player : Creature
{
    public Inventory Inventory { get; private set; }

    public Player(string name, int maxHealth) : base(name, maxHealth)
    {
        Inventory = new Inventory();
    }

    // Implement abstract method
    public override void PerformAction(Creature target)
    {
        Console.WriteLine($"{Name} interacts with {target.Name}");
    }
}

// Monster class inheriting from Creature
public class Monster : Creature
{
    public int AttackPower { get; private set; }

    public Monster(string name, int maxHealth, int attackPower) : base(name, maxHealth)
    {
        AttackPower = attackPower;
    }

    // Implement abstract method
    public override void PerformAction(Creature target)
    {
        target.TakeDamage(AttackPower);
        Console.WriteLine($"{Name} attacks {target.Name} with {AttackPower} damage!");
    }
}

// Base Item class
public abstract class Item
{
    public string Name { get; protected set; }
    public abstract void Use(Creature target);
}

// Weapon subclass
public class Weapon : Item
{
    public int Damage { get; private set; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public override void Use(Creature target)
    {
        target.TakeDamage(Damage);
        Console.WriteLine($"Using {Name} to attack {target.Name} for {Damage} damage!");
    }
}

// Potion subclass
public class Potion : Item
{
    public int HealingPower { get; private set; }

    public Potion(string name, int healingPower)
    {
        Name = name;
        HealingPower = healingPower;
    }

    public override void Use(Creature target)
    {
        target.Heal(HealingPower);
        Console.WriteLine($"Using {Name} to heal {target.Name} for {HealingPower} HP!");
    }
}

// Inventory class (updated to work with the abstract Item class)
public class Inventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"Added item: {item.Name}");
    }

    public void RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            Console.WriteLine($"Removed item: {item.Name}");
        }
    }

    public void UseItem(int index, Creature target)
    {
        if (index >= 0 && index < items.Count)
        {
            items[index].Use(target);
        }
    }

    public void DisplayInventory()
    {
        Console.WriteLine("Inventory Contents:");
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i}. {items[i].Name}");
        }
    }
}

// GameMap class remains the same as in previous example
