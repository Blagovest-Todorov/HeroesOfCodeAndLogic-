using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroesOfTheCodeAndLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            int numHeroes = int.Parse(Console.ReadLine());
            Dictionary<string, double> hpPointsByHero = new Dictionary<string, double>();
            Dictionary<string, double> mpPointsByHero = new Dictionary<string, double>();

            for (int i = 0; i < numHeroes; i++)
            {
                string[] heroData = Console.ReadLine().Split(' '); //{hero name} {HP} {MP} 

                string herosName = heroData[0];

                double hitPoints = double.Parse(heroData[1]); //max 100 HP
                double manaPoints = double.Parse(heroData[2]); // max 200 MP

                if (!hpPointsByHero.ContainsKey(herosName))
                {
                    hpPointsByHero.Add(herosName, hitPoints);
                    mpPointsByHero.Add(herosName, manaPoints);
                }
            }

            while (true)
            {
                string lineCommands = Console.ReadLine();

                if (lineCommands == "End" )
                {
                    //ToDo
                    break;
                }

                string[] data = lineCommands.Split(" - ");               

                string currCommand = data[0];
                string heroName = data[1];

                if (currCommand == "CastSpell") //CastSpell – {hero name} – {MP needed} – {spell name} 
                {
                    double neededMP = double.Parse(data[2]);
                    string spellName = data[3];

                    if (mpPointsByHero[heroName] - neededMP >= 0)
                    {
                        mpPointsByHero[heroName] -= neededMP;
                        Console.WriteLine($"{heroName} has successfully cast {spellName} " +
                            $"and now has {mpPointsByHero[heroName]} MP!");
                    }
                    else // if (mpPointsByHero[heroName] - neededMP < 0)
                    {
                        Console.WriteLine($"{heroName} does not have enough MP to cast {spellName}!");
                    }
                }   // TakeDamage – {hero name} – {damage} – {attacker}
                else if (currCommand == "TakeDamage")
                {
                    double damage = double.Parse(data[2]);
                    string attacker = data[3];

                    if (hpPointsByHero[heroName] - damage > 0)
                    {
                        hpPointsByHero[heroName] -= damage;
                        Console.WriteLine($"{heroName} was hit for {damage} HP by {attacker}" +
                            $" and now has {hpPointsByHero[heroName]} HP left!");
                    }
                    else // if (hpPointsByHero[heroName] - damage <= 0
                    {
                        Console.WriteLine($"{heroName} has been killed by {attacker}!");
                        hpPointsByHero.Remove(heroName);
                        mpPointsByHero.Remove(heroName);
                    }
                }   //Recharge – {hero name} – {amount}
                else if (currCommand == "Recharge")
                {
                    double amountToCharge = double.Parse(data[2]);   // max 200 MP//

                    if (amountToCharge >= 200 - mpPointsByHero[heroName])
                    {
                        amountToCharge = 200 - mpPointsByHero[heroName];
                        mpPointsByHero[heroName] += amountToCharge;
                    }
                    else // if (amountToCharge < 200 - mpPointsByHero[heroName])
                    {
                        mpPointsByHero[heroName] += amountToCharge;
                    }

                    Console.WriteLine($"{heroName} recharged for {amountToCharge} MP!");
                } //Heal – {hero name} – {amount}
                else // if (currCommand == "Heal")
                {
                    double amountToChargeHP = double.Parse(data[2]);

                    if (amountToChargeHP >= 100 - hpPointsByHero[heroName])
                    {
                        amountToChargeHP = 100 - hpPointsByHero[heroName];
                        hpPointsByHero[heroName] += amountToChargeHP;
                    }
                    else // if (amountToChargeHP < 100 - hpPointsByHero[heroName])
                    {
                        hpPointsByHero[heroName] += amountToChargeHP;
                    }

                    Console.WriteLine($"{heroName} healed for {amountToChargeHP} HP!"); 
                }
            }

            Dictionary<string, double> sortedHpPointsByHero = hpPointsByHero
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Value);

            foreach (var kvp in sortedHpPointsByHero)
            {
                string heroName = kvp.Key;
                double currHP = kvp.Value;
                Console.WriteLine($"{heroName}");
                Console.WriteLine($"  HP: {currHP}");
                Console.WriteLine($"  MP: {mpPointsByHero[heroName]}");
            }
        }
    }
}
