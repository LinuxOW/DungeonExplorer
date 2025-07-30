// Manages multiple interconnected rooms
public class GameMap
{
    private Dictionary<string, List<string>> rooms;
    public GameMap()
    {
        rooms = new Dictionary<string, List<string>>();
    }
    public void AddRoom(string roomName)
    {
        if (!rooms.ContainsKey(roomName))
        {
            rooms[roomName] = new List<string>();
            Console.WriteLine($"Room '{roomName}' has been added to the map.");
        }
        else
        {
            Console.WriteLine($"Room '{roomName}' already exists.");
        }
    }
    public void ConnectRooms(string room1, string room2)
    {
        if (rooms.ContainsKey(room1) && rooms.ContainsKey(room2))
        {
            rooms[room1].Add(room2);
            rooms[room2].Add(room1);
            Console.WriteLine($"Rooms '{room1}' and '{room2}' have been connected.");
        }
        else
        {
            Console.WriteLine("One or both rooms do not exist.");
        }
    }
    public void ShowMap()
    {
        Console.WriteLine("Game Map:");
        foreach (var room in rooms)
        {
            Console.WriteLine($"{room.Key} connects to: {string.Join(", ", room.Value)}");
        }
    }
}