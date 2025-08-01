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

class Enemy(string name, int level, int health, int attack)
{
    public string enemyName = name;
    public int enemyLevel = level;
    public int maxHealth = health;
    public int currentHealth = health;
    public int enemyAttack = attack;
    public int goldReward = level*10;
}

class Weapon(string name, int damage, int accuracy, int block)
{
    public string weaponName = name;
    public int weaponDamage = damage;
    public int weaponAccuracy = accuracy;
    public int weaponBlock = block;
}

// To Do: Add list as return
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
    public static sortedDictionary<string, Enemy> LoadEnemies()
    {
        Enemy slime = new("Slime", 1, 4, 1);
        Enemy goblin = new("Goblin", 2, 6, 3);
    }
}

class Play
{
    public static void CombatRound(Enemy activeEnemy)
    {
        int enemyDamage = activeEnemy.enemyAttack;
        int playerDamage = player1.equippedWeapon.weaponDamage;
        Console.PrintLine($"UH OH! a {activeEnemy.enemyName} appeared!");
        while (activeEnemy.currentHealth > 0 || player1.currentHealth > 0)
        {
            Console.PrintLine("1. Attack\n");
            Console.PrintLine("Please select an action: ");
            int userInput = InputHandler.ToInt([1]);
            if (userInput == 1)
            {
                int damageDealt = random.Next(playerDamage - 1, playerDamage + 1);
                activeEnemy.currentHealth -= damageDealt;
                PrintLine($"You hit {activeEnemy.enemyName}, dealing {damageDealt} damage!")
            }
            if (activeEnemy.currentHealth > 0)
            {
                int damageTaken = random.Next(enemyDamage - 1, enemyDamage + 1);
                Player1.currentHealth -= damageTaken;
                PrintLine($"{activeEnemy.enemyName} hits you, dealing {damageTaken} damage!");
            }
        }
        if (activeEnemy.currentHealth <= 0)
        {
            Console.WriteLine($"{activeEnemy} has died!");
            int goldRecieved = random.Next(activeEnemy.gold / 2, activeEnemy.gold * 2);
            player1.gold += goldRecieved;
            Console.PrintLine($"You recieved {goldRecieved} gold!");
        }
    }
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
        Random random = new();
        Console.WriteLine("Loading Weapons...\n");
        var allWeapons = ObjectLoader.LoadWeapons();
        Console.WriteLine("Loading Enemies...\n");
        var allEnemies = ObjectLoader.LoadEnemies();
        Console.WriteLine("Please enter your name");
        var player1 = new Player(Console.ReadLine());
        Console.WriteLine($"Welcome {player1.playerName}!");
        Console.WriteLine("1.Wooden Club\n2.Sling\n\nPlease select a starting weapon: ");
        int userInput = InputHandler.ToInt([1, 2]);
        if (userInput == 1)
        {
            player1.GiveWeapon(allWeapons["woodenClub"]);
        }
        else
        {
            player1.GiveWeapon(allWeapons["sling"]);
        }
        Play.CombatRound(allEnemies[slime]);
    }
}
