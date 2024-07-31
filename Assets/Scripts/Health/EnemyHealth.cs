using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private ParticleSystem damageParticles;

    private ParticleSystem damageParticlesInstance;

    private DamageFlash _damageFlash;

    private void Start()
    {
        currentHealth = maxHealth;
        _damageFlash = GetComponent<DamageFlash>();

    }
    public void Damage(float damageAmount, Vector2 attackDirection)
    {
        currentHealth -= damageAmount;

        //Spawn particles
        SpawnDamageParticles(attackDirection);



        if (currentHealth <= 0) 
        {
            Destroy(gameObject);
        }

        //Spawn Damage Flash
        _damageFlash.StartDamageFlash();
    }


    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

        damageParticlesInstance = Instantiate(damageParticles, transform.position, spawnRotation);
    }
}
