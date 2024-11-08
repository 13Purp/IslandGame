﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace IslandGame
{
    internal class ArchipelAlgo
    {

        private CellTable _layer1;
        private CellTable _layer2;
        private CellTable _layer3;
        private CellTable _final;
        private List<Island> _islands;
        private int _width;
        private int _height;
        private float _maxHeight;

        public ArchipelAlgo(int x, int y, int sizeOfCell)
        {
            _width = x / sizeOfCell;
            _height = y / sizeOfCell;
            _layer1=new CellTable(x, y, sizeOfCell);
            _layer2=new CellTable(x, y, sizeOfCell);
            _layer3=new CellTable(x, y, sizeOfCell);
            _final=new CellTable(x, y, sizeOfCell);
            _islands=new List<Island>();
            _maxHeight=0;
           

        }

        public void generate()
        {
            Random random = new Random();
            Random mountainCorrection = new Random();

            _layer1.Randomize();
            _layer2.Randomize();
            _layer3.Randomize();

            int randomValue = random.Next(2, 15);
            for (int i = 0; i < randomValue; i++)   
                _layer1.UpdateState();

            randomValue = random.Next(10, 25);
            for (int i = 0; i < randomValue; i++)
                _layer2.UpdateState();

            for(int i=0; i<randomValue*2; i++)
                _layer3.UpdateState();

            Automata[,] layer1Map = _layer1.GetTable(); 
            Automata[,] layer2Map = new Automata[_width,_height]; 
            Automata[,] layer2Temp = _layer2.GetTable();
            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                {

                    layer2Map[i, j]=new Conway( layer2Temp[i, j]);

                }

            for (int i = 0;i<_width;i++)
                for (int j = 0; j < _height; j++)
                {
                   
                        layer2Map[i,j].State =(layer1Map[i, j].Neighbours != 0 && layer2Map[i, j].Neighbours != 0) ? layer1Map[i, j].State + layer2Map[i, j].State:0;

                }

            _layer1.UpdateState();
            layer1Map = _layer1.GetTable();
            Automata[,] layer3Map = _layer3.GetTable();

            for (int i = 0; i < _width; i++)
                for (int j = 0; j < _height; j++)
                {

                    layer2Map[i, j].State = (layer1Map[i, j].Neighbours != 0 && layer2Map[i, j].Neighbours != 0) ? layer1Map[i, j].State + layer2Map[i, j].State : 0;

                    if (layer2Map[i, j].State >= 2)
                        layer2Map[i, j].State = layer3Map[i, j].State + layer2Map[i, j].State;

                  


                }

            findIslands(layer2Map);


            int correction = mountainCorrection.Next(1, 3);
            int[] rNbr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] cNbr = { -1, 0, 1, -1, 1, -1, 0, 1 };


            for (int i = 1; i < _width-1; i++)
                for (int j = 1; j < _height-1; j++)
                {
                    if (layer2Map[i, j].State == 4)
                    {
                        for (int k = 0; k < 8; ++k)
                        {
                            int newR = i + rNbr[k];
                            int newC = j + cNbr[k];
                            if (layer2Map[newR, newC].State == 1) layer2Map[newR, newC].State += correction;
                        }
                    }
                    else if (layer2Map[i, j].State == 0)
                        foreach (Island island in _islands)
                        {
                            if (island.CorrrectWater(layer2Map[i, j]))
                                break;
                        }


                }

           // correctingWater(layer2Map);

            for (int i = 1; i < _width - 1; i++)
                for (int j = 1; j < _height - 1; j++)
                {
                 
                    if (layer2Map[i, j].State >0 )
                    {


                        int colorUpscale = layer2Map[i, j].State * 250;
                        layer2Map[i, j].State = random.Next(colorUpscale - 250, colorUpscale + 1);
                    }
                }



            _final.SetTable(layer2Map);

            foreach (Island island in _islands)
            {
                island.CalcHeight();
                float height=island.GetAverageHeight();
                if(height>_maxHeight)
                    _maxHeight = height;
            }



        }

        private void correctingWater(Automata[,] map)
        {
            int[,] matrix = new int[_width, _height];
            for (int i = 0; i < _width ; i++)
                for (int j = 0; j < _height; j++)
                {
                    if(i==0||j==0||i==_width-1||j==_height-1)
                        matrix[i, j] = 0;
                    else
                        matrix[i, j] = -1;
                }

            for (int i = 1; i < _width-1 ; i++)
                for (int j = 1; j < _height-1 ; j++)
                {
                    if (map[i, j].State == 0 && matrix[i,j]==-1)
                    {
                        bool isFree=false;
                        foreach (Island island in _islands)
                        {
                            if (island.CorrrectWater(map[i, j])) {
                                isFree = true;
                                break;
                            }
                        }
                        if (!isFree)
                        {
                            waterBFS(map, matrix, i, j);
                        }
                    }
                }
        }

        private void waterBFS(Automata[,] map, int[,] matrix, int i, int j)
        {
            int[] rNbr = { -1, 0, 0, 1 };
            int[] cNbr = { 0, -1, 1, 0 };
            int[] rNbr2 = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] cNbr2 = { -1, 0, 1, -1, 1, -1, 0, 1 };
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((i,j));
            matrix[i,j] = 0;
    
            while (q.Count > 0)
            {
                var (r, c) = q.Dequeue();
                int numOfWaterNeighbors = 0;

                for (int k = 0; k < 4; k++)
                {
                    int newR = r + rNbr[k];
                    int newC = c + cNbr[k];
                    if (IsSafeWater(map, newR, newC))
                        numOfWaterNeighbors++;
                    if (numOfWaterNeighbors < 4)
                        continue;
                }
                for (int k = 0; k < 4; k++)
                {
                    int newR = r + rNbr[k];
                    int newC = c + cNbr[k];
                    if (IsSafeWater(map, newR, newC))
                    {
                        if (matrix[newR, newC] == -1)
                        {
                            q.Enqueue((newR, newC));
                            matrix[newR, newC] = 0;
                        }

                    }
                }
            }

        }

        private bool IsSafe(Automata[,] map, int x, int y, bool[,] visited)
        {
            int ROW = map.GetLength(0);
            int COL = map.GetLength(1);

         
            return (x >= 0) && (x < ROW) && (y >= 0) && (y < COL) &&
                   (map[x, y].State != 0 && !visited[x, y]);
        }

        private bool IsSafeWater(Automata[,] map, int x, int y)
        {
            int ROW = map.GetLength(0);
            int COL = map.GetLength(1);


            return (x >= 0) && (x < ROW) && (y >= 0) && (y < COL) &&
                   (map[x, y].State == 0);
        }


        private void DFS(Automata[,] map, int x, int y, bool[,] visited, Island island)
        {

           
            int[] rNbr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] cNbr = { -1, 0, 1, -1, 1, -1, 0, 1 };

            if (island.GetCount() == 0 && IsSafe(map, x, y, visited))
                island.add(map[x, y]);

            visited[x, y] = true;
            


            for (int k = 0; k < 8; ++k)
            {
                int newR = x + rNbr[k];
                int newC = y + cNbr[k];
                if (IsSafe(map, newR, newC, visited))
                {
                    DFS(map, newR, newC, visited,island);
                    island.add(map[newR, newC]);
                }
            }
        }
        private void BFS(Automata[,] map, int sr, int sc, bool[,] vis, Island island)
        {

          
            int[] rNbr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] cNbr = { -1, 0, 1, -1, 1, -1, 0, 1 };

            
            Queue<(int, int)> q = new Queue<(int, int)>();
            q.Enqueue((sr, sc));
            if (island.GetCount() == 0 && IsSafe(map, sr, sc, vis))
                island.add(map[sr, sc]);
            vis[sr,sc] = true;

            while (q.Count > 0)
            {
                var (r, c) = q.Dequeue();

                for (int k = 0; k < 8; k++)
                {
                    int newR = r + rNbr[k];
                    int newC = c + cNbr[k];
                    if (IsSafe(map, newR, newC, vis))
                    {
                        vis[newR,newC] = true;
                        q.Enqueue((newR, newC));
                        island.add(map[newR, newC]);

                    }
                }
            }
        }

        private void findIslands(Automata[,] map)
        {
            int ROW = map.GetLength(0);
            int COL = map.GetLength(1);


            bool[,] visited = new bool[ROW, COL];


            int count = 0;
            for (int r = 0; r < ROW; ++r)
            {
                for (int c = 0; c < COL; ++c)
                {


                    if (map[r, c].State != 0 && !visited[r, c])
                    {
                        Island island = new Island();

                        BFS(map, r, c, visited, island);

                        _islands.Add(island);
                        ++count;
                    }
                }
            }


        }
        public CellTable GetCellTable() => _final;

        public Island? GetIsland(Automata a)
        {
            foreach (Island island in _islands)
            {
                if (island.isInIsland(a))
                    return island;
            }
            return null;
        }

        public float getMaxHeight() => _maxHeight;

    }
}