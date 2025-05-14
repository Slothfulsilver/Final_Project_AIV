using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BurglarBehaviors : MonoBehaviour
{
    NavMeshAgent agent; //El que tendrá el movimiento autónomo (el que huye)
    Detection detection;
    public GameObject target; //Objetivo del cual vamos a huir (el que persigue)
    
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        detection = GetComponent<Detection>();
    }
    void Update()
    {
        detection.DetectTargetsForBurglar(this);
    }
    
    public void Seek(Vector3 location){ //Necesito un vector3 para saber a donde tiene que ir
        agent.SetDestination(location);
    }
    public void Flee(Vector3 location){
        Vector3 fleeVector = location - this.transform.position; //Obtener el vector de a donde queremos huir (fleeVector). location es la posicion del poli
        agent.SetDestination(this.transform.position - fleeVector); //Lo restamos para invertir la posicion y que se vaya para el otro lado
    }
    
    public void Evade(GameObject police)
    {
        if (police == null) return; // Evita errores si no hay un policía

        Vector3 targetDir = police.transform.position - this.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + police.GetComponent<NavMeshAgent>().speed);
        Vector3 evadeVector = police.transform.position + police.transform.forward * lookAhead * 5;
    
        Flee(evadeVector);
    }
}
