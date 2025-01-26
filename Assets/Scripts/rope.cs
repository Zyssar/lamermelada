using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] public Vector3 centerPoint; // El punto central al que est� anclada la cuerda
    public float maxDistance = 8f; // Longitud m�xima de la cuerda
    public float pullSpeed = 5f; // Velocidad con la que el jugador es tirado hacia el centro
    public LineRenderer ropeRenderer; // Componente LineRenderer para la visualizaci�n de la cuerda

    private bool isPulledBack = false;

    void Start()
    {
        if (ropeRenderer == null)
        {
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configuraci�n b�sica del LineRenderer
        ropeRenderer.startWidth = 0.1f;
        ropeRenderer.endWidth = 0.1f;
        ropeRenderer.positionCount = 2;
        ropeRenderer.material = new Material(Shader.Find("Sprites/Default"));
        ropeRenderer.startColor = Color.white;
        ropeRenderer.endColor = Color.white;
    }

    void Update()
    {
        // Calcular la distancia desde el centro
        float distance = Vector3.Distance(transform.position, centerPoint);

        // Si la distancia supera la longitud m�xima
        if (distance > maxDistance)
        {
            // Calcular la direcci�n hacia el punto central
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;

            // Mover el objeto hacia el centro a la velocidad de tir�n
            transform.position += directionToCenter * pullSpeed * Time.deltaTime;
            isPulledBack = true;
        }
        else
        {
            isPulledBack = false;
        }

        // Actualizar la visualizaci�n de la cuerda
        UpdateRopeVisual(distance);
    }

    void UpdateRopeVisual(float distance)
    {
        // Establecer los puntos del LineRenderer (origen y destino)
        ropeRenderer.SetPosition(0, centerPoint);
        ropeRenderer.SetPosition(1, transform.position);

        // Si la cuerda est� estirada al m�ximo, mantenerla tensa
        if (distance >= maxDistance)
        {
            ropeRenderer.startColor = Color.red; // Cambiar color a rojo para indicar tensi�n
            ropeRenderer.endColor = Color.red;
        }
        else
        {
            ropeRenderer.startColor = Color.white; // Restablecer el color
            ropeRenderer.endColor = Color.white;
        }
    }
}
