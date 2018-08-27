using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class MoveToPos : MonoBehaviour {
    private NavMeshAgent agent;
    [SerializeField] private Transform goal;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Assert.IsNotNull(agent, "YO! There's no NavMeshAgent!");
	}
	
	void Update () {
        agent.destination = goal.position;
        agent.transform.LookAt(goal, Vector3.up);
	}
}
