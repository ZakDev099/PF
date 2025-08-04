using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

public class Weapon(string name, int damage, int dropChance)
{
    public string WeaponName = name;
    public int WeaponDamage = damage;
    public int WeaponDropChance = dropChance;
}