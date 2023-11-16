using UnityEngine;

namespace UnitySpriteCutter.Control.PostProcessing
{
    public class ParentedPostProcessor : CutPostProcessor
    {
        public override SpriteCutterOutput PostProcessCut(SpriteCutterOutput output)
        {
            var firstParented = output.firstSide.GetComponent<ParentedCuttable>();
            var secondParented = output.secondSide.GetComponent<ParentedCuttable>();

            bool isBothParented = (firstParented != null && secondParented != null);
            bool isOneParented = (firstParented != null || secondParented != null);

            if (isBothParented == false && isOneParented)
            {
                var parented = firstParented != null ? firstParented : secondParented;

                var firstDistanceToJoint = Vector3.Distance(output.firstSide.RealCenter, parented.JointPosition);
                var secondDistanceToJoint = Vector3.Distance(output.secondSide.RealCenter, parented.JointPosition);

                if (firstDistanceToJoint > secondDistanceToJoint)
                {
                    output.firstSide.GetOrAddRigidbody().bodyType = RigidbodyType2D.Static;
                    output.secondSide.GetOrAddRigidbody().bodyType = RigidbodyType2D.Dynamic;
                }
                else
                {
                }
            }

            return output;
        }
    }
}