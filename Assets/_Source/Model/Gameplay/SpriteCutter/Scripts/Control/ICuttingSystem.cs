using System;
using System.Collections.Generic;
using FurryCutter.Gameplay.Session.ServiceLocator;

namespace UnitySpriteCutter.Control
{
    public interface ICuttingSystem : ISessionService
    {
        event Action<LineCutResult[]> Cutted;
        void Cut(Line cutLine);
        void Cut(IEnumerable<Line> cutLine);
    }
}