using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private Image bar;
    public int AvailableBars;
    public int TotalBars;
    public float timeToConsume;
    public float regenenerateTime;

    private void Start()
    {
        bar = gameObject.GetComponentInChildren<Image>();
    }

    void Update()
    {
    }

    public IEnumerator barRefill(float time)
    {
        float filling = time / 50;
        bar.fillAmount = 0;
        while (bar.fillAmount < 1)
        {
            yield return new WaitForSeconds(filling);
            bar.fillAmount += 0.02f;
        }

    }
    /* habria que hacer que la stamina se controle aca
     * cada nivel de stamina te deja agregar una barra más
     * que tan solo seria disminuir el tiempo en que se regenera la barra total
     * y disminuir el consumo necesario a la mitad de la barra actual o la mitad cada nivel
     * y visualmente agregar una linea cada division para ver visualmetnte cada barra de stamina
     * tengo fe que esto sirve para hacer más flexible en el futuro subir de nivel la stamina
     * tmb hacer q mientras más dash se stackeen los dashes en una lista y q la cantidad de eso afecte el multiplicador de consumo de burbujas
     * tengo fe de que la manera q lo escribi aca tiene sentido no hay nada aca x ahora en codigo uuwuwuw
     */
}
