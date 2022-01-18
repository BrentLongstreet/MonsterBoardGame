using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardGen;
using MyUtilities;

namespace CharacterGen
{
    class Wraith : Monster , IActor
    {
        bool hostile;
        public int row { get; set; }
        public int col { get; set; }
        public string symbol { get; }
        public int speed { get; }
        public ConsoleColor color { get; }
        public string name { get; set; }
        

        public Wraith(int atk, int hp, int def, int lvl, bool hos, string sym, ConsoleColor c = ConsoleColor.DarkBlue) : base(atk, hp, def, lvl, hos, sym)
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
            name = "Wraith";
        }
        public override void Interact(Board b, IActor a)
        {
            a.health -= attack;
            Utils.Message(name + " attacked" + a.name + " for " + attack + " damage!");
            System.Threading.Thread.Sleep(1500);
        }
        public override void Death(Board b)
        {
            Utils.Message("The " + name + " evaported into thin air", color);
            System.Threading.Thread.Sleep(1000);
            Utils.Message("You gained 5 health! ");
            System.Threading.Thread.Sleep(1000);
        }
        public override void Move(Board b, Player p = null)
        {
            b.board[row, col].occupied = null;
            b.ShowTile(row, col);

            int newRow = row;
            int newCol = col;

            int x = StaticRandom.Instance.Next(0, 4);
            int rndRow = StaticRandom.Instance.Next(2, 4);
            int rndCol = StaticRandom.Instance.Next(2, 4);
            if (x == 0 && b.board[row - rndRow, col].symbol != "#" && b.board[row - rndRow, col].occupied == null)
            {
                if (row > 0)
                    newRow -= rndRow;
            }
            else if (x == 0 && b.board[row - rndRow, col].symbol != "#" && b.board[row - rndRow, col].occupied != null)
            {
                Interact(b, b.board[row - rndRow, col].occupied);
                
            }

            if (x == 1 && b.board[row + rndRow, col].symbol != "#" && b.board[row + rndRow, col].occupied == null)
            {
                if (row < b.height - 2)
                    newRow += rndRow;
            }
            else if (x == 1 && b.board[row + rndRow, col].symbol != "#" && b.board[row + rndRow, col].occupied != null)
            {
                Interact(b, b.board[row + rndRow, col].occupied);
            }
            if (x == 2 && b.board[row, col - rndCol].symbol != "#" && b.board[row, col - rndCol].occupied == null)
            {
                if (col > 0)
                    newCol -= rndCol;
            }
            else if (x == 2 && b.board[row, col - rndCol].symbol != "#" && b.board[row, col - rndCol].occupied != null)
            {
                Interact(b, b.board[row, col - rndCol].occupied);
            }

            if (x == 3 && b.board[row, col + rndCol].symbol != "#" && b.board[row, col + rndCol].occupied == null)
            {
                if (col < b.width - 1)
                    newCol += rndCol;
                    
            }
            else if (x == 3 && b.board[row, col + rndCol].symbol != "#"  && b.board[row, col + rndCol].occupied != null)
            {
                Interact(b, b.board[row, col + rndCol].occupied);
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
