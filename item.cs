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