using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGen;
using MyUtilities;

namespace CharacterGen
{
    class Monster : GameCharacter, IActor
    {
        bool hostile;
        public int row { get; set; }
        public int col { get; set; }
        public string symbol { get; }
        public int speed { get; }
        public ConsoleColor color { get; }
        public string name { get; set; }
        
        public Monster(int atk, int hp, int def, int lvl, bool hos, string sym, ConsoleColor c = ConsoleColor.White) 
        {
            attack = atk;
            health = hp;
            defense = def;
            level = lvl;
            hostile = hos;
            row = 0;
            col = 0;
            symbol = sym;
            color = c;
            name = "Monster";
        }
        
        public override string ToString()
        {
            string returnString = "";
            returnString += "Health: " + health + Environment.NewLine;
            returnString += "Level: " + level + Environment.NewLine;
            if (hostile)
            {
                returnString += "The monster is eyeing you aggressively.";
            }
            else
            {
                returnString += "The monster is not interested in you.";
            }
            return returnString;
            
        }

        public virtual void Death(Board b)
        {
            Utils.Message("The "+name+" breathes its final breath!",color);
            System.Threading.Thread.Sleep(1000);
            Utils.Message("You gained 5 health! ");
            System.Threading.Thread.Sleep(1000);
        }

        public virtual void Interact(Board b, IActor a)
        {
            
            a.health -= attack;
            Utils.Message( name + " attacked"+ a.name+" for " + attack + " damage!");
            System.Threading.Thread.Sleep(1500);
            
        }

        //Default move... to move randomly
        public virtual void Move(Board b, Player p = null)
        {
            b.board[row, col].occupied = null;
            b.ShowTile(row, col);

            int newRow = row;
            int newCol = col;

            int x = StaticRandom.Instance.Next(0, 4);
            if (x == 0 && b.board[row-1, col].symbol != "#" && b.board[row -1, col].occupied == null)
            {
                if (row > 0)
                    newRow -= 1;
            }
            else if (x == 0 && b.board[row - 1, col].symbol != "#" && b.board[row - 1, col].occupied != null)
            {
                Interact(b, b.board[row - 1, col].occupied);
            }
            if (x == 1 && b.board[row + 1, col].symbol != "#" && b.board[row + 1, col].occupied == null)
            {
                if (row < b.height - 1)
                    newRow += 1;
            }
            else if (x == 1 && b.board[row + 1, col].symbol != "#" && b.board[row + 1, col].occupied != null)
            {
                Interact(b, b.board[row + 1, col].occupied);
            }
            if (x == 2 && b.board[row, col-1].symbol != "#" && b.board[row, col - 1].occupied == null)
            {
                if (col > 0)
                    newCol -= 1;
            }
            else if (x == 2 && b.board[row, col - 1].symbol != "#" && b.board[row, col - 1].occupied != null)
            {
                Interact(b, b.board[row, col - 1].occupied);
            }

            if (x == 3 && b.board[row, col+1].symbol != "#" && b.board[row,col+1].occupied == null)
            {
                if (col < b.width - 1)
                    newCol += 1;
                        
            }
            else if (x == 3 && b.board[row, col + 1].symbol != "#" && b.board[row, col + 1].occupied != null)
            {
                Interact(b, b.board[row, col + 1].occupied);
            }

                //determine results of command - see if occupied 
            //if (b.board[newRow, newCol].occupied != null)
            //{
                //*** Logic for actor interaction here ***
                
            //}
            else
            {
                row = newRow;
                col = newCol;
            }

            b.board[row, col].occupied = this;
            b.board[row, col].occupied.health = health;
            b.board[row, col].occupied.defense = defense;
            b.ShowTile(row, col);
                        
        }
    } 
}
