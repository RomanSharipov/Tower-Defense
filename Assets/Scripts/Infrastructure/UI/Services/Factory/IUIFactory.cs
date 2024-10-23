using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        public UniTask CreateRootCanvas();
        public UniTask<WindowBase> CreateWindow(WindowId windowType);
    }
}