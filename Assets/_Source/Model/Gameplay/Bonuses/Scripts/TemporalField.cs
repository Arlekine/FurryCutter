using System.Collections;
using UnityEngine;

namespace FurryCutter.Bonuses
{
    public class TemporalField : MonoBehaviour
    {
        [SerializeField] private SlowmoZone _slowmoZone;

        public void Show(float autoDestroyTime)
        {
            //TODO: showing animation
            StartCoroutine(DestroyRoutine(autoDestroyTime));
        }

        public IEnumerator DestroyRoutine(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}