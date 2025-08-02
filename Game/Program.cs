using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace Game;

class Player()
{
    public string playerName = "player";
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

class Enemy(string name, int level, int health, int attack, int rarity)
{
    public string enemyName = name;
    public int enemyLevel = level;
    public int maxHealth = health;
    public int currentHealth = health;
    public int enemyAttack = attack;
    public int enemyRarity = rarity;
    public int goldReward = level * 10;
}

class Weapon(string name, int damage, int rarity)
{
    public string weaponName = name;
    public int weaponDamage = damage;
    public int weaponRarity = rarity;
}

// To Do: Add list as return
class ObjectLoader
{
    public static SortedDictionary<string, Weapon> LoadWeapons()
    {
        Console.WriteLine("Loading Weapons...");
        Weapon woodenClub = new("Wooden Club", 1, 1);
        Weapon sling = new("Sling", 1, 1);
        var weapons = new SortedDictionary<string, Weapon>
        {
            {"woodenClub", woodenClub},
            {"sling", sling}
        };
        return weapons;
    }
    public static SortedDictionary<string, Enemy> LoadEnemies()
    {
        Console.WriteLine("Loading Enemies...\n");
        Enemy slime = new("Slime", 1, 4, 1, 1);
        Enemy goblin = new("Goblin", 2, 6, 3, 1);
        var enemies = new SortedDictionary<string, Enemy>
        {
            {"slime", slime},
            {"goblin", goblin}
        };
        return enemies;
    }
}

class PlayGame(Player player1)
{
    public Player player1 = player1;
    public void StartGame(SortedDictionary<string, Weapon> allWeapons)
    {
        Console.WriteLine("Please enter your name");
        Console.WriteLine($"Welcome {player1.playerName}!");
        Console.WriteLine
        (
            "1.Wooden Club",
            "2.Sling",
            "Please select a starting weapon: "
        );
        int userInput = InputHandler.ToInt([1, 2]);
        if (userInput == 1)
        {
            player1.GiveWeapon(allWeapons["woodenClub"]);
        }
        else
        {
            player1.GiveWeapon(allWeapons["sling"]);
        }
    }
    
    public void CombatRound(Enemy activeEnemy, Random random)
    {
        int enemyDamage = activeEnemy.enemyAttack;
        int playerDamage = player1.equippedWeapon.weaponDamage;

        Console.WriteLine($"UH OH! a {activeEnemy.enemyName} appeared!");
        while (activeEnemy.currentHealth > 0 && player1.currentHealth > 0)
        {
            Console.WriteLine
            (
                "1. Attack",
                "Please select an action: "
            );
            int userInput = InputHandler.ToInt([1]);
            if (userInput == 1)
            {
                int damageDealt = random.Next(playerDamage - 1, playerDamage + 2);
                activeEnemy.currentHealth -= damageDealt;
                Console.WriteLine($"You hit {activeEnemy.enemyName}, dealing {damageDealt} damage!");
            }
            if (activeEnemy.currentHealth > 0)
            {
                int damageTaken = random.Next(enemyDamage - 1, enemyDamage + 2);
                player1.currentHealth -= damageTaken;
                Console.WriteLine($"{activeEnemy.enemyName} hits you, dealing {damageTaken} damage!");
            }
        }
        if (activeEnemy.currentHealth <= 0)
        {
            Console.WriteLine($"{activeEnemy.enemyName} has died!");
            int goldRecieved = random.Next(activeEnemy.goldReward / 2, activeEnemy.goldReward * 2);
            player1.gold += goldRecieved;
            Console.WriteLine($"You recieved {goldRecieved} gold!");
        }
    }
}

class Menu
{
    public static void MainMenu
    (
        Random globalRandom,
        SortedDictionary<string, Weapon> allWeapons,
        SortedDictionary<string, Enemy> allEnemies,
        Player player1,
        PlayGame Play,
        bool runProgram
    )
    {
        Console.WriteLine
        (
            "1. Fight",
            "2. Shop",
            "3. My Character",
            "4. Quit Game"
        );
        var userInput = InputHandler.ToInt([1, 2, 3, 4]);
        if (userInput == 1)
        {
            //Play.CombatRound()...
        }
        else if (userInput == 2)
        {
            //Shop code goes here...
        }
        else if (userInput == 3)
        {
            //Show character inventory, stats, etc...
        }
        else
        {
            //Are you sure you want to quit?...
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
        Random globalRandom = new();
        var allWeapons = ObjectLoader.LoadWeapons();
        var allEnemies = ObjectLoader.LoadEnemies();
        var player1 = new Player();
        PlayGame Play = new(player1);
        Play.StartGame(allWeapons);
        bool runProgram = true;
        while (runProgram)
        {
            Menu.MainMenu(globalRandom, allWeapons, allEnemies, player1, Play, runProgram);
        }
        //Play.CombatRound(allEnemies["slime"], globalRandom);
    }
}
