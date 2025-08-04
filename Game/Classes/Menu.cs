using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

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
            "\n1. Fight\n" +
            "2. Shop\n" +
            "3. My Character\n" +
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
            //Shop function call goes here...
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