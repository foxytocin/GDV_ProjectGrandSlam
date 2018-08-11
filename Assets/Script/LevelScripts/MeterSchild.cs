using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterSchild : MonoBehaviour {
	public string meterWert;
	public Text myText;

	// Use this for initialization
	void Start () {
		meterWert = transform.position.z.ToString();
		myText.text = meterWert + " Meter";
	}
}
