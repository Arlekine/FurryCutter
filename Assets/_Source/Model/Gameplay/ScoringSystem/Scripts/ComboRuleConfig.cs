using UnityEngine;

namespace FurryCutter.Gameplay.ScoringSystem
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Combo Rule Config", fileName = "Combo Rule Config")]
    public class ComboRuleConfig : ScriptableObject
    {
        [SerializeField] private float _bonusMultiplayerPerPiece = 0.25f;

        public float GetMultiplayerForPiecesAmount(int amount)
        {
            var multiplayer = _bonusMultiplayerPerPiece * amount;
            return Mathf.Max(1f, multiplayer);
        }
    }
}