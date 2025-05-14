using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilBehaviors : MonoBehaviour
{
    NavMeshAgent agent; //El que tendrá el movimiento autónomo (el que huye)
    Detection detection;
    public GameObject target; //Objetivo del cual vamos a huir (el que persigue)
    void Start()
    {
        detection = GetComponent<Detection>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        detection.DetectTargetsForCivil(this);
    }
    
    public void Flee(Vector3 location){
        Vector3 fleeVector = location - this.transform.position; //Obtener el vector de a donde queremos huir (fleeVector). location es la posicion del poli
        agent.SetDestination(this.transform.position - fleeVector); //Lo restamos para invertir la posicion y que se vaya para el otro lado
    }
    
    public void Seek(Vector3 location){ //Necesito un vector3 para saber a donde tiene que ir
        agent.SetDestination(location);
    }
    
    Vector3 wanderTarget = Vector3.zero;
    public void Wander(){
        if (agent.remainingDistance < 1f) // Evita recalcular cada frame
        {
            float wanderRadius = 10;
            float wanderDistance = 20;
            float wanderJitter = 5;

            wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0,
                Random.Range(-1.0f, 1.0f) * wanderJitter);

            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
            Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);
            Seek(targetWorld);
        }
    }
}
