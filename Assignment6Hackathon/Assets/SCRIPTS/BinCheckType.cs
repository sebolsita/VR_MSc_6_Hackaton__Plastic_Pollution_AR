using UnityEngine;

public class BinCollision : MonoBehaviour
{
    public string correctPlasticType; // Assign this in the inspector

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the correct plastic type tag
        if (collision.gameObject.CompareTag(correctPlasticType))
        {
            Debug.Log("Bin (" + gameObject.tag + ") registered correct trash (" + correctPlasticType + ")");
        }
        else
        {
            Debug.Log("Bin (" + gameObject.tag + ") registered wrong trash (" + collision.gameObject.tag + ")");
        }
    }
}