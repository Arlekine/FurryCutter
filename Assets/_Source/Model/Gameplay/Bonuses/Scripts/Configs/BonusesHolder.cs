using UnityEngine;

namespace FurryCutter.Bonuses
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Bonuses/Bonuses Holder", fileName = "BonusesHolder")]
    public class BonusesHolder : ScriptableObject
    {
        [SerializeField] private StarLinesCuttingConfig _starLinesCuttingConfig;
        [SerializeField] private LinesCuttingConfig _linesCuttingConfig;
        [SerializeField] private TemporalFieldConfig _temporalFieldConfig;

        public StarLinesCuttingConfig StarLinesCuttingConfig => _starLinesCuttingConfig;
        public LinesCuttingConfig LinesCuttingConfig => _linesCuttingConfig;
        public TemporalFieldConfig TemporalFieldConfig => _temporalFieldConfig;
    }
}