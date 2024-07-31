using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlashV2 : MonoBehaviour
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashTime = 0.25f;
    [SerializeField] private AnimationCurve _flashSpeedCurve;


    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    private Coroutine damageFlashCoroutine;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        //Initialize the materails at the awake
        Initialize();
    }
    private void Initialize()
    {
        _materials = new Material[_spriteRenderers.Length];

        //Assign sprite renderer materials to all _materails
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
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
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_FlashColor", _flashColor);
        }    
    }
    private void SetFlashAmount(float amount)
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetFloat("_FlashAmount", amount);
        }
        
    }
}
