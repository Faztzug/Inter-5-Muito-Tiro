using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTEsquivarOponente : BTNode
{
	public override IEnumerator Run(BehaviourTree bt)
	{
		status = Status.RUNNING;
		Print();

		GameObject[] objetos = GameObject.FindGameObjectsWithTag("NPC");
		GameObject objeto = null;
		float distancia = Mathf.Infinity;
		foreach (GameObject obj in objetos) {
			if (obj != bt.gameObject) {
				if (Vector3.Distance(bt.transform.position, obj.transform.position) < distancia) {
					objeto = obj;
					distancia = Vector3.Distance(bt.transform.position, obj.transform.position);
					break;
				}
			}
		}

		NavMeshAgent agent = bt.GetComponent<NavMeshAgent>();
		Vector3 destino = bt.transform.position + bt.transform.right;
		while (objeto) {
			agent.SetDestination(destino);
			yield return new WaitForSeconds(0.2f);

			if (agent.remainingDistance < 0.1) {
				status = Status.SUCCESS;
				break;
			}
		}

		if (status == Status.RUNNING) status = Status.FAILURE;
		Print();
	}
}
