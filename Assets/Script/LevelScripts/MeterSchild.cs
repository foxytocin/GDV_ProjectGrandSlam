using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterSchild : MonoBehaviour {
	public string meterWert;
	public Text myText;

	public void setMeter (int row, bool normalline) {

		if(normalline)
		{
			meterWert = row.ToString();
			myText.text = meterWert + " Meter";
		} else {
			
			myText.text = "Last Record";
		}	
	}
}
