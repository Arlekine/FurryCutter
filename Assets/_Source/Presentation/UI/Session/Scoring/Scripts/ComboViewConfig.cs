using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FurryCutter.Presentation.UI
{
    [CreateAssetMenu(menuName = "Data/View/ComboViewConfig", fileName = "ComboViewConfig")]
    public class ComboViewConfig : ScriptableObject
    {
        [Serializable]
        private class ComboStep
        {
            [Min(0)] 
            public int TargetCombo;
            public string Text;
        }

        [SerializeField] private List<ComboStep> _comboSteps = new List<ComboStep>();

        public string GetComboWordForComboCount(int count)
        {
            var targetStep = _comboSteps.IndexOf(_comboSteps.FirstOrDefault(x => x.TargetCombo > count)) - 1;
            if (targetStep < 0)
                return "";
            else
                return _comboSteps[targetStep].Text;
        }

        private void OnValidate()
        {
            if (_comboSteps.Count == 0)
                return;

            int prevValue = _comboSteps[0].TargetCombo;
            for (int i = 1; i < _comboSteps.Count; i++)
            {
                if (_comboSteps[i].TargetCombo <= prevValue)
                {
                    Debug.LogError($"Error in element {i}. Next {nameof(ComboStep)}'s {nameof(ComboStep.TargetCombo)} should be bigger than previous!");
                    return;
                }
            }
        }
    }
}