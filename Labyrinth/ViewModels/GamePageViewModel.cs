using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Labyrinth.Command;
using Labyrinth.Data;
using Labyrinth.ViewModels.Base;
using Labyrinth.Models;
using System.Diagnostics.Eventing.Reader;

namespace Labyrinth.ViewModels
{
    public class GamePageViewModel : ViewModel
    {
        private int stars = 0;
        public static int Score { get; set; } = 0;
        public static int Level { get; set; }

        private char[,] map;
        public char[,] Map { get => map; set => Set(ref map, value); }

        #region Consructor
        public GamePageViewModel()
        {
            MoveDownCommand = new(MoveDown);
            MoveLeftCommand = new(MoveLeft);
            MoveRightCommand = new(MoveRight);
            MoveUpCommand = new(MoveUp);
            RestartGame = new(StartGame);

            Level = 0;
            StartGame();
            GetStars();
        }
        #endregion

        #region Start Game
        public void StartGame()
        {
            Score = 0;
            map = MapModel.GetMap(Level).map.Clone() as char[,];
            Update();
        }
        #endregion

        #region Commands
        public RelayCommand MoveLeftCommand { get; init; }
        public RelayCommand MoveRightCommand { get; init; }
        public RelayCommand MoveUpCommand { get; init; }
        public RelayCommand MoveDownCommand { get; init;}
        public RelayCommand RestartGame { get; init; }
        #endregion

        #region GetPlayerPos
        public int[] GetPlayerPos()
        {
            for ( int i = 0; i < map.GetLength(0); i++ )
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == '*')
                    {
                        return new int[] { i, j };
                    } 
                }
            }
            return new int[] { -1, -1 };
        }
        #endregion

        #region Moves
        private void MoveDown()
        {
            MoveDir(1, 0);
        }
        private void MoveLeft()
        {
            MoveDir(0, -1);
        }
        private void MoveRight()
        {
            MoveDir(0, 1);
        }
        private void MoveUp()
        {
            MoveDir(-1, 0);
        }
        #endregion

        #region Update
        public void Update()
        {
            OnPropertyChanged("Map");
        }
        #endregion

        public void GetStars()
        {
            stars = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '1')
                    {
                        stars++;
                    }
                }
            }
        }

        #region MoveDir
        public void MoveDir(int i, int j) {
            int playeri = GetPlayerPos()[0];
            int playerj = GetPlayerPos()[1];
            char dirblock = Map[i + playeri, j + playerj];

            if (dirblock != '#')
            {
                if (dirblock == '1') Score++;

                if (Score == stars)
                {

                    if (Level < MapModel.GetMapsCount() - 1)
                    {
                        Level++;
                        StartGame();
                        GetStars();
                        return;
                    }
                    else
                    {
                        Map[playeri, playerj] = '0';
                        Map[i + playeri, j + playerj] = '*';
                        Update();
                        MessageBoxResult mbr = MessageBox.Show("Вы прошли все уровни, начать заново?", "Конец игры", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                        if (mbr == MessageBoxResult.Yes)
                        {
                            Level = 0;
                            StartGame();
                            GetStars();
                            return;
                        }
                        else
                        {
                            Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    Map[playeri, playerj] = '0';
                    Map[i + playeri, j + playerj] = '*';
                }
                MoveDir(i, j);
                return;
            }

            Update();
        }
        #endregion
    }
}

/*
* - игок
# - стена
0 - пусто
1 - выход
*/