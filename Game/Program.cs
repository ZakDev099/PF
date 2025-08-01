using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Game;

class Player(string name)
{
    public string playerName = name;
    public int level = 0;
    public int gold = 0;
    public Weapon ?equippedWeapon;
    public int maxHealth = 10;
    public int currentHealth = 10;
    public void GiveWeapon(Weapon weaponReward)
    {
        equippedWeapon = weaponReward;
        Console.WriteLine($"You have equipped {equippedWeapon.weaponName}!");
    }
}

class Enemy(string name, int health, int attack)
{
    public string enemyName = name;
    public int maxHealth = health;
    public int currentHealth = health;
    public int enemyAttack = attack;
}
class Weapon(string name, int damage, int accuracy, int block)
{
    public string weaponName = name;
    public int weaponDamage = damage;
    public int weaponAccuracy = accuracy;
    public int weaponBlock = block;
}
// Add list as return
class ObjectLoader
{
    public static SortedDictionary<string, Weapon> LoadWeapons()
    {
        Weapon woodenClub = new("Wooden Club", 1, 1, 1);
        Weapon sling = new("Sling", 1, 1, 1);
        var weapons = new SortedDictionary<string, Weapon>
        {
            {"woodenClub", woodenClub},
            {"sling", sling}
        };
        return weapons;
    }
}

class Battle
{
    
}

class InputHandler
{
    public static int ToInt(List<int> acceptedInputs)
    {
        int result = 0;
        bool isValidInput = false;

        while (!isValidInput)
        {
            string? userInput = Console.ReadLine();
            if (int.TryParse(userInput, out result))
            {
                foreach (int x in acceptedInputs)
                {
                    if (result == x)
                    {
                        Console.WriteLine($"You Entered: {result}");
                        isValidInput = true;
                    }
                }
            }
            if (!isValidInput)
            {
                Console.WriteLine("Invalid input, try again.\nPlease enter a number:");
            }
        }
        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Loading Weapons...\n");
        var allWeapons = ObjectLoader.LoadWeapons();
        Console.WriteLine("Please enter your name");
        var Player1 = new Player(Console.ReadLine());
        Console.WriteLine($"Welcome {Player1.playerName}!");
        Console.WriteLine("1.Wooden Club\n2.Sling\n\nPlease select a starting weapon: ");
        int userInput = InputHandler.ToInt([1, 2]);
        if (userInput == 1)
        {
            Player1.GiveWeapon(allWeapons["woodenClub"]);
        }
        else
        {
            Player1.GiveWeapon(allWeapons["sling"]);
        }
        
    }
}
