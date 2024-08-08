using System;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Bonuses
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Bonuses/Lines Cut", fileName = "Lines Cut Bonus Config")]
    public class LinesCuttingConfig : BonusConfig
    {
        [Space]
        [SerializeField] private Vector2 _ñutDirection;
        [SerializeField] private int _cutLinesAmount;
        [SerializeField] private Vector2 _cuttingZoneSize;

        [Space]
        [SerializeField] private MultipleLinesView _linesView;

        public Vector2 ÑutDirection => _ñutDirection;
        public int CutLinesAmount => _cutLinesAmount;
        public Vector2 CuttingZoneSize => _cuttingZoneSize;
        public override BonusType Type => BonusType.LinesCut;

        public override Func<IServiceLocator<ISessionService>, IBonusPlacer> GetPlacerCreator()
        {
            return (serviceLocator) => new LinesCutBonusPlacer(ÑutDirection, CutLinesAmount, CuttingZoneSize, serviceLocator.GetService<ICuttingSystem>());
        }

        public override Func<Transform, GameObject> GetPreviewCreator()
        {
            return (gameParent) =>
            {
                var linesView = Instantiate(_linesView, gameParent);
                var lines = MultipleLinesCreator.GetParallelLinesZone(linesView.transform.position, _ñutDirection, _cuttingZoneSize, _cutLinesAmount);
                linesView.Draw(lines);
                return linesView.gameObject;
            };
        }
    }
}