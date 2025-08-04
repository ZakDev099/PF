using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace Game;

class Player()
{
    public string PlayerName = "player";
    public int PlayerLevel = 1;
    public int Gold = 0;
    public Weapon ?EquippedWeapon;
    public int MaxHealth = 10;
    public int CurrentHealth = 10;
    public void GiveWeapon(Weapon weaponReward)
    {
        EquippedWeapon = weaponReward;
        Console.WriteLine($"You have equipped {EquippedWeapon.WeaponName}!");
    }
}

class Enemy
{
    public string EnemyName;
    public int EnemyLevel;
    public int MaxHealth;
    public int CurrentHealth;
    public int EnemyAttack;
    public int SpawnChance;
    public int GoldRewardMax;
    public int GoldRewardMin;

    public Enemy(string name, int level, int health, int attack, int spawnChance, int gold)
    {
        EnemyName = name;
        EnemyLevel = level;
        MaxHealth = health;
        CurrentHealth = health;
        EnemyAttack = attack;
        SpawnChance = spawnChance;
        GoldRewardMax = gold + 5;
        if (gold - 5 >= 1)
        {
            GoldRewardMin = gold - 5;
        }
        else
        {
            GoldRewardMin = 1;
        }
    }

    public static Enemy GenerateEnemy(SortedDictionary<string, Enemy> allEnemies, int PlayerLevel, Random globalRandom)
    {
        int randomCap = 0;
        SortedDictionary<int, Enemy> enemyPool = new();
        foreach (Enemy enemy in allEnemies.Values)
        {
            if (enemy.EnemyLevel == PlayerLevel)
            {
                enemyPool.Add(enemy.SpawnChance, enemy);
                randomCap += enemy.SpawnChance;
            }
        }
        int randomSelection = globalRandom.Next(1, randomCap + 1);
        int position = 0;
        foreach (KeyValuePair<int, Enemy> enemy in enemyPool)
        {
            position += enemy.Key;
            if (randomSelection <= position)
            {
                return (Enemy)enemy.Value.MemberwiseClone();
            }
        }
        return (Enemy)allEnemies["voidSlime"].MemberwiseClone();
    }
}

class Weapon(string name, int damage, int rarity)
{
    public string WeaponName = name;
    public int WeaponDamage = damage;
    public int WeaponRarity = rarity;
}

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

class PlayGame(Player player1)
{
    public Player player1 = player1;

    public void StartGame(SortedDictionary<string, Weapon> allWeapons)
    {
        Console.WriteLine("Please enter your name");
        player1.PlayerName =Console.ReadLine();
        Console.WriteLine($"Welcome {player1.PlayerName}!\n");
        Console.WriteLine
        (
            "1.Wooden Club\n"+
            "2.Sling\n"+
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
        int enemyDamage = activeEnemy.EnemyAttack;
        int playerDamage = player1.EquippedWeapon.WeaponDamage;

        Console.WriteLine($"\nUH OH! a {activeEnemy.EnemyName} appeared!");
        while (activeEnemy.CurrentHealth > 0 && player1.CurrentHealth > 0)
        {
            Console.WriteLine
            (
                "1. Attack\n" +
                "Please select an action: "
            );
            int userInput = InputHandler.ToInt([1]);
            if (userInput == 1)
            {
                int damageDealt = random.Next(playerDamage - 1, playerDamage + 2);
                activeEnemy.CurrentHealth -= damageDealt;
                Console.WriteLine($"\nYou hit {activeEnemy.EnemyName}, dealing {damageDealt} damage!");
            }
            if (activeEnemy.CurrentHealth > 0)
            {
                int damageTaken = random.Next(enemyDamage - 1, enemyDamage + 2);
                player1.CurrentHealth -= damageTaken;
                Console.WriteLine($"{activeEnemy.EnemyName} hits you, dealing {damageTaken} damage!");
            }
        }
        if (activeEnemy.CurrentHealth <= 0)
        {
            Console.WriteLine($"{activeEnemy.EnemyName} has died!");
            int GoldRecieved = random.Next(activeEnemy.GoldRewardMin, activeEnemy.GoldRewardMax+1);
            player1.Gold += GoldRecieved;
            Console.WriteLine($"You recieved {GoldRecieved} gold!");
            Console.WriteLine($"You have {player1.Gold} gold!");
            return;
        }
        else if (player1.CurrentHealth <= 0)
        {
            Console.WriteLine("You have died...\nGAME OVER!");
            Console.WriteLine("1. New Game [Work In Progress...]\n2. Exit\nPlease select an option:");
            int userInput = InputHandler.ToInt([1, 2]);
            if (userInput == 1)
            {
                string executablePath = Assembly.GetExecutingAssembly().Location;
                Process.Start(executablePath);
                Environment.Exit(0);
            }
            else
            {
                Program.runProgram = false;
            }
        }
        //WRITE CONDITION FOR CHARACTER DEATH...
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
        PlayGame play,
        bool runProgram
    )
    {
        Console.WriteLine
        (
            "\n1. Fight\n"+
            "2. Shop\n"+
            "3. My Character\n"+
            "4. Quit Game"
        );
        var userInput = InputHandler.ToInt([1, 2, 3, 4]);
        if (userInput == 1)
        {
            Enemy combatEnemy = Enemy.GenerateEnemy(allEnemies, player1.PlayerLevel, globalRandom);
            play.CombatRound(combatEnemy, globalRandom);
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
    public static bool runProgram = true;
    static void Main(string[] args)
    {
        Random globalRandom = new();
        var allWeapons = ObjectLoader.LoadWeapons();
        var allEnemies = ObjectLoader.LoadEnemies();
        var player1 = new Player();
        PlayGame play = new(player1);
        play.StartGame(allWeapons);
        while (runProgram)
        {
            Menu.MainMenu(globalRandom, allWeapons, allEnemies, player1, play, runProgram);
        }
        //Play.CombatRound(allEnemies["slime"], globalRandom);
    }
}
