using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPegarColetavel : BTNode
{
	public override IEnumerator Run(BehaviourTree bt)
	{
		status = Status.RUNNING;
		Print();

		GameObject[] objetos = GameObject.FindGameObjectsWithTag("Coletavel");

		foreach (GameObject obj in objetos) {
			if (Vector3.Distance(bt.transform.position, obj.transform.position) < 0.5) {
				GameObject.Destroy(obj);
				status = Status.RUNNING;
			}
		}

		if (status == Status.RUNNING) status = Status.FAILURE;
		Print();
		yield break;
	}
}
