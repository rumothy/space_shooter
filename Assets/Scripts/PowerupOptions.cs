using UnityEngine;
using System.Collections;

public class PowerupOptions : MonoBehaviour 
{
	public string CurrentPowerup;
	public GameObject[] powerupTexts;

	private int powerupCount = -1;
	private int PowerupIndex 
	{
		get { return powerupCount % powerupTexts.Length;}
	}

	public void PowerupPickedUp()
	{
		DeselectPrevious();
		
		SelectNext();
	}

	void DeselectPrevious ()
	{
		if (powerupCount < 0) return;
		GUIText powerupText = powerupTexts [PowerupIndex].guiText;
		powerupText.color = Color.white;

	}

	void SelectNext()
	{
		powerupCount++;
		GUIText powerupText = powerupTexts[PowerupIndex].guiText;
		CurrentPowerup = powerupText.name;
		powerupText.color = Color.red;

	}

	public void ResetPowerup()
	{
//		if (powerupCount >= 0) 
//		{
//			Debug.Log(string.Format("ResetPowerup::powerupCount: {0}", powerupCount));
//			Debug.Log(string.Format("ResetPowerup::PowerupIndex: {0}", PowerupIndex));
//		}
		DeselectPrevious ();
		powerupCount = -1;
		CurrentPowerup = "";
	}


}

