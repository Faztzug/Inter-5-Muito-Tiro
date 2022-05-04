using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	public GameObject tiro;

	void Start()
	{
		BTSequence combate = new BTSequence();
		combate.children.Add(new BTOponenteProximo());
		combate.children.Add(new BTAtacarOponente());
		combate.children.Add(new BTEsquivarOponente());

		BTSequence sequencia = new BTSequence();
		sequencia.children.Add(new BTTemColetavel());
		sequencia.children.Add(new BTMoverAteColetavel());
		sequencia.children.Add(new BTPegarColetavel());

		BTSelector selector = new BTSelector();
		selector.children.Add(combate);
		selector.children.Add(sequencia);

		BehaviourTree bt = GetComponent<BehaviourTree>();
		bt.root = selector;

		StartCoroutine(bt.Executa());
	}
}
