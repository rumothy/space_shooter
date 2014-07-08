using UnityEngine;
using System.Collections;

public class QuestionOptions : MonoBehaviour 
{
	public string CurrentSelection;
	public GameObject[] selectionTexts;
	
	private int selectionCount = -1;
	private int SelectionIndex 
	{
		get { return selectionCount % selectionTexts.Length;}
	}
	
	public void SelectionChanged()
	{
		DeselectPrevious();
		
		SelectNext();
	}

	void Start()
	{
		SelectNext();
	}

	void Upadate()
	{
		if (Input.GetAxis("Horizontal") < 0)
			selectionCount--;
		else if (Input.GetAxis("Horizontal") > 0)
			selectionCount++;
	}

	void DeselectPrevious ()
	{
		if (selectionCount < 0) return;
		GUIText selectionText = selectionTexts [SelectionIndex].guiText;
		selectionText.color = Color.white;
		
	}
	
	void SelectNext()
	{
		selectionCount++;
		GUIText selectionText = selectionTexts[SelectionIndex].guiText;
		CurrentSelection = selectionText.name;
		selectionText.color = Color.red;
		
	}
	
	public void ResetSelection()
	{
		Debug.Log(string.Format("ResetSelection::selectionCount: {0}", selectionCount));
		Debug.Log(string.Format("ResetSelection::SelectionIndex: {0}", SelectionIndex));
		DeselectPrevious ();
		selectionCount = -1;
		CurrentSelection = "";
	}

}