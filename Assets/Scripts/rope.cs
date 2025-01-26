using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] public Vector3 centerPoint; // El punto central al que est� anclada la cuerda
    public float centerRadius = 0.5f; // Radio en el que se considera que el jugador est� en el centro
    public float midDistance = 5f; // Distancia media donde comienza a jalar suavemente
    public float maxDistance = 8f; // Distancia m�xima donde se jala con m�s fuerza
    public float pullSpeed = 5f; // Velocidad de jal�n en midDistance
    private bool isPulledBack = false;

    public LineRenderer ropeRenderer; // Componente LineRenderer para la visualizaci�n de la cuerda

    void Start()
    {
        // Si no se asign� un LineRenderer en el inspector, agregar uno autom�ticamente
        if (ropeRenderer == null)
        {
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configuraci�n inicial del LineRenderer
        ropeRenderer.startWidth = 0.1f;
        ropeRenderer.endWidth = 0.1f;
        ropeRenderer.positionCount = 2;
        ropeRenderer.material = new Material(Shader.Find("Sprites/Default"));
        ropeRenderer.startColor = Color.white;
        ropeRenderer.endColor = Color.white;
    }

    void Update()
    {
        // Calcular la distancia desde el punto central
        float distance = Vector3.Distance(transform.position, centerPoint);

        // Verificar si el jugador est� dentro del radio central
        if (distance <= centerRadius)
        {
            // Considerar que el jugador est� en el centro y detener el jal�n
            isPulledBack = false;
            return; // Salir del m�todo para evitar cualquier l�gica adicional
        }

        // L�gica de jal�n seg�n la distancia
        if (distance > midDistance && distance <= maxDistance)
        {
            // Jalar suavemente cuando est� entre midDistance y maxDistance
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * pullSpeed * Time.deltaTime;
        }
        else if (distance > maxDistance)
        {
            // Jalar con m�s fuerza cuando excede maxDistance
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * 4 * pullSpeed * Time.deltaTime;
            isPulledBack = true;
        }

        if (isPulledBack)
        {
            // Suavizar el jal�n si ya estaba siendo jalado
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * 2 * pullSpeed * Time.deltaTime;
        }

        // Actualizar la visualizaci�n de la cuerda
        UpdateRopeVisual(distance);
    }

    void UpdateRopeVisual(float distance)
    {
        // Establecer los puntos del LineRenderer (origen y destino)
        ropeRenderer.SetPosition(0, centerPoint);
        ropeRenderer.SetPosition(1, transform.position);

        // Cambiar el color de la cuerda seg�n la tensi�n
        if (distance >= maxDistance)
        {
            ropeRenderer.startColor = Color.red; // Cuerda tensa
            ropeRenderer.endColor = Color.red;
        }
        else if (distance > midDistance)
        {
            ropeRenderer.startColor = Color.yellow; // Cuerda parcialmente tensa
            ropeRenderer.endColor = Color.yellow;
        }
        else
        {
            ropeRenderer.startColor = Color.white; // Cuerda relajada
            ropeRenderer.endColor = Color.white;
        }
    }
}
