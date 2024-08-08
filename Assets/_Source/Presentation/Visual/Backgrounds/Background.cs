using UnityEngine;

namespace FurryCutter.Presentation.Visual
{
    public abstract class Background : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
        public abstract void HideInstantly();
    }
}