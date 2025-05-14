using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
     public float detectionRange = 10f;

    // Encuentra el objeto más cercano con un tag específico
    public GameObject FindClosestWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < minDistance && distance <= detectionRange)
            {
                minDistance = distance;
                closest = obj;
            }
        }
        return closest;
    }

    // Para el ladrón: determina si debe evadir o buscar
    public void DetectTargetsForBurglar(BurglarBehaviors burglar)
    {
        GameObject nearestPolice = FindClosestWithTag("Policia");
        GameObject nearestCivil = FindClosestWithTag("Civil");

        if (nearestPolice != null && (nearestCivil == null || 
                                      Vector3.Distance(transform.position, nearestPolice.transform.position) < Vector3.Distance(transform.position, nearestCivil.transform.position)))
        {
            burglar.Evade(nearestPolice); // Evadir si el policía está más cerca
        }
        else if (nearestCivil != null)
        {
            burglar.Seek(nearestCivil.transform.position); // Buscar al civil si está más cerca
        }
        else
        {
            burglar.Evade(nearestPolice); // Si no hay nadie, sigue evadiendo sin objetivo
        }
    }

    // Para el policía: determina si debe buscar al ladrón
    public void DetectTargetsForPolice(PoliceBehaviors police)
    {
        GameObject nearestBurglar = FindClosestWithTag("Ladron");
        if (nearestBurglar != null)
        {
            police.Seek(nearestBurglar.transform.position);
        }
        else
        {
            police.Wander(); // Si no detecta ladrones, sigue patrullando
        }
    }

    // Para el civil: determina si debe huir
    public void DetectTargetsForCivil(CivilBehaviors civil)
    {
        GameObject nearestBurglar = FindClosestWithTag("Ladron");
        if (nearestBurglar != null)
        {
            civil.Flee(nearestBurglar.transform.position);
        }
        else
        {
            civil.Wander(); // Si no hay ladrones cerca, sigue caminando
        }
    }
}
