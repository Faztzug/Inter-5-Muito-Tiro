using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTOponenteProximo : BTNode
{
	public override IEnumerator Run(BehaviourTree bt)
	{
		GameObject[] objetos = GameObject.FindGameObjectsWithTag("NPC");
		GameObject objeto = null;
		foreach (GameObject obj in objetos) {
			if (obj != bt.gameObject) {
				if (Vector3.Distance(bt.transform.position, obj.transform.position) < 6) {
					objeto = obj;
					break;
				}
			}
		}

		if (objeto) {
			status = Status.SUCCESS;
		}
		else {
			status = Status.FAILURE;
		}

		Print();
		yield break;
	}
}
