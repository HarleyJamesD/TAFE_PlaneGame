using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HoopScoringBehaviour : MonoBehaviour
{
    private static int Score;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " entered");
        if (other.CompareTag("Player"))
        {
            Debug.Log(++Score);
        }
    }
}
