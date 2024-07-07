using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameBootstraper : MonoBehaviour
    {
        private GameRoot _game;

        private void Awake()
        {
            _game = new GameRoot();
        }
    }
}
