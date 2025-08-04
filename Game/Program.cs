using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Game.Classes;

namespace Game;

class Program
{
    public static bool runProgram = true;
    static void Main(string[] args)
    {
        GameData GameData = new(new Player(), ObjectLoader.LoadWeapons(), ObjectLoader.LoadEnemies());
        GameServices GameServices = new(new Random());
        Event Events = new();
        Events.StartGame(GameData.Player);
        while (runProgram)
        {
            Menu.MainMenu(GameData, GameServices, Events);
        }
    }
}
