using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    // Положение точки назначения
    UnityEngine.AI.NavMeshAgent agent;
    public Transform[] goals;
    int index = 0;
    float distanceToChangeGoal = 3f;
    void Start()
    {
        // Получение компонента агента
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Указаие точки назначения
        agent.destination = goals[index].position;
        
    }
    void Update ()
    {
        if (agent.remainingDistance < distanceToChangeGoal)
        {
            index++;
            if (index == goals.Length) index = 0;
            agent.destination = goals[index].position;
        }
    }
}
