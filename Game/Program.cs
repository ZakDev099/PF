using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace Game;

class Player(string name)
{
    public string PlayerName = name;
    public int Level = 0;
    public int Gold = 0;
    public Weapon? equippedWeapon;
}

class Weapon(string name, int damage, int accuracy, int block)
{
    public string WeaponName = name;
    public int WeaponDamage = damage;
    public int WeaponAccuracy = accuracy;
    public int WeaponBlock = block;
}
// Add list as return
class ObjectLoader
{
    public static List<Weapon> CreateWeapons()
    {
        Weapon woodenClub = new("Wooden Club", 1, 1, 1);
        Weapon sling = new("Sling", 1, 1, 1);
        var weapons = new List<Weapon>
        {
            {woodenClub},
            {sling}
        };
        return weapons;
    }
}

class Battle
{
    
}

class InputHandler
{
    public static int ToInt()
    {
        int result = 0;
        bool isValidInt = false;

        while (!isValidInt)
        {
            string? userInput = Console.ReadLine();
            if (int.TryParse(userInput, out result))
            {
                Console.WriteLine($"You Entered: {result}");
                isValidInt = true;
            }
            else
            {
                Console.WriteLine("Invalid input, try again.\nPlease enter a number: ");
            }
        }

        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter your name");
        var Player1 = new Player(Console.ReadLine());
        Console.WriteLine($"Welcome {Player1.PlayerName}!");
        Console.WriteLine("1.Wooden Club\n2.Sling\n\nPlease select a starting weapon: ");
        int userInput = InputHandler.ToInt();
        //DEBUG
        Console.WriteLine($"DEBUG:: userInput ={userInput}");
        //
        
    }
}
