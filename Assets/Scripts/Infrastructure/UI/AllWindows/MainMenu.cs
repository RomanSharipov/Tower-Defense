using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.Infrastructure.UI
{
    public class MainMenu : WindowBase
    {
        [Inject] GameRoot GameRoot;

        [ContextMenu("Print()")]
        public void Print()
        {
            Debug.Log($"GameRoot = {GameRoot}");
        }
    }
}
