using System;
using System.Threading;

namespace ConsoleGame
{
    public partial class Program
    {
        static void DrawLevel(string[] GameLevel)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (string line in GameLevel)
            {
                foreach (char c in line)
                {
                    if (c == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("@");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (c == '$')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("$");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (c == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("O");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (c == '^')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write('^');
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
                Console.Write("\n");
            }
        }

        #region checks
        static bool LevelTeleportQ(string[] GameLevel, int PosX, int PosY)
        {
            string ActualLine = GameLevel[PosY];

            if (ActualLine[PosX] == '@')
                return true;

            return false;
        }
       
        static bool LevelComplete( string[] Level, int PosX, int PosY)
        {
            string ActualLine = Level[PosY];

            if (ActualLine[PosX] == 'O' || ActualLine[PosX] == '@')
                return true;

            return false;
        }

        static bool CanPlayerMove(string[] GameLevel, int PosX, int PosY)
        {
            if (PosX < 0 || PosY < 0 || PosY > GameLevel.Length - 1)
                return false;

            string ActualLine = GameLevel[PosY];

            if (ActualLine[PosX] == ' ' || ActualLine[PosX] == '@' || ActualLine[PosX] == '$' || ActualLine[PosX] == 'O')
                return true;

            return false;
        }
        #endregion

        #region fields
        static int posX;
        static int posY;
        static int currentLevel;
        static int gameState;
        public enum Directions
        {
            Left,
            Up,
            Right,
            Down,
            Unknown
        }
        static Directions playerDirection = Directions.Unknown;
        #endregion

        static bool Movement(string[] Map)
        {
            Console.SetCursorPosition(posX, posY);
            Console.Write(" ");

            switch (playerDirection)
            {
                case Directions.Down:
                    if (CanPlayerMove(Map, posX, posY + 1))
                        posY++;
                    break;

                case Directions.Up:
                    if (CanPlayerMove(Map, posX, posY - 1))
                        posY--;
                    break;

                case Directions.Left:
                    if (CanPlayerMove(Map, posX - 1, posY))
                        posX--;
                    break;

                case Directions.Right:
                    if (CanPlayerMove(Map, posX + 1, posY))
                        posX++;
                    break;
            }

            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#");
            Console.ResetColor();

            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        playerDirection = Directions.Down;                        
                        break;
                    case ConsoleKey.UpArrow:
                        playerDirection = Directions.Up;
                        break;
                    case ConsoleKey.LeftArrow:
                        playerDirection = Directions.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        playerDirection = Directions.Right;
                        break;
                    case ConsoleKey.Escape:
                        return false;
                }
            }
            return true;
        }

        static void ShowCongratulations()
        {
            Console.Clear();
            Console.WriteLine("\n    Congratulations! You have completed level {0}.", gameState);
            Console.WriteLine("               Press spacebar to continue");
            Console.ForegroundColor = ConsoleColor.Black;
            while (Console.ReadKey(false).Key != ConsoleKey.Spacebar) ;
        }

        static void ShowSecretCongratulations()
        {
            Console.Clear();
            Console.WriteLine("\n You crazy madlad, you did it.");
            Thread.Sleep(2000);
            Console.WriteLine("Nice.");
            Thread.Sleep(1000);
            Console.Clear();
            Thread.Sleep(1000);
        }

        static void AnnounceLevel()
        {
            Console.Clear();
            DrawLevel(Doors);
            Thread.Sleep(2000);
            Console.Clear();
        }

