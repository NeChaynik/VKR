using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace allAI
{
    public class ttest : MonoBehaviour
{
    // Положение точки назначения
    public UnityEngine.AI.NavMeshAgent agent;
        public Vector3 a;
   // public Transform[] goals;
    public int index;
    float distanceToChangeGoal = 1.5f;
    //Голова
    public GameObject Head;
        public GameObject Path;
        public Animator thisAnim;
    void Start()
    {
        index = 0;
        // Получение компонента агента
        thisAnim = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // Указаие точки назначения
        // agent.destination = goals[index].position;
        agent.speed = 1.4f;
        //Голова
        Head = gameObject.transform.Find("Head").gameObject;
            Path = gameObject.transform.Find("Path").gameObject;
            agent.destination = ChooseGoal();
        }
    void Update()
    {
            Debug.DrawRay(Path.transform.position, (Path.transform.forward + new Vector3(0, -0.02f, 0)) * 30, Color.blue);
            if (agent.remainingDistance < distanceToChangeGoal)
        {
                agent.destination = ChooseGoal();
                //ChooseGoal();
                /*
            index++;
            if (index == goals.Length) index = 0;
            agent.destination = goals[index].position;
                */
            }
    }
        public Vector3 ChooseGoal ()
        {
            Vector3 result = new Vector3(0,0,0);
            while (result == new Vector3(0, 0, 0))
            {
                float deg = Random.Range(0f, 360f);
                RaycastHit hit;

                Path.transform.Rotate(0, deg, 0);
                Debug.DrawRay(Path.transform.position, (Path.transform.forward + new Vector3(0, -0.02f, 0)) * 30, Color.blue);
                Physics.Raycast(Path.transform.position, (Path.transform.forward + new Vector3(0, -0.02f, 0)), out hit, 30, ~(1 << 2));
                a = hit.point;
                result = hit.point;
            }
            return result;
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
            Physics.Raycast(headPosition, controlDot.normalized, out hit, distance, ~(1 << 2));
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