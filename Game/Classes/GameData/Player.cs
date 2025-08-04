using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

public class Player()
{
    public string ?PlayerName = "player";
    public int PlayerLevel = 1;
    public int Gold = 0;
    public Weapon EquippedWeapon = new("fists", 1, 0);
    public int MaxHealth = 10;
    public int CurrentHealth = 10;
    public void GiveWeapon(Weapon weaponReward)
    {
        EquippedWeapon = weaponReward;
        Console.WriteLine($"You have equipped {EquippedWeapon.WeaponName}!");
    }
}
