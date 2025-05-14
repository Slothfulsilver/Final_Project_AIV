using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " colisionó con " + other.name);

        if (this.CompareTag("Policia") && other.CompareTag("Ladron"))
        {
            Debug.Log("¡Policía atrapó a un ladrón: " + other.name + "!");

            // Desactivar componentes antes de destruir
            if (other.GetComponent<NavMeshAgent>() != null)
                other.GetComponent<NavMeshAgent>().enabled = false;
            if (other.GetComponent<BurglarBehaviors>() != null)
                other.GetComponent<BurglarBehaviors>().enabled = false;

            // Validar que el objeto sigue existiendo antes de destruirlo
            if (other != null && other.gameObject != null)
            {
                Destroy(other.gameObject);
            }
        }
        else if (this.CompareTag("Civil") && other.CompareTag("Ladron"))
        {
            Debug.Log("¡Un ladrón asustó a un civil: " + this.name + "!");

            // Desactivar componentes antes de destruir
            if (this.GetComponent<NavMeshAgent>() != null)
                this.GetComponent<NavMeshAgent>().enabled = false;
            if (this.GetComponent<CivilBehaviors>() != null)
                this.GetComponent<CivilBehaviors>().enabled = false;

            // Validar que el objeto sigue existiendo antes de destruirlo
            if (this != null && this.gameObject != null)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
