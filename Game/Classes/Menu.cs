using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

class Menu
{
    public static void MainMenu
    (
        GameData GameData,
        GameServices GameServices,
        Event Events
    )
    {
        Console.WriteLine
        (
            "\n1. Fight\n" +
            "2. Shop\n" +
            "3. My Character\n" +
            "4. Quit Game"
        );
        var userInput = GameServices.InputToInt([1, 2, 3, 4]);
        if (userInput == 1)
        {
            Enemy combatEnemy = Enemy.GenerateEnemy(GameData, GameServices);
            Events.CombatRound(GameData.Player, combatEnemy, GameServices.GlobalRandom);
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