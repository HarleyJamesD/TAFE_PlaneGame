using System.Collections;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
    [SerializeField] private float particleDuration;

    private void OnEnable()
    {
        StartCoroutine(ParticleDurationTimer());
        IEnumerator ParticleDurationTimer()
        {
            yield return new WaitForSeconds(particleDuration);
            Destroy(gameObject);
        }
    }
}
