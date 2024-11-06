using System;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public interface IInputService
    {
        public IObservable<Unit> GetKeyUp { get; }
        public IObservable<Unit> GetKeyDown { get; }
        public Vector3 MousePosition { get; }
    }
}