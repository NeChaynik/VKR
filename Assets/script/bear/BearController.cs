using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using allAI;

public class BearController : MoveIface
{
    int distance = 10;
    public bool hunt = false;
    public RabbitController rabbit;
    int rememberIndex;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rabbit" && other.isTrigger != true)
        {
            rabbit = other.gameObject.GetComponent<RabbitController>();
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        // патруль территории, cрабатывает и после удара лапой
        if (other.gameObject.tag == "Rabbit" && rabbit.isDead)
        {
            thisAnim.SetInteger("State", 2);
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
                rememberIndex = index+1;
                if (rememberIndex == goals.Length) rememberIndex = 0;
            }
            if (hunt)
            {
                Hunt(other);
            }
        }
    }
    void Hunt(Collider other)
    {
        // анимация бега во время охоты на зайца
        agent.speed = 6f;
        thisAnim.SetInteger("State", 1);
        agent.destination = other.gameObject.transform.position;
        // при достижении расстояния для удара
        if (agent.remainingDistance < 2.5f)
        {
            agent.destination = goals[rememberIndex].position;
            index = rememberIndex;
            //остановка, удар
            agent.speed = 0f;
            thisAnim.SetInteger("State", 3);
            if (!rabbit.isDead) //промах
            {
                thisAnim.SetInteger("State", 4);
                agent.speed = 6f;
            }
            else // попадание
            {
                // проигрывание всех анимаций и завершение охоты
                if (thisAnim.GetCurrentAnimatorStateInfo(0).IsName("Bear_WalkForward"))
                {
                    hunt = false;
                    agent.speed = 1.3f;
                    thisAnim.SetInteger("State", 2);
                }
            }
        }
    }
}
