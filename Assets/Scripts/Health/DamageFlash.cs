using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime = 0.25f;
    [SerializeField] private AnimationCurve _flashSpeedCurve;


    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private Coroutine damageFlashCoroutine;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
    }


    public void StartDamageFlash()
    {
        damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }


    private IEnumerator DamageFlasher()
    {
        //Set The Color
        SetFlashColor();

        //Lerp flash amount
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < _flashTime)
        {
            //increase the elapsed time
            elapsedTime += Time.deltaTime;

            //Lerp
            currentFlashAmount = Mathf.Lerp(1f, _flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / _flashTime));


            //Set Flash Amount
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }

    }

    private void SetFlashColor()
    {
        _material.SetColor("_FlashColor", _flashColor);
    }

    private void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashAmount", amount);
    }
}
