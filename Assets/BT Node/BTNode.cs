using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode
{
	public enum Status { RUNNING, SUCCESS, FAILURE }
	public Status status { protected set; get; }

	public abstract IEnumerator Run(BehaviourTree bt);

	public virtual void Print()
	{
		Debug.Log(this.GetType().Name + " - " + status.ToString());
	}
}
