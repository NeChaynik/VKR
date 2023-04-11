using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class PawController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rabbit")
        {
            Rabbit RabbitState = other.gameObject.GetComponent<Rabbit>();
            RabbitState.isDead = true;
        }
    }
}
