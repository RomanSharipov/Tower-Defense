using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts
{
    /// <summary>
    /// This algorithm is written for readability. Although it would be perfectly fine in 80% of games, please
    /// don't use this in an RTS without first applying some optimization mentioned in the video: https://youtu.be/i0x5fj4PqP4
    /// If you enjoyed the explanation, be sure to subscribe!
    ///
    /// Also, setting colors and text on each hex affects performance, so removing that will also improve it marginally.
    /// </summary>
    public static class Pathfinding
    {
        public static List<TileData> FindPath(TileData startNode, TileData targetNode)
        {
            List<TileData> toSearch = new List<TileData>() { startNode };
            HashSet<TileData> processed = new HashSet<TileData>();

            while (toSearch.Any())
            {
                TileData current = toSearch[0];
                foreach (TileData t in toSearch)
                    if (t.F < current.F || t.F == current.F && t.H < current.H) current = t;

                processed.Add(current);
                toSearch.Remove(current);

                

                if (current == targetNode)
                {
                    TileData currentPathTile = targetNode;
                    List<TileData> path = new List<TileData>();
                    int count = 100;
                    while (currentPathTile != startNode)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0) throw new Exception();
                    }
                    return path;
                }

                foreach (TileData neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t)))
                {
                    bool inSearch = toSearch.Contains(neighbor);

                    float costToNeighbor = current.G + current.GetDistance(neighbor);

                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.SetG(costToNeighbor);
                        neighbor.SetConnection(current);

                        if (!inSearch)
                        {
                            neighbor.SetH(neighbor.GetDistance(targetNode));
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }
            return null;
        }
    }
}