using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        public UniTask CreateRootCanvas();
        public UniTask<TWindow> CreateWindow<TWindow>() where TWindow : WindowBase;
    }
}