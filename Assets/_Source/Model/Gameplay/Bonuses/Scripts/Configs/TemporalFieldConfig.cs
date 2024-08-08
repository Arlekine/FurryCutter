using System;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Bonuses/Temporal Field", fileName = "Tempofield Bonus Config")]
    public class TemporalFieldConfig : BonusConfig
    {
        [Space]
        [SerializeField] private TemporalField _temporalFieldPrefab;
        [SerializeField] private GameObject _temporalFieldPreviewPrefab;
        [SerializeField] private float _lifeTime;

        public TemporalField TemporalFieldPrefab => _temporalFieldPrefab;
        public float LifeTime => _lifeTime;
        public override BonusType Type => BonusType.TemporalField;

        public override Func<IServiceLocator<ISessionService>, IBonusPlacer> GetPlacerCreator()
        {
            return (serviceLocator) => new TemporalFieldBonusPlacer(TemporalFieldPrefab, serviceLocator.GetService<SessionScene>().SceneSessionParent, LifeTime);
        }

        public override Func<Transform, GameObject> GetPreviewCreator()
        {
            return (gameParent) => UnityEngine.Object.Instantiate(_temporalFieldPreviewPrefab, gameParent);
        }
    }
}