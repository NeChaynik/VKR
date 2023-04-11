using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using allAI;

public class RabbitController : MoveIface
{
    int distance = 12;
    public bool isDead = false;
    public bool hunt = false;
    BearController bear;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bear" && other.isTrigger != true)
        {
            bear = other.gameObject.GetComponent<BearController>();
            //agent.destination = goals[GetMaxDistance(goals, this.gameObject.transform)].position;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bear" && other.isTrigger != true)
        {
            if (SeeIt(other, 1.2f, 1.4f, 2f, distance))
            {
                // Медведь в поле зрения
                // Побег в самую дальнюю от медведя точку
                hunt = true;
            }
            if (hunt)
            {
                Hunt(other);
            }
            else //если преследуется медведем
            {
                hunt = bear.hunt;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bear")
        {
            isDead = false;
            agent.speed = 1.5f;
            thisAnim.speed = 1.0f;
            thisAnim.SetInteger("State", 2);
            hunt = false;
        }
    }
    void Hunt(Collider other)
    {
        
        // попытка убежать
        thisAnim.speed = 2.0f;
        agent.speed = 3f;
        if (isDead) // смерть
        {
            thisAnim.SetInteger("State", 1);
            agent.speed = 0f;
            hunt = false;
        }
    }
    int GetMaxDistance(Transform[] goals, Transform obj)
    {
        int result = 0;
        int i=0;
        float max = 0f;
        foreach (Transform goal in goals)
        {
            if ((goal.position - obj.position).magnitude > max)
            {
                max = (goal.position - obj.position).magnitude;
                result = i;
            }
            i++;
        }
        return result;
    }
}
