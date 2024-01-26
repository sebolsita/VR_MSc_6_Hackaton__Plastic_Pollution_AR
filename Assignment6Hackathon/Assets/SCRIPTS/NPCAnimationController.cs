using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public Transform playerTransform;
    public Transform chairTransform;

    private bool isPlayerInvited = false;
    private bool isPlayerAtDesk = false;
    private bool isPlayerNearNpc = false;
    private bool isNpcNearChair = false;

    void Start()
    {
        // Get the Animator component attached to this NPC
        animator = GetComponent<Animator>();

        // Check if the Animator component exists
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this NPC.");
        }

        // Get the NavMeshAgent component attached to this NPC
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Check if the NavMeshAgent component exists
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on this NPC.");
        }

        navMeshAgent.isStopped = true; //Make sure npc is not walking to the player
        Debug.Log("NPC movement OFF");

    }

    void Update()
    {
        // Check if the Animator and NavMeshAgent components are valid
        if (animator != null && navMeshAgent != null)
        {
            // Calculate the speed percentage based on the NavMeshAgent's speed
            float speedPercentage = navMeshAgent.velocity.magnitude / navMeshAgent.speed;

            // Set the "Speed" parameter in the blend tree to control the animation
            animator.SetFloat("Speed", speedPercentage);
        }

        //CHECK IF PLAYER IS NOT IN FRONT OF THE DESK AND WAS NOT INVITED
        if (!isPlayerAtDesk && !isPlayerInvited)
        {
            StartCoroutine(WaitBeforeChecking(0.1f));
        }
        else
        {
            StartCoroutine(WaitBeforeChecking2(0.1f));
        }
    }

    void InvitePlayer()
    {
        bool isSitting = animator.GetBool("Sitting");
        if (isSitting)
        {
            Debug.Log("The NPC is currently sitting, standing UP.");
            animator.SetTrigger("StandUp");
            animator.SetBool("Sitting", false);
            StartCoroutine(WaitBeforeMoving(2)); // Wait for animation to finish and start walking
        }
        else
        {
            Debug.Log("The NPC is not sitting");
            navMeshAgent.isStopped = false;
            Debug.Log("NPC movement ON");
            MoveToTarget(playerTransform);
            Debug.Log("NPC destination set to Player");
        }

        CheckIfNearPlayer();
        if (isPlayerNearNpc == true)
        {
            Debug.Log("NPC is inviting");
            navMeshAgent.isStopped = true;
            Debug.Log("NPC movement OFF");
            animator.SetTrigger("StandingInvitation");
            StartCoroutine(WaitBeforeMoving(5));
            
            isPlayerInvited = true;
        }
    }

    void MoveToTarget(Transform targetTransform)
    {
        // Check if the NavMeshAgent and targetTransform are assigned
        if (navMeshAgent != null && targetTransform != null)
        {
            // Set the destination for the NavMeshAgent to the target's position
            // navMeshAgent.isStopped = false; // Ensure the agent can move
            navMeshAgent.SetDestination(targetTransform.position);
        }
        else
        {
            Debug.LogError("NavMeshAgent or targetTransform is not assigned.");
        }
    }

    IEnumerator WaitBeforeMoving(float waitTime)
    {
        // This will pause the execution of this coroutine for waitTime.
        // During this time, the rest of the game will continue running.
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waiting for: " + waitTime + " sec");

        // activating npc movement
        animator.ResetTrigger("StandingInvitation");
        navMeshAgent.isStopped = false;
        Debug.Log("NPC movement ON");
    }
    
    IEnumerator WaitBeforeChecking(float waitTime)
    {
        // This will pause the execution of this coroutine for waitTime.
        // During this time, the rest of the game will continue running.
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waiting for: " + waitTime + " sec");

        InvitePlayer();
    }
    IEnumerator WaitBeforeChecking2(float waitTime)
    {
        // This will pause the execution of this coroutine for waitTime.
        // During this time, the rest of the game will continue running.
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Waiting for: " + waitTime + " sec");

        NpcGoToChair();
    }

    void CheckIfNearPlayer()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    // Here the NPC has reached the player or has stopped for some other reason
                    isPlayerNearNpc = true;
                }
            }
        }
    }

    void NpcGoToChair()
    {
        MoveToTarget(chairTransform);
        Debug.Log("NPC destination set  to chair");
        CheckIfNearChair();
        if (isNpcNearChair)
        {
            Debug.Log("NPC reached chair");
            animator.SetTrigger("SitDown");
            animator.SetBool("Sitting", true);
        }
    }

    void CheckIfNearChair()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    isNpcNearChair = true;
                }
            }
        }
    }

}