        static void Main(string[] args)
        {            
            Console.WriteLine("\n             ========== WELCOME TO THE GAME ===========\n");
            Console.WriteLine("             You are the # sign. Move by pressing arrow keys.");
            Console.WriteLine("        Go into red teleports on the main map to teleport to levels.");
            Console.WriteLine("Collect yellow coins inside levels to progress though them and unlock other levels.");
            Console.WriteLine("                   The rest of the game remains a secret.");
            Console.WriteLine("            You can exit the game anytime by pressing ESC button.");
            Console.WriteLine("\n              ======== PRESS ANY KEY TO CONTINUE =======");
            Thread.Sleep(1000);
            Console.ReadKey();

            bool isGameRunning = true;
            currentLevel = -1;
            Console.CursorVisible = false;
            gameState = 1 ;

            while (isGameRunning)
            {
                               
                switch (currentLevel)
                {
                    default:
                        posX = 2;
                        posY = 12;
                        currentLevel = 0;
                        playerDirection = Directions.Unknown;

                        if(gameState == 2)
                        {
                            GameWorld[6] = "*               *****-*****                 ***** *****                      *";
                            Doors[7] = @"___|______|____!.,.!,.!,!| |_|___2___|_| |,!,.!.,.!..__|_____|_____";
                        }
                        else if(gameState == 3)
                        {
                            GameWorld[6] = "*               *****-*****                 *****-*****                      *";
                            GameWorld[18] = "*               ***** *****                 *****-*****                      *";
                            Doors[7] = @"___|______|____!.,.!,.!,!| |_|___3___|_| |,!,.!.,.!..__|_____|_____";
                        }
                        else if(gameState == 4)
                        {
                            GameWorld[6] = "*               *****-*****                 *****-*****                      *";
                            GameWorld[18] = "*               *****-*****                 ***** *****                      *";
                            Doors[7] = @"___|______|____!.,.!,.!,!| |_|___4___|_| |,!,.!.,.!..__|_____|_____";
                        }
                        else if(gameState == 5)
                        {
                            GameWorld[6] = "*               *****-*****                 *****-*****                      *";
                            GameWorld[18] = "*               *****-*****                 *****-*****                      *";
                            GameWorld[12] = "*                                                               @      *     *";
                            Doors[6] = @"   |      |    |   |`-,!_|_|_| FINAL |_|_||.!-;'  |    |     |     ";
                            Doors[7] = @"___|______|____!.,.!,.!,!| |_|_LEVEL_|_|_|_| |,!,.!.,.!..__|_____|_____";
                        }
                        else if(gameState == 6)
                        {
                            GameWorld[4] = "*               *         *                 *         *                      *";
                            GameWorld[6] = "*               *****-*****                 *****-*****                      *";
                            GameWorld[12] = "*                            @                           |             *     *";
                            GameWorld[20] = "*               *         *                 *         *                      *";
                        }
                        else if(gameState == 7)
                        {
                            Doors[4] = @"   |``'..__ |    |`';.| !|               |'| _!-|   |   _|..-|'    ";
                            Doors[5] = @"   |      |``--..|_ | `;!|Congratulations|.'|   |_..!-'|     |     ";
                            Doors[6] = @"   |      |    |   |`-,!_|               ||.!-;'  |    |     |     ";
                            Doors[7] = @"___|______|____!.,.!,.!,!|               |,!,.!.,.!..__|_____|_____";
                            Doors[8] = @"      |     |    |  |  | |               || |   |   |    |      |  ";
                            Doors[9] = @"      |     |    |..!-''||               | |`-..|   |    |      |  ";
                            Doors[10]= @"      |    _!.-|'  | _!,'|               ||!._|  `'-!.._ |      |  ";
                            Doors[11]= @"     _!.-'|    | _.'|  !||               |`.| `-._|    |``-.._  |  ";
                            Doors[12]= @"..-|'     |  _.''|  !-| !|               |.|`-. | ``._ |     |``'..";
                            Doors[13]= @"   |      |.|    |.|  !| |  You escaped  ||`. |`!   | `'.    |     ";
                            Doors[14]= @"   |  _.-'  |  .'  |.' |/|   the maze.   |! |`!  `,.|    |-._|     ";
                            Doors[18]= @"     .'   | .'   |/|  /    Press any key    \ |`!   |`.|    `.  |  ";
                            Doors[19]= @"  _.'     !'|   .' | /      to walk out      \|  `  |  `.    |`.|  ";


                            Console.Clear();
                            DrawLevel(Doors);
                            Thread.Sleep(10);
                            Console.ForegroundColor = ConsoleColor.Black;
                            //while (Console.ReadKey(false).Key != ConsoleKey.Spacebar)
                            Console.ReadKey();
                            
                            isGameRunning = false;
                        }
                        DrawLevel(GameWorld);

                        break;
                        
                    case 0:
                        {
                            if (!LevelTeleportQ(GameWorld, posX, posY))
                                isGameRunning = Movement(GameWorld);
                            else if (gameState == 1)
                            {
                                AnnounceLevel();
                                currentLevel = 1;
                                playerDirection = Directions.Unknown;
                                posX = 2;
                                posY = 5;
                                DrawLevel(Level1);
                            }
                            else if (gameState == 2)
                            {
                                AnnounceLevel();
                                currentLevel = 2;
                                playerDirection = Directions.Unknown;
                                posX = 28;
                                posY = 18;
                                DrawLevel(Level2);
                            }
                            else if (gameState == 3)
                            {
                                AnnounceLevel();
                                currentLevel = 3;
                                playerDirection = Directions.Unknown;
                                posX = 2;
                                posY = 21;
                                DrawLevel(Level3);
                            }
                            else if (gameState == 4)
                            {
                                AnnounceLevel();
                                currentLevel = 4;
                                playerDirection = Directions.Unknown;
                                posX = 70;
                                posY = 3;
                                DrawLevel(Level4);
                            }       
                            else if (gameState == 5)
                            {
                                AnnounceLevel();
                                currentLevel = 5;
                                playerDirection = Directions.Unknown;
                                posX = 1;
                                posY = 29;  
                                DrawLevel(FinalLevel);
                            }
                            else if (gameState == 6)
                            {
                                Console.Clear();
                                currentLevel = 6;
                                playerDirection = Directions.Unknown;
                                posX = 9;
                                posY = 2;
                                DrawLevel(SecretLevel);
                            }
                        }
                        break;
                    
                    case 1:
                        {
                            isGameRunning = Movement(Level1);
                            if (LevelComplete(Level1, posX, posY))
                            {
                                ShowCongratulations();
                                currentLevel = -1;
                                gameState = 2;
                            }
                        }
                        break;

                    case 2:
                        {
                            isGameRunning = Movement(Level2);
                            if (LevelComplete(Level2, posX, posY))
                            {
                                ShowCongratulations();
                                currentLevel = -1;
                                gameState = 3;
                            }                            
                        }
                        break;

                    case 3:
                        {
                            isGameRunning = Movement(Level3);
                            if (LevelComplete(Level3, posX, posY))
                            {
                                ShowCongratulations();
                                currentLevel = -1;
                                gameState = 4;
                            }
                        }
                        break;

                    case 4:
                        {
                            isGameRunning = Movement(Level4);
                            if (LevelComplete(Level4, posX, posY))
                            {
                                ShowCongratulations();
                                currentLevel = -1;
                                gameState = 5;
                            }
                        }
                        break;

                    case 5:
                        {
                            isGameRunning = Movement(FinalLevel);
                            if (LevelComplete(FinalLevel, posX, posY))
                            {
                                ShowCongratulations();

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Clear();
                                Thread.Sleep(1000);
                                Console.WriteLine("\n it's not over yet");
                                Thread.Sleep(1000);
                                Console.Clear();
                                Thread.Sleep(2000);
                                Console.WriteLine("\n there's one last challenge left to do");
                                Thread.Sleep(3000);
                                Console.Clear();
                                Thread.Sleep(1000);
                                Console.WriteLine("\n Good luck.");
                                Thread.Sleep(2000);
                                Console.Clear();
                                Thread.Sleep(2000);

                                currentLevel = -1;
                                gameState = 6;
                                
                            }
                        }
                        break;

                    case 6:
                        {
                            isGameRunning = Movement(SecretLevel);
                            if (LevelComplete(SecretLevel, posX, posY))
                            {
                                currentLevel = -1;
                                gameState = 7;
                            }
                        }
                        break;
                }                
                Thread.Sleep(50);
            }
        }
    }
}
