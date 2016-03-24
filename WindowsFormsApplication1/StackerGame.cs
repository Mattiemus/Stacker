using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Stores the stacker game handler.
    /// </summary>
    public class StackerGame
    {
        /// <summary>
        /// Stores the main form instance.
        /// </summary>
        private Main _MainForm;

        /// <summary>
        /// Stores the game thread.
        /// </summary>
        private Thread _GameThread;

        /// <summary>
        /// Stores the array of tiles.
        /// </summary>
        private Button[,] _Tiles;

        /// <summary>
        /// Stores the list of speeds for each line.
        /// </summary>
        private int[] _SpeedTable = new int[]
        {
            20,
            17,
            17,
            16,
            15,
            14,
            14,
            12,
            12,
            12,
            10,
            13,
            12,
            11,
            7,
        };

        /// <summary>
        /// Stores if the game about to start is a new game.
        /// </summary>
        private bool _NewGame;

        /// <summary>
        /// Stores if a game is in progress.
        /// </summary>
        private bool _GameInProgress;

        /// <summary>
        /// Gets or sets if a game is currently in progress.
        /// </summary>
        public bool GameInProgress
        {
            get
            {
                return this._GameInProgress;
            }
            set
            {
                // Make sure we aren't resetting a game.
                if (value == true && this._GameInProgress == true)
                {
                    throw new Exception("Game already in progress.");
                }
                else if (value == false && this._GameInProgress == false)
                {
                    throw new Exception("Game already stopped.");
                }

                // Start/stop the game in progress.
                if (value)
                {
                    this._GameInProgress = true;
                    this._NewGame = true;
                }
                else
                {
                    this._GameInProgress = false;
                }
            }
        }

        /// <summary>
        /// Stores if should place block on current tile.
        /// </summary>
        private bool _PlaceBlock;

        /// <summary>
        /// Sets if a block should be placed.
        /// </summary>
        public bool PlaceBlock
        {
            get
            {
                return this._PlaceBlock;
            }
            set
            {
                this._PlaceBlock = value;
            }
        }

        /// <summary>
        /// Initialises this stacker game hander.
        /// </summary>
        /// <param name="MainForm"></param>
        public StackerGame(Main MainForm)
        {
            // Store.
            this._MainForm = MainForm;

            // Create starting game params.
            this._GameInProgress = false;

            // Create tiles array.
            this._Tiles = new Button[7, 15];
            {
                // Row A.
                this._Tiles[0, 0] = this._MainForm.A1;
                this._Tiles[1, 0] = this._MainForm.A2;
                this._Tiles[2, 0] = this._MainForm.A3;
                this._Tiles[3, 0] = this._MainForm.A4;
                this._Tiles[4, 0] = this._MainForm.A5;
                this._Tiles[5, 0] = this._MainForm.A6;
                this._Tiles[6, 0] = this._MainForm.A7;

                // Row B.
                this._Tiles[0, 1] = this._MainForm.B1;
                this._Tiles[1, 1] = this._MainForm.B2;
                this._Tiles[2, 1] = this._MainForm.B3;
                this._Tiles[3, 1] = this._MainForm.B4;
                this._Tiles[4, 1] = this._MainForm.B5;
                this._Tiles[5, 1] = this._MainForm.B6;
                this._Tiles[6, 1] = this._MainForm.B7;

                // Row C.
                this._Tiles[0, 2] = this._MainForm.C1;
                this._Tiles[1, 2] = this._MainForm.C2;
                this._Tiles[2, 2] = this._MainForm.C3;
                this._Tiles[3, 2] = this._MainForm.C4;
                this._Tiles[4, 2] = this._MainForm.C5;
                this._Tiles[5, 2] = this._MainForm.C6;
                this._Tiles[6, 2] = this._MainForm.C7;

                // Row D.
                this._Tiles[0, 3] = this._MainForm.D1;
                this._Tiles[1, 3] = this._MainForm.D2;
                this._Tiles[2, 3] = this._MainForm.D3;
                this._Tiles[3, 3] = this._MainForm.D4;
                this._Tiles[4, 3] = this._MainForm.D5;
                this._Tiles[5, 3] = this._MainForm.D6;
                this._Tiles[6, 3] = this._MainForm.D7;

                // Row E.
                this._Tiles[0, 4] = this._MainForm.E1;
                this._Tiles[1, 4] = this._MainForm.E2;
                this._Tiles[2, 4] = this._MainForm.E3;
                this._Tiles[3, 4] = this._MainForm.E4;
                this._Tiles[4, 4] = this._MainForm.E5;
                this._Tiles[5, 4] = this._MainForm.E6;
                this._Tiles[6, 4] = this._MainForm.E7;

                // Row F.
                this._Tiles[0, 5] = this._MainForm.F1;
                this._Tiles[1, 5] = this._MainForm.F2;
                this._Tiles[2, 5] = this._MainForm.F3;
                this._Tiles[3, 5] = this._MainForm.F4;
                this._Tiles[4, 5] = this._MainForm.F5;
                this._Tiles[5, 5] = this._MainForm.F6;
                this._Tiles[6, 5] = this._MainForm.F7;

                // Row G.
                this._Tiles[0, 6] = this._MainForm.G1;
                this._Tiles[1, 6] = this._MainForm.G2;
                this._Tiles[2, 6] = this._MainForm.G3;
                this._Tiles[3, 6] = this._MainForm.G4;
                this._Tiles[4, 6] = this._MainForm.G5;
                this._Tiles[5, 6] = this._MainForm.G6;
                this._Tiles[6, 6] = this._MainForm.G7;

                // Row H.
                this._Tiles[0, 7] = this._MainForm.H1;
                this._Tiles[1, 7] = this._MainForm.H2;
                this._Tiles[2, 7] = this._MainForm.H3;
                this._Tiles[3, 7] = this._MainForm.H4;
                this._Tiles[4, 7] = this._MainForm.H5;
                this._Tiles[5, 7] = this._MainForm.H6;
                this._Tiles[6, 7] = this._MainForm.H7;

                // Row I.
                this._Tiles[0, 8] = this._MainForm.I1;
                this._Tiles[1, 8] = this._MainForm.I2;
                this._Tiles[2, 8] = this._MainForm.I3;
                this._Tiles[3, 8] = this._MainForm.I4;
                this._Tiles[4, 8] = this._MainForm.I5;
                this._Tiles[5, 8] = this._MainForm.I6;
                this._Tiles[6, 8] = this._MainForm.I7;

                // Row J.
                this._Tiles[0, 9] = this._MainForm.J1;
                this._Tiles[1, 9] = this._MainForm.J2;
                this._Tiles[2, 9] = this._MainForm.J3;
                this._Tiles[3, 9] = this._MainForm.J4;
                this._Tiles[4, 9] = this._MainForm.J5;
                this._Tiles[5, 9] = this._MainForm.J6;
                this._Tiles[6, 9] = this._MainForm.J7;

                // Row K.
                this._Tiles[0, 10] = this._MainForm.K1;
                this._Tiles[1, 10] = this._MainForm.K2;
                this._Tiles[2, 10] = this._MainForm.K3;
                this._Tiles[3, 10] = this._MainForm.K4;
                this._Tiles[4, 10] = this._MainForm.K5;
                this._Tiles[5, 10] = this._MainForm.K6;
                this._Tiles[6, 10] = this._MainForm.K7;

                // Row L.
                this._Tiles[0, 11] = this._MainForm.L1;
                this._Tiles[1, 11] = this._MainForm.L2;
                this._Tiles[2, 11] = this._MainForm.L3;
                this._Tiles[3, 11] = this._MainForm.L4;
                this._Tiles[4, 11] = this._MainForm.L5;
                this._Tiles[5, 11] = this._MainForm.L6;
                this._Tiles[6, 11] = this._MainForm.L7;

                // Row M.
                this._Tiles[0, 12] = this._MainForm.M1;
                this._Tiles[1, 12] = this._MainForm.M2;
                this._Tiles[2, 12] = this._MainForm.M3;
                this._Tiles[3, 12] = this._MainForm.M4;
                this._Tiles[4, 12] = this._MainForm.M5;
                this._Tiles[5, 12] = this._MainForm.M6;
                this._Tiles[6, 12] = this._MainForm.M7;

                // Row N.
                this._Tiles[0, 13] = this._MainForm.N1;
                this._Tiles[1, 13] = this._MainForm.N2;
                this._Tiles[2, 13] = this._MainForm.N3;
                this._Tiles[3, 13] = this._MainForm.N4;
                this._Tiles[4, 13] = this._MainForm.N5;
                this._Tiles[5, 13] = this._MainForm.N6;
                this._Tiles[6, 13] = this._MainForm.N7;

                // Row O.
                this._Tiles[0, 14] = this._MainForm.O1;
                this._Tiles[1, 14] = this._MainForm.O2;
                this._Tiles[2, 14] = this._MainForm.O3;
                this._Tiles[3, 14] = this._MainForm.O4;
                this._Tiles[4, 14] = this._MainForm.O5;
                this._Tiles[5, 14] = this._MainForm.O6;
                this._Tiles[6, 14] = this._MainForm.O7;
            }

            // Finally, create game thread and play cool animations...
            this._GameThread = new Thread(new ThreadStart(GameLoop));
            this._GameThread.Start();
        }

        /// <summary>
        /// Sets a tile to be on or off.
        /// </summary>
        /// <param name="X">X position.</param>
        /// <param name="Y">Y position.</param>
        /// <param name="Value">Value to set tile to.</param>
        private void SetTile(int X, int Y, bool Value)
        {
            try
            {
                if (this._MainForm.InvokeRequired)
                {
                    this._MainForm.Invoke((new MethodInvoker(delegate
                        {
                            if (Value)
                            {
                                this._Tiles[X, Y].BackColor = Color.Red;
                            }
                            else
                            {
                                this._Tiles[X, Y].BackColor = Color.White;
                            }
                        })));
                }
                else
                {
                    if (Value)
                    {
                        this._Tiles[X, Y].BackColor = Color.Red;
                    }
                    else
                    {
                        this._Tiles[X, Y].BackColor = Color.White;
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// The game loop.
        /// </summary>
        private void GameLoop()
        {
            // Store game play data.
            long CurrentTick = 0;
            int CurrentLevel = 0;
            int CurrentPosition = 0;
            bool CurrentDirection = true; // True ->       False <-
            bool JoinedLevel = true;
            int CurrentSpeed = 0;

            while (true)
            {
                // Check form still exists, or we may crash.
                if (this._MainForm.Disposing || this._MainForm.IsDisposed)
                {
                    return;
                }

                // If the game is new, reset.
                if (this._NewGame)
                {
                    CurrentTick = 0;
                    CurrentLevel = 0;
                    CurrentPosition = 0;
                    CurrentDirection = true;
                    JoinedLevel = true;
                    CurrentSpeed = 0;

                    // Clear the board.
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 15; y++)
                        {
                            SetTile(x, y, false);
                        }
                    }

                    this._NewGame = false;
                }

                if (this._GameInProgress)
                {
                    // Recently joined level, create blocks & calculate speed.
                    if (JoinedLevel)
                    {
                        // Firstly add the blocks.
                        // Levels 0 - 4 = 3 Blocks.
                        if (CurrentLevel >= 0 && CurrentLevel <= 4)
                        {
                            SetTile(0, CurrentLevel, true);
                            SetTile(1, CurrentLevel, true);
                            SetTile(2, CurrentLevel, true);
                            CurrentPosition = 0;
                        }
                        // Levels 5 - 7 = 2 Blocks.
                        else if (CurrentLevel >= 5 && CurrentLevel <= 7)
                        {
                            SetTile(0, CurrentLevel, true);
                            SetTile(1, CurrentLevel, true);
                            CurrentPosition = 0;
                        }
                        // All other levels = 1 Block.
                        else
                        {
                            SetTile(0, CurrentLevel, true);
                            CurrentPosition = 0;
                        }

                        // Now calculate speed.
                        CurrentSpeed = this._SpeedTable[CurrentLevel];

                        JoinedLevel = false;
                    }

                    // Move blocks.
                    {
                        // Check we should move the blocks this tick.
                        if (CurrentTick % CurrentSpeed == 0)
                        {
                            // Firstly check for hitting the walls.
                            if (CurrentDirection && this._Tiles[6, CurrentLevel].BackColor == Color.Red)
                            {
                                CurrentDirection = false;
                            }
                            else if (!CurrentDirection && this._Tiles[0, CurrentLevel].BackColor == Color.Red)
                            {
                                CurrentDirection = true;
                            }

                            // Now we can move the blocks.
                            if (CurrentDirection)
                            {
                                // Set block to left to false, and block to right to true.
                                SetTile(CurrentPosition, CurrentLevel, false);

                                // Levels 0 - 4 = 3 Blocks.
                                if (CurrentLevel >= 0 && CurrentLevel <= 4)
                                {
                                    SetTile(CurrentPosition + 3, CurrentLevel, true);
                                }
                                // Levels 5 - 7 = 2 Blocks.
                                else if (CurrentLevel >= 5 && CurrentLevel <= 7)
                                {
                                    SetTile(CurrentPosition + 2, CurrentLevel, true);
                                }
                                // All other levels = 1 Block.
                                else
                                {
                                    SetTile(CurrentPosition + 1, CurrentLevel, true);
                                }

                                CurrentPosition++;
                            }
                            else
                            {
                                // Set block to right to false, and block to left to true.
                                SetTile(CurrentPosition - 1, CurrentLevel, true);

                                // Levels 0 - 4 = 3 Blocks.
                                if (CurrentLevel >= 0 && CurrentLevel <= 4)
                                {
                                    SetTile(CurrentPosition + 2, CurrentLevel, false);
                                }
                                // Levels 5 - 7 = 2 Blocks.
                                else if (CurrentLevel >= 5 && CurrentLevel <= 7)
                                {
                                    SetTile(CurrentPosition + 1, CurrentLevel, false);
                                }
                                // All other levels = 1 Block.
                                else
                                {
                                    SetTile(CurrentPosition, CurrentLevel, false);
                                }

                                CurrentPosition--;
                            }
                        }
                    }

                    // Check for block placement.
                    if (this.PlaceBlock)
                    {
                        this.PlaceBlock = false;

                        // Destroy all overhanging blocks.
                        if (CurrentLevel != 0)
                        {
                            // Levels 0 - 4 = 3 Blocks.
                            if (CurrentLevel >= 0 && CurrentLevel <= 4)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (this._Tiles[CurrentPosition + i, CurrentLevel - 1].BackColor != Color.Red)
                                    {
                                        SetTile(CurrentPosition + i, CurrentLevel, false);
                                    }
                                }
                            }
                            // Levels 5 - 7 = 2 Blocks.
                            else if (CurrentLevel >= 5 && CurrentLevel <= 7)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    if (this._Tiles[CurrentPosition + i, CurrentLevel - 1].BackColor != Color.Red)
                                    {
                                        SetTile(CurrentPosition + i, CurrentLevel, false);
                                    }
                                }
                            }
                            // All other levels = 1 Block.
                            else
                            {
                                for (int i = 0; i < 1; i++)
                                {
                                    if (this._Tiles[CurrentPosition + i, CurrentLevel - 1].BackColor != Color.Red)
                                    {
                                        SetTile(CurrentPosition + i, CurrentLevel, false);
                                    }
                                }

                                //if (this._Tiles[CurrentPosition, CurrentLevel - 1].BackColor != Color.Red)
                                //{
                                //    SetTile(CurrentPosition, CurrentLevel, false);
                                //}
                            }

                            // If there are no blocks left on current level - lose criteria.
                            bool FoundRed = false;
                            for (int x = 0; x < 7; x++)
                            {
                                if (this._Tiles[x, CurrentLevel].BackColor == Color.Red)
                                {
                                    FoundRed = true;
                                }
                            }
                            if (!FoundRed)
                            {
                                // Lose!
                                MessageBox.Show("You lose", "Stacker");
                                this.GameInProgress = false;
                                this._MainForm.Invoke((new MethodInvoker(delegate
                                {
                                    this._MainForm.StartButton.Text = "START";
                                })));
                                continue;
                            }
                        }

                        // Increment level and specify level join.
                        CurrentLevel++;
                        JoinedLevel = true;
                        CurrentDirection = true;

                        // Check for win condition.
                        if (CurrentLevel == 11)
                        {
                            // Minor prize.
                            DialogResult Result = MessageBox.Show("Would you like to stop here and claim your minor prize?", "Stacker", MessageBoxButtons.YesNo);
                            if (Result == DialogResult.Yes)
                            {
                                MessageBox.Show("Claim your minor prize.", "Stacker");
                                this.GameInProgress = false;
                                this._MainForm.Invoke((new MethodInvoker(delegate
                                {
                                    this._MainForm.StartButton.Text = "START";
                                })));
                            }
                        }
                        else if (CurrentLevel == 15)
                        {
                            // Major prize.
                            MessageBox.Show("CONGRATULATIONS, Claim your major prize.", "Stacker");
                            this.GameInProgress = false;
                            this._MainForm.Invoke((new MethodInvoker(delegate
                            {
                                this._MainForm.StartButton.Text = "START";
                            })));
                            continue;
                        }
                    }

                    CurrentTick++;
                }

                Thread.Sleep(10);
            }
        }
    }
}
