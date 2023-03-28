using System;

namespace MineSweeper
{
    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {   //Playing field
        
        static MineSweeperGame _mineSweeperLogic = new MineSweeperGame();

        /// <summary>
        /// User request for the width of the playing field
        /// </summary>
        /// <returns>width of the playing field if not the user is informed that the input is incorrect and asked again</returns>
        static int UserRequestWidthPlayingField()
        {
            bool correctInput = false;
            int playingFieldWidth = 0;
            while (!correctInput)
            {
                try
                {
                    while (playingFieldWidth <= 0)
                    {
                        Console.WriteLine("Wie breit soll dein Spielfeld werden?");
                        string input = Console.ReadLine();
                        Console.Clear();
                        playingFieldWidth = int.Parse(input);
                    }
                    correctInput = true;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine("Fehlerhafte Eingabe");
                    Console.WriteLine(fe.Message);
                }
            }
            return playingFieldWidth;
        }

        /// <summary>
        /// User request for the height of the playing field
        /// </summary>
        /// <returns>height of the playing field if not the user is informed that the input is incorrect and asked again </returns>
        static int UserRequestHeightPlayingField()
        {
            bool correctInput = false;
            int playingFieldHeight = 0;
            while (!correctInput)
            {
                try
                {
                    while (playingFieldHeight <= 0)
                    {
                        Console.WriteLine("Wie hoch soll dein Spielfeld werden?");
                        string input = Console.ReadLine();
                        Console.Clear();
                        playingFieldHeight = int.Parse(input);
                    }

                    correctInput = true;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Fehlerhafte Eingabe");
                    Console.WriteLine(fe.Message);
                }
            }

            return playingFieldHeight;
        }

        /// <summary>
        /// User request for the amount of bombs in the game
        /// </summary>
        /// <returns>number of bombs in the game played with if not he user is informed that the input is incorrect and asked again how many bombs he wants to play with</returns>
        static int UserRequestBombs()
        {
            bool correctInput = false;
            int amountBombs = 0;

            while (!correctInput)
            {
                try
                {
                    while (amountBombs <= 0 || amountBombs > _mineSweeperLogic.PlayingField.GetLength(0) * _mineSweeperLogic.PlayingField.GetLength(1) - 1)
                    {
                        Console.WriteLine("Mit wie vielen Bomben möchtest du spielen?");
                        string input = Console.ReadLine();
                        Console.Clear();
                        amountBombs = int.Parse(input);
                    }
                    correctInput = true;
                }
                catch (FormatException fe)
                {
                    Console.WriteLine("Fehlerhafte Eingabe");
                    Console.WriteLine(fe.Message);
                }
            }
            return amountBombs;
        }

        /// <summary>
        /// Initialization of the playing field according to the entered height and width with the values 0
        /// </summary>
        /// <param name="playingFieldHeight">Height of the playing field</param>
        /// <param name = "playingFieldWidth" > Width of the playing field</param>
        //static void InitializePlayingField(int playingFieldHeight, int playingFieldWidth)
        //{
        //    _playingField = new int[playingFieldWidth, playingFieldHeight];
        //    _isUncovered = new bool[playingFieldWidth, playingFieldHeight];
        //}

        /// <summary>
        /// Bombs are distributed randomly in the playing field
        /// </summary>
        /// <param name="amountBombs">number of bombs in the game</param>
        //static void PlaceBombs(int amountBombs, int firstXMove, int firstYMove)
        //{
        //    Random rnd = new Random();
        //    int amountBombsAlreadyInGame = 0;

        //    while (amountBombsAlreadyInGame < amountBombs)
        //    {
        //        int x = rnd.Next(0, _playingField.GetLength(0));
        //        int y = rnd.Next(0, _playingField.GetLength(1));

        //        if ((x != firstXMove && y != firstYMove) || _playingField[x, y] != -1)
        //        {
        //            _playingField[x, y] = -1;
        //            amountBombsAlreadyInGame++;
        //        }
        //    }
        //}

        /// <summary>
        /// Displaying the x-axis description
        /// </summary>
        static void DrawXAxisInitial()
        {
            Console.Write("  ");
            for (int width = 0; width < _mineSweeperLogic.PlayingField.GetLength(1); width++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" " + width);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Displaying the y-axis description
        /// </summary>
        /// <param name="identifier">numbers for the axis</param>
        static void DrawYAxisIdentifier(int identifier)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" " + identifier + " ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Drawing the playing field
        /// </summary>
        static void DrawPlayingField()
        {
            Console.Clear();
            DrawXAxisInitial();

            for (int yPosition = 0; yPosition < _mineSweeperLogic.PlayingField.GetLength(0); yPosition++)
            {
                DrawYAxisIdentifier(yPosition);

                for (int xPosition = 0; xPosition < _mineSweeperLogic.PlayingField.GetLength(1); xPosition++)
                {
                    if (_mineSweeperLogic.IsUncovered[yPosition, xPosition])
                        Console.Write(_mineSweeperLogic.PlayingField[yPosition, xPosition] + " ");
                    else
                        Console.Write("X ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// User is prompted to select the coordinates for a cell
        /// </summary>
        /// <param name="xorYCoordinate">X or Y direction</param>
        /// <returns>Coordinates for selected cell</returns>
        static int UserRequestCoordinate(string xorYCoordinate)
        {
            Console.WriteLine();
            Console.WriteLine("Bitte gebe eine Zahl ein für die " + xorYCoordinate + "-Richtung");
            Console.Write(xorYCoordinate + ": ");
            string input = Console.ReadLine();
            int chooseDirection = int.Parse(input);

            return chooseDirection;
        }

        /// <summary>
        /// Revealing the value of the cell
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        //static void UncoverField(int x, int y)
        //{
        //    _isUncovered[y, x] = true;
        //}

        /// <summary>
        /// Displays clues in the playing field as numbers. These numbers indicate how many adjacent cells contain a bomb
        /// </summary>
        //static void SetHints()
        //{
        //    for (int yIndex = 0; yIndex < _playingField.GetLength(0); yIndex++)
        //    {
        //        for (int xIndex = 0; xIndex < _playingField.GetLength(1); xIndex++)
        //        {
        //            if (_playingField[yIndex, xIndex] != -1)
        //            {
        //                CalculateSurroundingBombValueForField(xIndex, yIndex);
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// Checks if the selected cell contains a bomb
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        /// <returns>The selected field is a bomb</returns>
        //static bool FieldIsBomb(int x, int y)
        //{
        //    return _playingField[x, y] == -1;
        //}

        /// <summary>
        /// Checks if the cell is outside the playing field
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        /// <returns>true if cell is on the playing field</returns>
        //static bool CheckIfCellIsOutsideField(int x, int y)
        //{
        //    return x < 0 || x >= _playingField.GetLength(0) || y >= _playingField.GetLength(1) || y < 0;
        //}

        /// <summary>
        /// Increment the bomb count. Checks if the field is out of bounds. The method checks wether a given cell at position (x|y) is a bomb or not
        /// and returns a boolean value accordingly
        /// </summary>
        /// <param name="x">Cordinate for x, index</param>
        /// <param name="y">Cordinate for y, index</param>
        /// <returns>false if the cell is outside the playing field,  true if the field is a bomb</returns>
        //static bool CheckBombIncrement(int x, int y)
        //{
        //    return !CheckIfCellIsOutsideField(x, y) && FieldIsBomb(x, y);
        //}

        /// <summary>
        /// Calculate the surrounding bombvalue for the cell
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        //static void CalculateSurroundingBombValueForField(int x, int y)
        //{

        //    for (int yCoordinate = y - 1; yCoordinate <= y + 1; yCoordinate++)
        //    {
        //        for (int xCoordinate = x - 1; xCoordinate <= x + 1; xCoordinate++)
        //        {
        //            if (CheckBombIncrement(yCoordinate, xCoordinate))
        //                _playingField[y, x]++;
        //        }
        //    }
        //}

        /// <summary>
        /// Uncovering the playing field
        /// </summary>
        //static void UncoverWholePlayingField()
        //{
        //    for (int width = 0; width < _playingField.GetLength(0); width++)
        //    {
        //        for (int height = 0; height < _playingField.GetLength(1); height++)
        //        {
        //            _isUncovered[width, height] = true;
        //        }
        //    }
        //}

        /// <summary>
        /// Checks if the game is finished. The game is either won or lost
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        //static bool WinLoseCheck(int x, int y)
        //{
        //    if (FieldIsBomb(y, x))
        //    {
        //        Console.WriteLine("Du hast verloren :(");
        //        UncoverWholePlayingField();
        //        return false;
        //    }
        //    else if (AllProperFieldsUncovered())
        //    {
        //        Console.WriteLine("Du hast gewonnen");
        //       UncoverWholePlayingField();
        //        return false;
        //    }
        //    else
        //        return true;
        //}

        /// <summary>
        /// Checks the playing field if all cells that are not bombs are uncovered
        /// </summary>
        /// <returns></returns>
        //static bool AllProperFieldsUncovered()
        //{
        //    for (int yIndex = 0; yIndex < _playingField.GetLength(0); yIndex++)
        //    {
        //        for (int xIndex = 0; xIndex < _playingField.GetLength(1); xIndex++)
        //        {
        //            if (!FieldIsBomb(yIndex, xIndex) && !_isUncovered[yIndex, xIndex])
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// Uncover multiple cells that are guaranteed to be safe. When a player clicks on a cell displaying a blank space, a number, or a mine, 
        /// they can use floodfill to reveal all adjacent cells that aren't mines. 
        /// This can save time and increase their chances of success in the game.
        /// </summary>
        /// <param name="x">Cordinate for x</param>
        /// <param name="y">Cordinate for y</param>
        /// <param name="visited">the field has already been viewed</param>
        //static void FloodFill(int x, int y, bool[,] visited)
        //{
        //    if (CheckIfCellIsOutsideField(x, y) || visited[x, y])
        //    {
        //        return;
        //    }

        //    if (FieldIsBomb(y, x))
        //    {
        //        return;
        //    }

        //    visited[x, y] = true;
        //    UncoverField(x, y);

        //    if (_playingField[y, x] == 0)
        //    {
        //        FloodFill(x - 1, y - 1, visited);
        //        FloodFill(x - 1, y, visited);
        //        FloodFill(x - 1, y + 1, visited);
        //        FloodFill(x, y - 1, visited);
        //        FloodFill(x, y + 1, visited);
        //        FloodFill(x + 1, y - 1, visited);
        //        FloodFill(x + 1, y, visited);
        //        FloodFill(x + 1, y + 1, visited);
        //    }
        //}

        /// <summary>
        /// Game process
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            _mineSweeperLogic.InitializePlayingField(UserRequestWidthPlayingField(), UserRequestHeightPlayingField());
            int bombs = UserRequestBombs();
            
            DrawPlayingField();
            int firstXMove = UserRequestCoordinate("X");
            int firstYMove = UserRequestCoordinate("Y");
           
            _mineSweeperLogic.PlaceBombs(bombs, firstXMove, firstYMove);
            _mineSweeperLogic.SetHints();
            bool[,] visited = new bool[_mineSweeperLogic.PlayingField.GetLength(0), _mineSweeperLogic.PlayingField.GetLength(1)];
            _mineSweeperLogic.FloodFill(firstXMove, firstYMove, visited);
            _mineSweeperLogic.UncoverField(firstXMove, firstYMove);
            DrawPlayingField();

            bool running = true;
            while (running)
            {
                int fieldX = UserRequestCoordinate("X");
                int fieldY = UserRequestCoordinate("Y");
                Console.Clear();
                _mineSweeperLogic.FloodFill(fieldX, fieldY, visited);
                _mineSweeperLogic.UncoverField(fieldX, fieldY);
                DrawPlayingField();
                if (_mineSweeperLogic.FieldIsBomb(fieldY, fieldX))
                {
                    Console.WriteLine("Du hast verloren :(");
                    _mineSweeperLogic.UncoverWholePlayingField();
                    running = false;
                }
                else if (_mineSweeperLogic.AllProperFieldsUncovered())
                {
                    Console.WriteLine("Du hast gewonnen");
                    _mineSweeperLogic.UncoverWholePlayingField();
                    running = false;
                }
                else
                    running = true;
            }
        }
    }
}

