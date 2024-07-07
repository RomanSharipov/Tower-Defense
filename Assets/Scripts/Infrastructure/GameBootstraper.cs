using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstraper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
        }
    }
}
