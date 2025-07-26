namespace Game;

class Player(string playerName)
{
    string playerName;
    int level = 0;
}

class Weapon
{

}

class Combat
{

}

class Reward
{

}

class UserInput
{
    static int numSelect
    {
        
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter your name");
        Player1 = new Player(Console.ReadLine());
        Console.WriteLine("Welcome " + Player1.playerName + "!\nPlease select a weapon:\n\n1. Copper Broadsword\n2.");
    }
}
