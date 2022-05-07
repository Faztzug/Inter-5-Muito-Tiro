using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoverAteColetavel : BTNode
{
	public override IEnumerator Run(BehaviourTree bt)
	{
		status = Status.RUNNING;
		Print();

		GameObject[] objetos = GameObject.FindGameObjectsWithTag("Coletavel");
		GameObject objeto = null;
		float distancia = Mathf.Infinity;
		foreach (GameObject obj in objetos) {
			if (Vector3.Distance(bt.transform.position, obj.transform.position) < distancia) {
				objeto = obj;
				distancia = Vector3.Distance(bt.transform.position, obj.transform.position);
			}
		}

		NavMeshAgent agent = bt.GetComponent<NavMeshAgent>();
		while (objeto) {
			agent.SetDestination(objeto.transform.position);
			yield return new WaitForSeconds(0.2f);

			if (agent.remainingDistance < 0.4) {
				if (Vector3.Distance(bt.transform.position, objeto.transform.position) < 0.5) {
					status = Status.SUCCESS;
				}
				break;
			}
		}

		if (status == Status.RUNNING) status = Status.FAILURE;
		Print();
	}
}
