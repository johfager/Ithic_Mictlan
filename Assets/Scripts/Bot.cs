using UnityEngine;

public class BotController : MonoBehaviour
{
    public Transform maestro;  // Referencia al personaje maestro
    public float distanciaSeguimiento = 2f;
    public float rangoAtaque = 1.5f;  // Rango de ataque del bot
    public LayerMask capaOponente;  // Capa que representa a los oponentes

    private enum EstadoBot
    {
        Seguir,
        Atacar,
        DerrotaOponente
    }

    private EstadoBot estadoActual;
    private Transform oponenteActual;

    void Start()
    {
        estadoActual = EstadoBot.Seguir;
    }

    void Update()
    {
        switch (estadoActual)
        {
            case EstadoBot.Seguir:
                SeguirMaestro();
                break;

            case EstadoBot.Atacar:
                AtacarOponente();
                break;

            case EstadoBot.DerrotaOponente:
                // Cambiar al estado de Seguir después de un tiempo o evento específico.
                break;
        }
    }

    void SeguirMaestro()
    {
        // Mueve el bot hacia el maestro manteniendo la distancia especificada
        Vector3 direccion = maestro.position - transform.position;
        float distanciaActual = direccion.magnitude;

        if (distanciaActual > distanciaSeguimiento)
        {
            // Mueve al bot hacia el maestro
            transform.Translate(direccion.normalized * Time.deltaTime);
        }

        // Comprueba si el maestro está atacando
        bool maestroAtacando = maestro.GetComponent<MaestroController>().EstaAtacando();

        if (maestroAtacando)
        {
            // Encuentra al oponente más cercano
            BuscarOponente();
            
            // Cambia al estado de Atacar si se encuentra un oponente
            if (oponenteActual != null)
            {
                estadoActual = EstadoBot.Atacar;
            }
        }
    }

    void BuscarOponente()
    {
        // Usa raycast para encontrar al oponente más cercano
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, capaOponente))
        {
            oponenteActual = hit.transform;
        }
        else
        {
            oponenteActual = null;
        }
    }

    void AtacarOponente()
    {
        // Ataca al oponente si está presente
        if (oponenteActual != null)
        {
            // Calcula la distancia al oponente
            float distanciaAlOponente = Vector3.Distance(transform.position, oponenteActual.position);

            // Si el oponente está dentro del rango de ataque, realiza el ataque
            if (distanciaAlOponente <= rangoAtaque)
            {
                // Implementa la lógica para atacar al oponente
                oponenteActual.GetComponent<OponenteController>().RecibirAtaque();

                // Cambia al estado de DerrotaOponente cuando el oponente es derrotado
                if (oponenteActual.GetComponent<OponenteController>().EstaDerrotado())
                {
                    estadoActual = EstadoBot.DerrotaOponente;
                }
            }
            else
            {
                // Si el oponente está fuera del rango de ataque, vuelve al estado de Seguir
                estadoActual = EstadoBot.Seguir;
            }
        }
        else
        {
            // Cambia al estado de Seguir si el oponente ya no está presente
            estadoActual = EstadoBot.Seguir;
        }
    }

    // Puedes agregar más funciones según sea necesario.
}
