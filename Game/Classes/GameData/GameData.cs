using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Game.Classes;

public class GameData(Player player, SortedDictionary<string, Weapon> allWeapons, SortedDictionary<string, Enemy> allEnemies)
{
    public Player Player = player;
    public SortedDictionary<string, Weapon> AllWeapons = allWeapons;
    public SortedDictionary<string, Enemy> AllEnemies = allEnemies;
}
