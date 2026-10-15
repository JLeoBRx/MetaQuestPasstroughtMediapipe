using UnityEngine;

public class HandFX : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem Particle1;
    public ParticleSystem Particle2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (Particle1 != null)
            {
                
                Particle1.Play();
                Particle2.Play();
            }
        }
  
        
    }
}