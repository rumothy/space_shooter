using UnityEngine;
using System.Collections;
using RossLib.Collections.Generic;

public class QuestionOptions : MonoBehaviour 
{
	public string CurrentSelection;
	public GameObject[] selectionTexts;
	public CircularLinkedList<GameObject> options;
	
	private Node<GameObject> currentNode;
	private bool left = false;
	private bool right = false;

	public float fireRate;
	private float nextFire;


	void Start()
	{
		options = new CircularLinkedList<GameObject>(selectionTexts);
		currentNode = options.Head;
		Select(currentNode);
	}

	void Update()
	{
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			if (Input.GetAxis("Horizontal") < 0 )
			{
				left = true;
			}
			else if (Input.GetAxis("Horizontal") > 0 )
			{
				right = true;
			}
		}

	}

	void FixedUpdate()
	{
		if (left)
		{
			Select (currentNode.Previous);
			left = false;
		}
		if (right)
		{
			Select (currentNode.Next);
			right = false;
		}


	}

	void Select(Node<GameObject> node)
	{
		GUIText selectionText = currentNode.Value.guiText;
		selectionText.color = Color.white;
		
		currentNode = node;
		selectionText = currentNode.Value.guiText;
		CurrentSelection = selectionText.name;
		selectionText.color = Color.red;
	}

	public void ResetSelection()
	{
		CurrentSelection = "";
	}

}