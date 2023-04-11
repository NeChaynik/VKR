using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace allAI
{
    public class MoveIface : MonoBehaviour
    {
        // Положение точки назначения
        public UnityEngine.AI.NavMeshAgent agent;
        public Transform[] goals;
        public int index;
        float distanceToChangeGoal = 1.5f;
        //Голова
        public GameObject Head;
        public Animator thisAnim;
        void Start()
        {
            index = 0;
            // Получение компонента агента
            thisAnim = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            // Указаие точки назначения
            agent.destination = goals[index].position;
            agent.speed = 1.4f;
            //Голова
            Head = gameObject.transform.Find("Head").gameObject;
        }
        void Update()
        {
            if (agent.remainingDistance < distanceToChangeGoal)
            {
                index++;
                if (index == goals.Length) index = 0;
                agent.destination = goals[index].position;
            }  
        }
        public bool SeeIt(Collider other, float x, float y, float z, int distance)
        {
            Color col = Color.red;

            Vector3 headPosition;
            Vector3 vectorToObject;
            Vector3 vectorFromHead;

            RaycastHit hit;
            bool result = false;

            headPosition = Head.transform.position;
            vectorToObject = other.gameObject.transform.position - headPosition;
            vectorFromHead = Head.transform.forward.normalized;

            Vector3[] controlDots =
            {
            vectorToObject,
            vectorToObject + new Vector3(0,y,0),
            vectorToObject + new Vector3(x/2, y/2, 0),
            vectorToObject + new Vector3(-x/2, y/2, 0),
            vectorToObject + new Vector3(z/2, y/2, 0),
            vectorToObject + new Vector3(-z/2, y/2, 0)
        };
            
            foreach (Vector3 controlDot in controlDots)
            {
                Physics.Raycast(headPosition, controlDot.normalized, out hit, distance, ~(1<<2));
                if (Vector3.Dot(controlDot.normalized, vectorFromHead) > 0 && hit.transform == other.gameObject.transform)
                {
                    col = Color.green;
                    result = true;
                } 
                else
                    col = Color.red;
                Debug.DrawRay(headPosition, controlDot, col);
            }
            return result;
        }
    }
}
