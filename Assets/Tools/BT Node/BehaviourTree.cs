using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
	public BTNode root;

	public IEnumerator Executa()
	{
		while(true) {
			yield return StartCoroutine(root.Run(this));
		}
	}
}
