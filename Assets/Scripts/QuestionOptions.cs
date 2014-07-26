using UnityEngine;
using System.Collections;


public class QuestionOptions : MonoBehaviour 
{
	public string CurrentSelection;
	public GameObject[] selectionTexts;
	private int currentIndex = -1;
	private int selectedIndex = -1;

	void Update()
	{
		if (Input.GetButton("Answer1"))
			Select(0);		
		if (Input.GetButton("Answer2"))
			Select(1);			
		if (Input.GetButton("Answer3"))
			Select(2);	
	}

	void Select(int index)
	{
		GUIText selectionText = null;
		if (currentIndex >= 0 && currentIndex < selectionTexts.Length)
		{
			selectionText = selectionTexts[currentIndex].guiText;
			selectionText.color = Color.white;
		}
		currentIndex = index;
		selectionText = selectionTexts[currentIndex].guiText;
		CurrentSelection = selectionText.name;
		selectionText.color = Color.red;
		selectedIndex = index;
	}

	public void ResetSelection()
	{
		CurrentSelection = "";
	}

	public bool IsCorrect()
	{
		int correctIndex = 2;//Random.Range(0, selectionTexts.Length);
		Debug.Log(selectedIndex == correctIndex);
		return selectedIndex == correctIndex;

	}


}