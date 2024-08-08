using UnityEngine;

namespace FurryCutter.Meta
{
    public class FurryGroupShopSettings
    {
        private int _cost = 100;
        private Color _color;

        public FurryGroupShopSettings(int cost, Color color)
        {
            _cost = cost;
            _color = color;
        }

        public int Cost => _cost;
        public Color Color => _color;
    }
}