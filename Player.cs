using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGen;
using MyUtilities;

namespace CharacterGen
{
    class Player : GameCharacter, IActor
    {
        public int score { get; set; }
        public string history { get; }
        public int row { get; set; }
        public int col { get; set; }
        public string symbol { get; }
        public int speed { get; }
        public ConsoleColor color { get; }
        public string name { get; set; }

        public Player(int lvl)
        {
            health = GenHealth();
            attack = GenAttack();
            defense = GenDefense();
            level = lvl;
            row = 0;
            col = 0;
            name = GenName();
            history = GenHistory();
            symbol = "@";
            color = ConsoleColor.Red;
        }
        public static string GenName()
        {
            string[] First = new string[] { "Tommy", "Billy", "Gregory", "Jerm", "Pickle" };
            string[] Second = new string[] { "Leema", "Rascal", "Thompson", "Ostrich", "Plunner" };
            string[] prefix = new string[] { "Mr", "Brother", "Captain", "Prince", "Dr", "Commander" };
            string[] suffix = new string[] { "Jr", "Sr", "III", "II" };



            int syl1 = StaticRandom.Instance.Next(0, First.Length);
            int syl2 = StaticRandom.Instance.Next(0, Second.Length);
            int pre = StaticRandom.Instance.Next(0, prefix.Length);
            int suf = StaticRandom.Instance.Next(0, suffix.Length);
            int health = StaticRandom.Instance.Next(20, 40);
            int attack = StaticRandom.Instance.Next(20, 50);
            string name = prefix[pre] + " " + First[syl1] + " " + Second[syl2] + " " + suffix[suf];
            return name;
        }
        public static string GenHistory()
        {
            string[] History1 = new string[] {  "You are a magical beast who only comes out at night!",
                                                 "You are the royale king of Enland!",
                                                    "You are the founder of the flat earth commity!",
                                                        "You are the president of the United States!",
                                                            "You are the owner of Google but share all of your wealth with Bernie Sanders." };


            string[] History2 = new string[] {"When you were a young lad you invented a new weaponary hot dog machine.",
                                                "When you become a teenager you invested all your money into a new tezla flamethrower.",
                                                    "As an old man you enjoy drinking a cold pack of brewskies and watching football.",
                                                        "You spend most of your day fishing and watching politics.",
                                                            "Your dream is to fly to Mars and be the first human to live there.",
                                                                "You're a professional bowler who only has one finger."};


            string[] History3 = new string[] { "In the future you believe we will have flying scooters.",
                "You will one day become a new SoundCloud rapper.",
                "You have a cat addiction and own 87 cats.",
                "After thousands of hours in Call of Duty you became a pro player.",
                "In the works of making a brand new popup console that has infinite power.",
                "You are being hunted by the F.B.I. for having evidence of the faked moon landing."};


            int sen = StaticRandom.Instance.Next(0, History1.Length);
            int sen2 = StaticRandom.Instance.Next(0, History2.Length);
            int sen3 = StaticRandom.Instance.Next(0, History3.Length);

            string history = History1[sen] + Environment.NewLine + History2[sen2] + Environment.NewLine + History3[sen3] + Environment.NewLine;
            return history;

        }
        public static int GenAttack()
        {
            int total = 0;

            for (int i = 0; i < 3; i++)
            {
                total += StaticRandom.Instance.Next(4, 7);
            }
            return total;
        }
        public static int GenHealth()
        {
            int total = 0;

            for (int i = 0; i < 5; i++)
            {
                total += StaticRandom.Instance.Next(4, 7);
            }
            return total;
        }
        public static int GenDefense()
        {
            int total = 0;

            for (int i = 0; i < 3; i++)
            {
                total += StaticRandom.Instance.Next(1, 7);
            }
            return total;
        }

