using System;
using System.Collections.Generic;

// ================= INTERFACES =================
public interface IDamageable
{
    string Name { get; }
    int Health { get; }
    void TakeDamage(int damage);
    void Heal(int amount);
}

public interface ICollectible
{
    string Name { get; }
    void OnCollect(Creature collector);
}

// ================= ABSTRACT CLASSES =================
public abstract class Creature : IDamageable
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }

    protected Creature(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        Health = Math.Max(0, Health - damage);
        Console.WriteLine($"{Name} takes {damage} damage! (Health: {Health}/{MaxHealth})");
    }

    public virtual void Heal(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
        Console.WriteLine($"{Name} heals {amount} HP! (Health: {Health}/{MaxHealth})");
    }

    public abstract void PerformAction(Creature target);

    public string GetStatus() => $"{Name} [{Health}/{MaxHealth} HP]";
}

// ================= CONCRETE CLASSES =================
public class Player : Creature
{
    public Inventory Inventory { get; } = new Inventory();

    public Player(string name, int maxHealth) : base(name, maxHealth) {}

    public override void PerformAction(Creature target)
    {
        Console.WriteLine($"[PLAYER] {Name} interacts with {target.Name}");
    }
}

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
        Console.WriteLine($"[MONSTER] {Name} attacks!");
        target.TakeDamage(AttackPower);
    }
}

public abstract class Item : ICollectible
{
    public string Name { get; protected set; }
    public abstract void OnCollect(Creature collector);
}

public class Weapon : Item
{
    public int Damage { get; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public override void OnCollect(Creature collector)
    {
        if (collector is Player player)
        {
            player.Inventory.AddItem(this);
            Console.WriteLine($"[LOOT] {player.Name} collected {Name}");
        }
    }

    public void Attack(IDamageable target)
    {
        target.TakeDamage(Damage);
        Console.WriteLine($"[ATTACK] Dealt {Damage} damage with {Name}");
    }
}

public class Potion : Item
{
    public int HealingPower { get; }

    public Potion(string name, int healingPower)
    {
        Name = name;
        HealingPower = healingPower;
    }

    public override void OnCollect(Creature collector)
    {
        collector.Heal(HealingPower);
        Console.WriteLine($"[HEAL] {collector.Name} used {Name} (+{HealingPower} HP)");
    }
}

// ================= SYSTEMS =================
public class Inventory
{
    private readonly List<Item> _items = new List<Item>();

    public void AddItem(Item item) => _items.Add(item);
    public bool Contains(Item item) => _items.Contains(item);

    public void UseItem<T>(Creature user) where T : Item
    {
        var item = _items.Find(i => i is T);
        if (item != null)
        {
            item.OnCollect(user);
            _items.Remove(item);
        }
    }

    public void Display()
    {
        Console.WriteLine("\n[INVENTORY]");
        _items.ForEach(item => Console.WriteLine($"- {item.Name}"));
    }
}

// ================= TEST CODE =================
public class Game
{
    public static void Main()
    {
        var player = new Player("Knight", 120);
        var goblin = new Monster("Goblin Scout", 45, 8);
        
        // Create items
        var sword = new Weapon("Steel Sword", 15);
        var potion = new Potion("Healing Draught", 25);
        var loot = new List<Item> { sword, potion };
        
        // Loot collection
        loot.ForEach(item => item.OnCollect(player));
        
        // Combat sequence
        Console.WriteLine("\n=== COMBAT STARTS ===");
        (sword as Weapon).Attack(goblin);
        goblin.PerformAction(player);
        player.Inventory.UseItem<Potion>(player);
        
        // Status update
        Console.WriteLine("\n=== STATUS ===");
        Console.WriteLine(player.GetStatus());
        Console.WriteLine(goblin.GetStatus());
        
        // Inventory check
        player.Inventory.Display();
    }
}
