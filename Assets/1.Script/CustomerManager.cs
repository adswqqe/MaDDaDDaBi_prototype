using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerManager : MonoBehaviour
{
    public Action BuyCustmer;

    [SerializeField]
    GameObject targetGo;

    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetGo.transform.position);

        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
        //    {
        //        Debug.Log("asdsad");
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == targetGo.name)
        {
            string nameTemp;
            nameTemp = targetGo.name;
            if (targetGo.GetComponent<PathScript>().GetTarget().name != "END" )
                targetGo = targetGo.GetComponent<PathScript>().GetTarget();
            else
            {
                BuyCustmer?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
