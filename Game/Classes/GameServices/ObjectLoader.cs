using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

class ObjectLoader
{
    public static Random loadGlobalRandom()
    {
        Random globalRandom = new();
        return globalRandom;
    }
    public static SortedDictionary<string, Weapon> LoadWeapons()
    {
        Console.WriteLine("Loading Weapons...");
        Weapon fists = new("Fists", 1, 1);
        Weapon woodenClub = new("Wooden Club", 1, 1);
        Weapon sling = new("Sling", 1, 1);
        var allWeapons = new SortedDictionary<string, Weapon>
        {
            {"fists", fists},
            {"woodenClub", woodenClub},
            {"sling", sling}
        };
        return allWeapons;
    }
    public static SortedDictionary<string, Enemy> LoadEnemies()
    {
        Console.WriteLine("Loading Enemies...\n");
        Enemy voidSlime = new("Void Slime", 1, 1, 1, 0, 0);
        Enemy greenSlime = new("Green Slime", 1, 3, 1, 10, 10);
        Enemy blueSlime = new("Blue Slime", 1, 6, 1, 8, 15);
        Enemy redSlime = new("Red Slime", 1, 10, 1, 6, 20);
        Enemy iridescentSlime = new("Iridescent Slime", 1, 10, 1, 1, 100);
        Enemy goblin = new("Goblin", 2, 6, 3, 20, 50);
        var enemies = new SortedDictionary<string, Enemy>
        {
            {"voidSlime", voidSlime},
            {"greenSlime", greenSlime},
            {"blueSlime", blueSlime},
            {"redSlime", redSlime},
            {"iridescentSlime", iridescentSlime},
            { "goblin", goblin}
        };
        return enemies;
    }
}