using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Game.Classes;

public class GameData()
{
    public static Player ?Player1;
    public static SortedDictionary<string, Weapon> ?AllWeapons;
    public static SortedDictionary<string, Enemy> ?AllEnemies;
}
