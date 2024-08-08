using System;
using Lean.Touch;

namespace GenericUI
{
    public interface ILeanSelectable
    {
        event Action<LeanFinger, ILeanSelectable> Selected;
    }
}