using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HoopScoringBehaviour : MonoBehaviour
{
    [SerializeField] private int pointValue = 1;
    private static int Score; // for debug purposes
    private bool hasScored = false;
    private void OnTriggerEnter(Collider other)
    {
        if (hasScored) return; //Ignore already scored instances
        Debug.Log(other.tag + " entered");
        if (other.CompareTag("Player"))
        {
            Debug.Log(++Score); 
            ScoreManager.instance.AddScore(pointValue);
            gameObject.SetActive(false);
            hasScored = true;      //Lock behind
        }
        
    }
}
