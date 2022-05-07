using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAtacarOponente : BTNode
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

		if (objeto) {
			bt.transform.LookAt(objeto.transform);
			GameObject tiro = bt.GetComponent<NPC>().tiro;
			Vector3 posicao = bt.transform.position + bt.transform.forward/2;
			Quaternion rotacao = Quaternion.FromToRotation(Vector3.up, bt.transform.forward);
			GameObject clone = GameObject.Instantiate(tiro, posicao, rotacao);
			clone.GetComponent<Rigidbody>().AddForce(bt.transform.forward * 100);
			status = Status.SUCCESS;
		}
		else {
			status = Status.FAILURE;
		}

		Print();
		yield break;
	}
}
