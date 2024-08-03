using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.UI.Services
{
    public interface IUIFactory
    {
        UniTask CreateRootCanvas();
        public UniTask<T> CreateWindow<T>(WindowId windowType) where T : Component;
    }
}