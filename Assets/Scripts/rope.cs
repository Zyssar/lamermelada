using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] public Vector3 centerPoint; // El punto central al que está anclada la cuerda
    public float centerRadius = 0.5f; // Radio en el que se considera que el jugador está en el centro
    public float midDistance = 5f; // Distancia media donde comienza a jalar suavemente
    public float maxDistance = 8f; // Distancia máxima donde se jala con más fuerza
    public float pullSpeed = 5f; // Velocidad de jalón en midDistance
    private bool isPulledBack = false;

    public LineRenderer ropeRenderer; // Componente LineRenderer para la visualización de la cuerda

    void Start()
    {
        // Si no se asignó un LineRenderer en el inspector, agregar uno automáticamente
        if (ropeRenderer == null)
        {
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configuración inicial del LineRenderer
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

        // Verificar si el jugador está dentro del radio central
        if (distance <= centerRadius)
        {
            // Considerar que el jugador está en el centro y detener el jalón
            isPulledBack = false;
            return; // Salir del método para evitar cualquier lógica adicional
        }

        // Lógica de jalón según la distancia
        if (distance > midDistance && distance <= maxDistance)
        {
            // Jalar suavemente cuando está entre midDistance y maxDistance
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * pullSpeed * Time.deltaTime;
        }
        else if (distance > maxDistance)
        {
            // Jalar con más fuerza cuando excede maxDistance
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * 4 * pullSpeed * Time.deltaTime;
            isPulledBack = true;
        }

        if (isPulledBack)
        {
            // Suavizar el jalón si ya estaba siendo jalado
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * 2 * pullSpeed * Time.deltaTime;
        }

        // Actualizar la visualización de la cuerda
        UpdateRopeVisual(distance);
    }

    void UpdateRopeVisual(float distance)
    {
        // Establecer los puntos del LineRenderer (origen y destino)
        ropeRenderer.SetPosition(0, centerPoint);
        ropeRenderer.SetPosition(1, transform.position);

        // Cambiar el color de la cuerda según la tensión
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
