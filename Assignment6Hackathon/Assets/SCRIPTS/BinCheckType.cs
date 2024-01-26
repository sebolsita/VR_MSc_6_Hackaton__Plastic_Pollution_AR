using System.Collections;
using System.Drawing.Text;
using UnityEngine;

public class BinCollision : MonoBehaviour
{
    public string correctPlasticType; // Assign this in the inspector
    public Animator npcAnimatorMale;
    public Animator npcAnimatorFemale;
    public int headLayerIndex; // Set this to the index of the head layer in the Animator


    void OnCollisionEnter(Collision collision)
    {
        int howMuchError = 0;
        // Check if the collided object has the correct plastic type tag
        if (collision.gameObject.CompareTag(correctPlasticType))
        {
            Debug.Log("Bin (" + gameObject.tag + ") registered correct trash (" + correctPlasticType + ")");
            npcAnimatorMale.SetLayerWeight(headLayerIndex, 0);
            npcAnimatorMale.SetTrigger("StandingYes");
            StartCoroutine(EnableLayerAfterDelay(2));

        }
        else
        {
            Debug.Log("Bin (" + gameObject.tag + ") registered wrong trash (" + collision.gameObject.tag + ")");
            howMuchError = howMuchError+1;
            if (howMuchError < 2)
            {
                npcAnimatorMale.SetLayerWeight(headLayerIndex, 0);
                npcAnimatorMale.SetTrigger("StandingNo");
                StartCoroutine(EnableLayerAfterDelay(2));
            }
            else
            {
                npcAnimatorMale.SetLayerWeight(headLayerIndex, 0);
                npcAnimatorMale.SetTrigger("StandingVeryNo");
                StartCoroutine(EnableLayerAfterDelay(2));
            }
        }
    }

    IEnumerator EnableLayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        npcAnimatorMale.SetLayerWeight(headLayerIndex, 1);
    }
}