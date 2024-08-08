using System;
using FurryCutter.Gameplay.Session.ServiceLocator;
using UnityEngine;
using UnitySpriteCutter.Control;

namespace FurryCutter.Bonuses
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Bonuses/Stars Cut", fileName = "Star Cut Bonus Config")]
    public class StarLinesCuttingConfig : BonusConfig
    {
        [Space]
        [SerializeField] private int _cutLinesAmount;
        [SerializeField] private float _linesLength;

        [Space]
        [SerializeField] private MultipleLinesView _linesView;

        public int CutLinesAmount => _cutLinesAmount;
        public float LinesLength => _linesLength;
        public override BonusType Type => BonusType.StarCut;

        public override Func<IServiceLocator<ISessionService>, IBonusPlacer> GetPlacerCreator()
        {
            return (serviceLocator) => new StarLinesCutBonusPlacer(CutLinesAmount, LinesLength, serviceLocator.GetService<ICuttingSystem>());
        }

        public override Func<Transform, GameObject> GetPreviewCreator()
        {
            return (gameParent) =>
            {
                var linesView = Instantiate(_linesView, gameParent);
                var lines = MultipleLinesCreator.GetCircleLinesFromPoint(linesView.transform.position, _linesLength, _cutLinesAmount);
                linesView.Draw(lines);
                return linesView.gameObject;
            };
        }
    }
}