        public override string ToString()
        {
            string returnString = "";
            returnString += "Name: " + name + Environment.NewLine;
            returnString += history + Environment.NewLine;
            returnString += "Health: " + health + Environment.NewLine;
            returnString += "Level: " + level + Environment.NewLine;
            returnString += "Attack: " + attack + Environment.NewLine;
            returnString += "Health: " + health + Environment.NewLine;
            returnString += "Defense: " + defense + Environment.NewLine;
            return returnString;
        }

        public void Death(Board b)
        {
            Utils.Message("Your wounds have become too greivous...", ConsoleColor.Red);
            System.Threading.Thread.Sleep(3500);
            Utils.Message("Everything begins to fade...", ConsoleColor.DarkRed);
            Console.ReadKey();
        }

        //What happens when the player moves into an actor
        public void Interact(Board b, IActor a)
        {
            a.health -= attack - a.defense;
            Utils.Message("You attack the "+ a.name + " for " + attack + " damage!");
            System.Threading.Thread.Sleep(1500);
            Utils.Message("Monster Health: " + a.health);
            System.Threading.Thread.Sleep(1000);
        }        

        //Handles commands for Player
        public void Move(Board b, Player p = null)
        {            
            Boolean moved = false; //flag for exiting move loop. Allows for the player to do things besides move and it won't trigger an enemy turn.
            while (moved == false)
            {
                //get user command
                Utils.Message("Command:");
                ConsoleKeyInfo k = Console.ReadKey(true);
                char input = char.ToUpper(k.KeyChar);                

                //init new row and col so we can adjust and validate before assigning
                int newRow = row;
                int newCol = col;
                //move if not wall or occupied                
                if (k.Key == ConsoleKey.NumPad1)
                {
                    newRow += 1; newCol -= 1;
                }
                else if (k.Key == ConsoleKey.NumPad2 || k.Key == ConsoleKey.S || k.Key == ConsoleKey.DownArrow)
                {                   
                    newRow += 1;                   
                }
                else if (k.Key == ConsoleKey.NumPad3)
                {
                    newRow += 1; newCol += 1;
                }
                else if (k.Key == ConsoleKey.NumPad4 || k.Key == ConsoleKey.A || k.Key == ConsoleKey.LeftArrow)
                {             
                    newCol -= 1;                  
                }
                else if (k.Key == ConsoleKey.NumPad6 || k.Key == ConsoleKey.D || k.Key == ConsoleKey.RightArrow)
                {                   
                    newCol += 1;                   
                }
                else if (k.Key == ConsoleKey.NumPad7)
                {
                    newRow -= 1; newCol -= 1;
                }
                else if (k.Key == ConsoleKey.NumPad8 || k.Key == ConsoleKey.W || k.Key == ConsoleKey.UpArrow)
                {
                    newRow -= 1;
                }
                else if (k.Key == ConsoleKey.NumPad9)
                {
                    newRow -= 1; newCol += 1;
                }
                else if (k.Key == ConsoleKey.H)
                {
                    Console.Clear();
                    Utils.Message(history);
                    System.Threading.Thread.Sleep(3500);
                    Console.Clear();
                    
                }
                else //no valid command entered
                {
                    continue; 
                }

                //determine results of command - see if occupied or wall before moving
                if (b.board[newRow,newCol].occupied != null && b.board[newRow, newCol].occupied != this)
                {
                    //*** Logic for actor interaction here ***
                    Interact(b, b.board[newRow, newCol].occupied);
                    moved = true;
                }
                else if (b.board[newRow, newCol].symbol == "#")
                {
                    Console.SetCursorPosition(0, b.height + 1);
                    Console.Write("That is a wall...");
                    Console.ReadKey();
                }
                else
                {                    
                    moved = true;
                }

                if (moved) //this is everything that triggers when a player moves
                {
                    //remove player from current location and redisplay tile
                    b.board[row, col].occupied = null;
                    b.ShowTile(row, col);

                    //add player to new location
                    row = newRow;
                    col = newCol;                    
                    b.board[row, col].occupied = this;
                    b.ShowTile(row, col);                 
                }

            }

        }
    }
}
