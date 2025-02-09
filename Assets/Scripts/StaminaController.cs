using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaController : MonoBehaviour
{
    private Image bar;
    public int TotalBars = 1; //para upgradear
    public float regenerateAmount = 1; // tasa de regeneración
    public float barCost = 1; // consumo en cuanto a barras
    public float currentStamina;
    public int speedLevel = 1; //para upgradear

    private void Start()
    {
        bar = gameObject.GetComponentInChildren<Image>();
        
    }

    void Update()
    {
        bar.fillAmount = currentStamina;
        if(bar.fillAmount < 1)
        {
            bar.fillAmount += regenerateAmount / TotalBars * speedLevel * Time.deltaTime ;
            
        }
        currentStamina = bar.fillAmount;
    }

    public void useStamina()
    {
        if(currentStamina >= barCost/TotalBars)
        {
            currentStamina -= barCost/TotalBars;
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
