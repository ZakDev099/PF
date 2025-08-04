using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

class Event()
{
    public void StartGame(Player player)
    {
        Console.WriteLine("Please enter your name");
        player.PlayerName = Console.ReadLine();
        Console.WriteLine($"Welcome {player.PlayerName}!\n");
        /*
        Console.WriteLine
        (
            "1.Wooden Club\n" +
            "2.Sling\n" +
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
        */
    }
    public void CombatRound(Player player, Enemy activeEnemy, Random random)
    {
        int enemyDamage = activeEnemy.EnemyAttack;
        int playerDamage = player.EquippedWeapon.WeaponDamage;

        Console.WriteLine($"\nUH OH! a {activeEnemy.EnemyName} appeared!");
        while (activeEnemy.CurrentHealth > 0 && player.CurrentHealth > 0)
        {
            Console.WriteLine
            (
                "1. Attack\n" +
                "Please select an action: "
            );
            int userInput = GameServices.InputToInt([1]);
            if (userInput == 1)
            {
                int damageDealt = random.Next(playerDamage - 1, playerDamage + 2);
                activeEnemy.CurrentHealth -= damageDealt;
                Console.WriteLine($"\nYou hit {activeEnemy.EnemyName}, dealing {damageDealt} damage!");
            }
            if (activeEnemy.CurrentHealth > 0)
            {
                int damageTaken = random.Next(enemyDamage - 1, enemyDamage + 2);
                player.CurrentHealth -= damageTaken;
                Console.WriteLine($"{activeEnemy.EnemyName} hits you, dealing {damageTaken} damage!");
            }
        }
        if (activeEnemy.CurrentHealth <= 0)
        {
            Console.WriteLine($"{activeEnemy.EnemyName} has died!");
            int GoldRecieved = random.Next(activeEnemy.GoldRewardMin, activeEnemy.GoldRewardMax + 1);
            player.Gold += GoldRecieved;
            Console.WriteLine($"You recieved {GoldRecieved} gold!");
            Console.WriteLine($"You have {player.Gold} gold!");
            return;
        }
        else if (player.CurrentHealth <= 0)
        {
            Console.WriteLine("You have died...\nGAME OVER!");
            Console.WriteLine("1. New Game [Work In Progress...]\n2. Exit\nPlease select an option:");
            int userInput = GameServices.InputToInt([1, 2]);
            if (userInput == 1)
            {
                //WORK IN PROGRESS:
                //string executablePath = Assembly.GetExecutingAssembly().Location;
                //Process.Start(executablePath);
                //Environment.Exit(0);
                Program.runProgram = false;
            }
            else
            {
                Program.runProgram = false;
            }
        }
        //WRITE CONDITION FOR CHARACTER DEATH...
    }
}