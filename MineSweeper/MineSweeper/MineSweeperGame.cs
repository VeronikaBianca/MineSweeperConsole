using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    internal class MineSweeperGame
    {
        public int[,] PlayingField;
        public bool[,] IsUncovered;

        public MineSweeperGame()
        {                     
        }

        /// <summary>
        /// Initialization of the playing field according to the entered height and width 
        /// </summary>
        /// <param name="playingFieldHeight">Height of the playing field</param>
        /// <param name="playingFieldWidth">Width of the playing field</param>
        public void InitializePlayingField(int playingFieldHeight, int playingFieldWidth)
        {
            PlayingField = new int[playingFieldWidth, playingFieldHeight];
            IsUncovered = new bool[playingFieldWidth, playingFieldHeight];
        }

        /// <summary>
        /// Bombs are distributed randomly in the playing field
        /// </summary>
        /// <param name="amountBombs">number of bombs in the game</param>
        public void PlaceBombs(int amountBombs, int firstXMove, int firstYMove)
        {
            Random rnd = new Random();
            int amountBombsAlreadyInGame = 0;

            while (amountBombsAlreadyInGame < amountBombs)
            {
                int x = rnd.Next(0, PlayingField.GetLength(0));
                int y = rnd.Next(0, PlayingField.GetLength(1));

                if ((x != firstXMove && y != firstYMove) || PlayingField[x, y] != -1)
                {
                    PlayingField[x, y] = -1;
                    amountBombsAlreadyInGame++;
                }
            }
        }

        /// <summary>
        /// Revealing the value of the cell
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        public void UncoverField(int x, int y)
        {
            IsUncovered[y, x] = true;
        }

        /// <summary>
        /// Displays clues in the playing field as numbers. These numbers indicate how many adjacent cells contain a bomb
        /// </summary>
        public void SetHints()
        {
            for (int yIndex = 0; yIndex < PlayingField.GetLength(0); yIndex++)
            {
                for (int xIndex = 0; xIndex < PlayingField.GetLength(1); xIndex++)
                {
                    if (PlayingField[yIndex, xIndex] != -1)
                    {
                        CalculateSurroundingBombValueForField(xIndex, yIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the selected cell contains a bomb
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        /// <returns>The selected field is a bomb</returns>
        public bool FieldIsBomb(int x, int y)
        {
            return PlayingField[x, y] == -1;
        }

        /// <summary>
        /// Checks if the cell is outside the playing field
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        /// <returns>true if cell is on the playing field</returns>
        private bool CheckIfCellIsOutsideField(int x, int y)
        {
            return x < 0 || x >= PlayingField.GetLength(0) || y >= PlayingField.GetLength(1) || y < 0;
        }

        /// <summary>
        /// Checks if the field is out of bounds. The method checks wether a given cell at position (x|y) is a bomb or not
        /// and returns a boolean value accordingly
        /// </summary>
        /// <param name="x">Cordinate for x, index</param>
        /// <param name="y">Cordinate for y, index</param>
        /// <returns>false if the cell is outside the playing field,  true if the field is a bomb</returns>
       private bool CheckBombIncrement(int x, int y)
        {
            return !CheckIfCellIsOutsideField(x, y) && FieldIsBomb(x, y);
        }

        /// <summary>
        /// Calculate the surrounding bombvalue for the cell
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        private void CalculateSurroundingBombValueForField(int x, int y)
        {

            for (int yCoordinate = y - 1; yCoordinate <= y + 1; yCoordinate++)
            {
                for (int xCoordinate = x - 1; xCoordinate <= x + 1; xCoordinate++)
                {
                    if (CheckBombIncrement(yCoordinate, xCoordinate))
                        PlayingField[y, x]++;
                }
            }
        }

        /// <summary>
        /// Uncovering the playing field
        /// </summary>
        public void UncoverWholePlayingField()
        {
            for (int width = 0; width < PlayingField.GetLength(0); width++)
            {
                for (int height = 0; height < PlayingField.GetLength(1); height++)
                {
                    IsUncovered[width, height] = true;
                }
            }
        }

        /// <summary>
        /// Checks the playing field if all cells that are not bombs are uncovered
        /// </summary>
        /// <returns></returns>
        public bool AllProperFieldsUncovered()
        {
            for (int yIndex = 0; yIndex < PlayingField.GetLength(0); yIndex++)
            {
                for (int xIndex = 0; xIndex < PlayingField.GetLength(1); xIndex++)
                {
                    if (!FieldIsBomb(yIndex, xIndex) && !IsUncovered[yIndex, xIndex])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Uncover multiple cells that are guaranteed to be safe. When a player clicks on a cell displaying a blank space, a number, or a mine, 
        /// they can use floodfill to reveal all adjacent cells that aren't mines. 
        /// This can save time and increase their chances of success in the game.
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        /// <param name="visited">the field has already been viewed</param>
        public void FloodFill(int x, int y, bool[,] visited)
        {
            if (CheckIfCellIsOutsideField(x, y) || visited[x, y])
            {
                return;
            }

            if (FieldIsBomb(y, x))
            {
                return;
            }

            visited[x, y] = true;
            UncoverField(x, y);

            if (PlayingField[y, x] == 0)
            {
                FloodFill(x - 1, y - 1, visited);
                FloodFill(x - 1, y, visited);
                FloodFill(x - 1, y + 1, visited);
                FloodFill(x, y - 1, visited);
                FloodFill(x, y + 1, visited);
                FloodFill(x + 1, y - 1, visited);
                FloodFill(x + 1, y, visited);
                FloodFill(x + 1, y + 1, visited);
            }
        }


    }

}
