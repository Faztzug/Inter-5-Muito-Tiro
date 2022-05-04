using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTemColetavel : BTNode
{
	public override IEnumerator Run(BehaviourTree bt)
	{
		GameObject objeto = GameObject.FindGameObjectWithTag("Coletavel");
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
