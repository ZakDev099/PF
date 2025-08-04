using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Classes;

class Enemy
{
    public string EnemyName;
    public int EnemyLevel;
    public int MaxHealth;
    public int CurrentHealth;
    public int EnemyAttack;
    public int SpawnChance;
    public int GoldRewardMax;
    public int GoldRewardMin;

    public Enemy(string name, int level, int health, int attack, int spawnChance, int gold)
    {
        EnemyName = name;
        EnemyLevel = level;
        MaxHealth = health;
        CurrentHealth = health;
        EnemyAttack = attack;
        SpawnChance = spawnChance;
        GoldRewardMax = gold + 5;
        if (gold - 5 >= 1)
        {
            GoldRewardMin = gold - 5;
        }
        else
        {
            GoldRewardMin = 1;
        }
    }

    public static Enemy GenerateEnemy(SortedDictionary<string, Enemy> allEnemies, int PlayerLevel, Random globalRandom)
    {
        int randomCap = 0;
        SortedDictionary<int, Enemy> enemyPool = new();
        foreach (Enemy enemy in allEnemies.Values)
        {
            if (enemy.EnemyLevel == PlayerLevel)
            {
                enemyPool.Add(enemy.SpawnChance, enemy);
                randomCap += enemy.SpawnChance;
            }
        }
        int randomSelection = globalRandom.Next(1, randomCap + 1);
        int position = 0;
        foreach (KeyValuePair<int, Enemy> enemy in enemyPool)
        {
            position += enemy.Key;
            if (randomSelection <= position)
            {
                return (Enemy)enemy.Value.MemberwiseClone();
            }
        }
        return (Enemy)allEnemies["voidSlime"].MemberwiseClone();
    }
}
