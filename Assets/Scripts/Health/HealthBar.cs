using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider mainHealthBarSlider;
    [SerializeField] private Slider easeHealthBarSlider;
    [SerializeField] private float lerpSpeed;
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        if (mainHealthBarSlider.value != easeHealthBarSlider.value)
        {
            easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value, mainHealthBarSlider.value, lerpSpeed);
        }

        //If our project is 3D
        transform.LookAt(transform.position + _mainCam.transform.forward);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        mainHealthBarSlider.value = currentHealth / maxHealth;
    }
}
