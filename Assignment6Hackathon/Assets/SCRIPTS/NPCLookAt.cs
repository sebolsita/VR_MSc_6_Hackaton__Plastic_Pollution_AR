using UnityEngine;

public class NPCLookAt : MonoBehaviour
{
    public Transform playerTarget; // Target when not sitting
    public Transform deskTarget; // Target when sitting
    public float maxHeadTurnAngle = 40f;
    public float headTurnSpeed = 3f;
    public float bodyTurnSpeed = 2f;
    public Animator animator;

    private Transform headTransform;
    private Transform currentTarget;

    void Start()
    {
        headTransform = animator.GetBoneTransform(HumanBodyBones.Head);
        currentTarget = playerTarget; // Initially set to player target
    }

    void Update()
    {
        // Check if the NPC is sitting
        if (animator.GetBool("Sitting"))
        {
            currentTarget = deskTarget; // If sitting, look at the desk
        }
        else
        {
            currentTarget = playerTarget; // If not sitting, look at the player
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtWeight(1f);
        animator.SetLookAtPosition(currentTarget.position);
    }

    void LateUpdate()
    {
        Vector3 targetDirection = currentTarget.position - transform.position;
        targetDirection.y = 0;

        Vector3 headTargetDirection = currentTarget.position - headTransform.position;
        headTargetDirection.y = 0;

        float currentHeadAngle = Vector3.Angle(headTransform.forward, headTargetDirection);

        if (currentHeadAngle <= maxHeadTurnAngle)
        {
            Quaternion headRotation = Quaternion.LookRotation(headTargetDirection);
            headTransform.rotation = Quaternion.Slerp(headTransform.rotation, headRotation, Time.deltaTime * headTurnSpeed);
        }
        else
        {
            Quaternion bodyRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, bodyRotation, Time.deltaTime * bodyTurnSpeed);
        }
    }
}

