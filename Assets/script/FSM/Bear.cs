using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class Bear : MonoBehaviour
    {
        #region Variables
        public UnityEngine.AI.NavMeshAgent agent;
        public Vector3 a;
        //float distanceToChangeGoal = 1.5f;
        //Голова
        public GameObject Head;
        public GameObject Path;
        public Animator thisAnim;
        int distance = 10;
        public bool hunt = false;
        public Rabbit rabbit;

        public BearStateMachine movementSM;
        public BearWalkState walking;
        public BearHuntState hunting;
        public BearAttackState attack;
        public BearEatState eating;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            thisAnim = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            Head = gameObject.transform.Find("Head").gameObject;
            Path = gameObject.transform.Find("Path").gameObject;

            movementSM = new BearStateMachine();

            walking = new BearWalkState(this, movementSM);
            hunting = new BearHuntState(this, movementSM);
            attack = new BearAttackState(this, movementSM);
            eating = new BearEatState(this, movementSM);

            movementSM.Initialize(walking);
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.DrawRay(Path.transform.position, (Path.transform.forward + new Vector3(0, -0.02f, 0)) * 30, Color.blue);
            movementSM.CurrentState.HandleInput();

            movementSM.CurrentState.LogicUpdate();
        }
        private void FixedUpdate()
        {
            movementSM.CurrentState.PhysicsUpdate();
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Rabbit" && other.isTrigger != true)
            {
                rabbit = other.gameObject.GetComponent<Rabbit>();
            }
        }
        void OnTriggerExit(Collider other)
        {
            // патруль территории, cрабатывает и после удара лапой
            //if (other.gameObject.tag == "Rabbit" && rabbit.isDead)
            if (other.gameObject.tag == "Rabbit")
            {
                //thisAnim.SetInteger("State", 2);
                hunt = false;
            }
        }
        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Rabbit" && other.isTrigger != true)
            {
                if (SeeIt(other, 0.4f, 0.5f, 1f, distance) && !rabbit.isDead && !hunt)
                {
                    // Кролик в поле зрения
                    hunt = true;
                }
            }
        }
        #region Methods
        public Vector3 ChooseGoal()
        {
            Vector3 result = new Vector3(0, 0, 0);
            while (result == new Vector3(0, 0, 0))
            {
                float deg = Random.Range(0f, 360f);
                RaycastHit hit;

                Path.transform.Rotate(0, deg, 0);
                //Debug.DrawRay(Path.transform.position, (Path.transform.forward + new Vector3(0, -0.02f, 0)) * 30, Color.blue);
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
                //Debug.DrawRay(headPosition, controlDot, col);
            }
            return result;
        }
        #endregion
    }
}

