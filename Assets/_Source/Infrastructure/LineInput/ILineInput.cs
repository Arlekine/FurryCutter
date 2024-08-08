using System;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace UnitySpriteCutter.Control
{
    public interface ILineInput : ISessionService
    {
        event Action<Vector2> Pressed;
        event Action<Vector2, Vector2> Released;

        bool IsPressed { get; }
        Vector2 CurrentPointerPosition { get; }

        void Enable();
        void Disable();
    }
}