using System;
using System.Collections.Generic;
// Represents creatures in rooms
public class Monster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int AttackPower { get; set; }
    public Monster(string name, int health, int attackPower)
    {
        Name = name;
        Health = health;
        AttackPower = attackPower;
    }
    public void Attack(Monster target)
    {
        target.Health -= AttackPower;
        Console.WriteLine($"{Name} attacks {target.Name} for {AttackPower} damage!");
    }
}
// Represents multiple types of items like weapons or potions
public class Item
{
    public string Name { get; set; }
    public string Type { get; set; } // e.g., "Weapon", "Potion"
    public int Value { get; set; } // Could represent damage for weapons or healing for potions
    public Item(string name, string type, int value)
    {
        Name = name;
        Type = type;
        Value = value;
    }
}