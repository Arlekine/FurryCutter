using System;
using System.Collections.Generic;
using FurryCutter.Bonuses;
using UnityEngine;

namespace GenericUI
{
    public class BonusMenuPresenter : MonoBehaviour
    {
        [SerializeField] private UIToSceneDragger _uiToSceneDragger;
        [SerializeField] private RectTransform _buttonsParent;
        [SerializeField] private BonusPlaceButton _bonusViewPrefab;

        private Dictionary<BonusPlaceButton, Bonus> _views = new Dictionary<BonusPlaceButton, Bonus>();
        private IBonusSystem _bonusSystem;
        private Transform _game;


        private GameObject _currentBonusPreview;
        public void Init(IReadOnlyList<Bonus> bonuses, IBonusSystem bonusSystem, Transform game)
        {
            foreach (var bonus in bonuses)
            {
                var newView = Instantiate(_bonusViewPrefab, _buttonsParent);
                newView.SetIcon(bonus.MainIcon);
                newView.SetQuantity(bonus.Count, true);

                if (bonus.CanSpend)
                    newView.Activate();
                else
                    newView.Deactivate();

                _views.Add(newView, bonus);
            }

            _game = game;
            _bonusSystem = bonusSystem;
            _uiToSceneDragger.SetElements(_views.Keys);
            _uiToSceneDragger.DragStared += OnDragStarted;
            _uiToSceneDragger.Dragging += OnDrag;
            _uiToSceneDragger.DragFinished += OnDragFinished;
        }

        private void OnDragStarted(ILeanSelectable selectable, Vector3 position)
        {
            if (_currentBonusPreview)
                throw new ArgumentException("Can't preview new bonus before current exist");

            var bonus = _views[(BonusPlaceButton)selectable];

            if (bonus.CanSpend == false)
                throw new ArgumentException($"This {nameof(Bonus)} can't be spend!");

            bonus.Spend();
            ((BonusPlaceButton)selectable).SetQuantity(bonus.Count, false);

            foreach (var viewsKey in _views.Keys)
            {
                viewsKey.Deactivate();
            }

            _currentBonusPreview = bonus.CreateBonusPreview(_game);
            _currentBonusPreview.transform.position = position;
        }

        private void OnDrag(ILeanSelectable selectable, Vector3 position)
        {
            if (_currentBonusPreview != null)
            {
                _currentBonusPreview.transform.position = position;
            }
        }

        private void OnDragFinished(ILeanSelectable selectable, Vector3 position)
        {
            foreach (var viewsKey in _views.Keys)
            {
                if (_views[viewsKey].CanSpend)
                    viewsKey.Activate();
            }

            Destroy(_currentBonusPreview);
            _currentBonusPreview = null;
            var bonusType = _views[(BonusPlaceButton)selectable].Type;
            _bonusSystem.PlaceBonus(bonusType, position);
        }
    }
}