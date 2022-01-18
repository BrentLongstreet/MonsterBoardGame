using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGen;
using CharacterGen;
using MyUtilities;

namespace CSharpProject
{
    class Program
    {
        static Player player;
        static Board board;
        static List<Monster> monsterList;
        

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 40);
            Console.CursorVisible = false;

            player = new Player(1);
            var arr = new[]


            {
                    @"      __  __  ___   ___. __     __          __  ___ __                            ",
                    @"     |__)|__)|__ |\ || '/__`   |  \|  ||\ |/ _`|__ /  \|\ |                   ",
                    @"     |__)|  \|___| \||  .__/   |__/\__/| \|\__>|___\__/| \|      ",
                    @"                                                              ",
                    @"          ",
                    @"          ",
                    @"          ",
                    @"          ",
                    @"                                                                                        ",
                    @"                                                                                      ",
                    @"                                                                                        ",
            };
            Console.WindowWidth = 160;
            Console.WriteLine("\n\n");
            foreach (string line in arr)
                Console.WriteLine(line);
            Console.WriteLine("Made By: Brent Longstreet");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("SCORE 1000 POINTS TO WIN!");
            Console.WriteLine(Environment.NewLine);
            Console.ReadKey();




            Console.WriteLine(player);
            Console.ReadKey();
            Console.Clear();
            
                    
                
                
            NewLevel(1); // Create level 1
            

            while (true)
            {
                HUD(board, player);
                board.ShowBoard();
                player.Move(board);
                
                int roll = StaticRandom.Instance.Next(1, 120);
                if (player.score >= 1000)
                {
                    Utils.Message("YOU WON!");
                    System.Threading.Thread.Sleep(2000);
                    System.Environment.Exit(1);
                }
                if (roll == 10 )
                {
                    player.health += 5;
                    
                    Utils.Message("You found Energy Drink, + 5 health");
                    System.Threading.Thread.Sleep(1000);

                }
                if (roll == 20 )
                {
                    player.attack += 3;
                    
                    Utils.Message("You found PowerBar, +3 ATTACK");
                    System.Threading.Thread.Sleep(1000);
                    player.score += 50;
                    
                }
                if (roll == 30)
                {
                    player.defense += 1;

                    Utils.Message("You found Toothpaste!");
                    System.Threading.Thread.Sleep(1000);
                    player.score += 10;
                }
                if (roll == 40)
                {
                    player.defense += 1;

                    Utils.Message("You found Hand Sanitizer!");
                    System.Threading.Thread.Sleep(1000);
                    player.score += 10;
                }
                if (roll == 50)
                {
                    player.defense += 1;

                    Utils.Message("You found a Pickle");
                    System.Threading.Thread.Sleep(1000);
                    player.score += 10;
                }
                if (board.board[player.row, player.col].stairsHere)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("You ascend the tower!");
                    Console.ReadKey();
                    NewLevel(board.level+1);
                    continue;
                }

                //Monster movement and death
                for (int i=0; i < monsterList.Count; i++) 
                {
                    if (monsterList[i].health <= 0)
                    {
                        // *** call the monster death method   ***        
                        //Monster.Death(board);
                        // *** remove monster from list. ***
                        monsterList[i].Death(board);
                        monsterList.RemoveAt(i);
                        player.score += 75;
                        continue;
                    }
                    monsterList[i].Move(board, player);
                }

                if (player.health <= 0)
                {
                    player.Death(board);
                    Utils.Message("You are dead.");
                    Console.ReadKey();
                    break;
                }
            }                
        }

        //Generates a new level. Called when stairs are reached
        static void NewLevel(int level)
        {
            board = new Board(60, 30, level);
            monsterList = new List<Monster>(); //reset monster list

            //Adds random monsters to board.
            for (int i = 0; i <= level; i++)
            {
                int x = StaticRandom.Instance.Next(board.level, 50 + board.level);          
                if (x <= 23)
                {
                    int health = StaticRandom.Instance.Next(15, 25 + board.level);
                    int attack = StaticRandom.Instance.Next(4, 8 + board.level);
                    string symbol = "Z";
                    monsterList.Add(new Monster(attack, health, 2, 5, true, symbol));
                }
                else if (x <= 30)
                {
                    int attack = StaticRandom.Instance.Next(7, 10 + board.level);
                    int health = StaticRandom.Instance.Next(30, 50 + (board.level*2));
                    string symbol = "W";
                    monsterList.Add(new Wraith(attack, health, 2, 5, true, symbol));
                }
                else if (x <= 42)
                {
                    int attack = StaticRandom.Instance.Next(7, 10 + board.level);
                    int health = StaticRandom.Instance.Next(30, 50 + (board.level * 2));
                    string symbol = "R";
                    monsterList.Add(new Rhino(attack, health, 5, 5, true, symbol));
                }
                else if (x <= 43)
                {                    
                    int health = StaticRandom.Instance.Next(20, 30 + (board.level * 2));
                    int attack = StaticRandom.Instance.Next(10, 15 + board.level);
                    string symbol = "B";
                    monsterList.Add(new Boss(attack, health, 5, 5, true, symbol));
                }
            }

            board.PlaceActor(player);
            foreach (Monster m in monsterList)
            {
                board.PlaceActor(m);
            }
            board.ShowBoard();
        }

        //Displays right player HUD
        static void HUD(Board b, Player p)
        {
            Console.SetCursorPosition(board.width + 5, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Health: " + player.health.ToString()); Console.Write("    ");
            Console.SetCursorPosition(board.width + 5, 12);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Attack: " + player.attack.ToString()); Console.Write("    ");
            Console.SetCursorPosition(board.width + 5, 14);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Score: " + player.score.ToString()); Console.Write("    ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(board.width + 5, 16);
            Console.Write("DLvl: " + board.level.ToString()); Console.Write("    ");
        }
    }
}
