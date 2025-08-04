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
    }
}
