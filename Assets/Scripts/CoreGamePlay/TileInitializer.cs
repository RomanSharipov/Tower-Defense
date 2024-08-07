using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CoreGamePlay
{
    public class TileInitializer : MonoBehaviour
    {
        [SerializeField] private MapGenerator _mapGenerator;


        private void Awake()
        {
            foreach (Tile tile in _mapGenerator.GameBoardTiles)
            {
                tile.InitializeNode(_mapGenerator.GameBoardTiles);
            }
        }
    }
}
