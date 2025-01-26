using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    [SerializeField] public TMP_Text depths;
    [SerializeField] public float divingSpeed = 5f; // Velocidad de descenso en metros por segundo
    [SerializeField] public float timer = 0f;
    public float meters = 0f;
    public float KM = 0f;

    void FixedUpdate()
    {
        // Actualiza el texto con la profundidad actual
        if (KM > 0)
            depths.text = KM.ToString("F1") + " km";
        else
            depths.text = meters.ToString("F0") + " m";
    }

    IEnumerator addDepth()
    {
        while (true)
        {
            // Espera un tiempo basado en Time.deltaTime para garantizar consistencia
            yield return null;

            // Calcula el descenso en función del tiempo y la velocidad
            meters += divingSpeed * Time.deltaTime;

            // Si supera 1000 metros, convierte a kilómetros
            if (meters >= 1000)
            {
                KM += 1f;
                meters -= 1000f;
            }

            // Ajusta los decimales del km para sincronizar con los metros
            if (KM > 0)
            {
                if (meters >= 100)
                {
                    KM += 0.1f;
                    meters -= 100f;
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(addDepth());
        depths.text = meters.ToString() + " m";
    }
}
