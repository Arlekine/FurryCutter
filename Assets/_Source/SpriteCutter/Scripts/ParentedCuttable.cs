using UnityEngine;

namespace UnitySpriteCutter
{
    public class ParentedCuttable : MonoBehaviour
    {
        [SerializeField] private Transform _joint;

        public Vector3 JointPosition => _joint.position;
    }
}