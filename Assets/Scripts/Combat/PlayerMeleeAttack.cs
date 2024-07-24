using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask damageableLayer;
    [SerializeField] private float damageAmount;

    private RaycastHit2D[] hits;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (UserInput.instance.controls.Attacking.Attack.WasPressedThisFrame())
        {
            _animator.SetBool("isAttacking", true);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


    #region Animation Events
    public void TriggerAttack()
    {
        hits = Physics2D.CircleCastAll(attackPoint.position, attackRadius, transform.right, 0f, damageableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable _iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            if (_iDamageable != null)
            {
                _iDamageable.Damage(damageAmount, transform.right);
            }
        }
    }

    public void AnimationFinishTrigger()
    {
        _animator.SetBool("isAttacking", false);
    }

    #endregion
}
