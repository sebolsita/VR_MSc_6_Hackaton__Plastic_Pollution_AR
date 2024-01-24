using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform target;
    public float maxHeadTurnAngle = 70f; // Maximum angle the head can turn
    public float headTurnSpeed = 3f; // Speed at which the head turns
    public float bodyTurnSpeed = 2f; // Speed at which the body turns
    public Animator animator; // The Animator for your NPC

    private Transform headTransform; // Cached head transform

    void Start()
    {
        // Cache the head transform here, adjust this to your specific rig setup
        headTransform = animator.GetBoneTransform(HumanBodyBones.Head);
    }

    void OnAnimatorIK(int layerIndex)
    {
        // Use the IK system to look at the target with a certain weight
        animator.SetLookAtWeight(1f); // Adjust this weight as necessary
        animator.SetLookAtPosition(target.position);
    }

    void LateUpdate()
    {
        // Calculate the direction from the NPC to the target
        Vector3 targetDirection = target.position - transform.position;
        targetDirection.y = 0; // Keep the rotation in the horizontal plane

        // Calculate the direction from the head to the target
        Vector3 headTargetDirection = target.position - headTransform.position;
        headTargetDirection.y = 0; // This keeps the head's rotation in the horizontal plane

        // Calculate the current rotation angle of the head
        float currentHeadAngle = Vector3.Angle(headTransform.forward, headTargetDirection);

        // Rotate the head towards the target if within the max head turn angle
        if (currentHeadAngle <= maxHeadTurnAngle)
        {
            Quaternion headRotation = Quaternion.LookRotation(headTargetDirection);
            headTransform.rotation = Quaternion.Slerp(headTransform.rotation, headRotation, Time.deltaTime * headTurnSpeed);
        }
        else
        {
            // If the head is turned to its limit, begin rotating the body
            Quaternion bodyRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, bodyRotation, Time.deltaTime * bodyTurnSpeed);
        }
    }
}
