using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaDetection : MonoBehaviour
{
    public BoxCollider deskArea;
    public BoxCollider npcArea;
    public BoxCollider chairArea;

    public UnityEvent onPlayerEnterDeskArea;
    public UnityEvent onPlayerExitDeskArea;
    public UnityEvent onPlayerEnterNpcArea;
    public UnityEvent onPlayerExitNpcArea;
    // ... Similarly for chairArea if needed

    void OnTriggerEnter(Collider other)
    {
        if (deskArea != null && other.gameObject == deskArea.gameObject && other.CompareTag("Player"))
        {
            Debug.Log("Player entered desk area");
            onPlayerEnterDeskArea.Invoke();
        }
        if (npcArea != null && other.gameObject == npcArea.gameObject && other.CompareTag("Player"))
        {
            Debug.Log("Player entered NPC area");
            onPlayerEnterNpcArea.Invoke();
        }
        // ... Similarly for chairArea if needed
    }

    void OnTriggerExit(Collider other)
    {
        if (deskArea != null && other.gameObject == deskArea.gameObject && other.CompareTag("Player"))
        {
            Debug.Log("Player exited desk area");
            onPlayerExitDeskArea.Invoke();
        }
        if (npcArea != null && other.gameObject == npcArea.gameObject && other.CompareTag("Player"))
        {
            Debug.Log("Player exited NPC area");
            onPlayerExitNpcArea.Invoke();
        }
        // ... Similarly for chairArea if needed
    }
}
