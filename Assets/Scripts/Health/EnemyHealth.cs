using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private SoundSO soundData;

    private ParticleSystem damageParticlesInstance;
    private DamageFlash _damageFlash;
    private HealthBar _healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        _damageFlash = GetComponent<DamageFlash>();
        _healthBar = GetComponentInChildren<HealthBar>();

    }
    public void Damage(float damageAmount, Vector2 attackDirection)
    {
        currentHealth -= damageAmount;

        //Spawn particles
        SpawnDamageParticles(attackDirection);

        //Spawn Damage Flash
        _damageFlash.StartDamageFlash();

        //Trigger hurt sound fx
        SoundManager.PlaySound(soundData,"Hurt", null, 1);

        //Health Bar Update
        _healthBar.UpdateHealthBar(maxHealth, currentHealth);

        //If health reached to 0, destroy the enemy.
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

        damageParticlesInstance = Instantiate(damageParticles, transform.position, spawnRotation);
    }
}
