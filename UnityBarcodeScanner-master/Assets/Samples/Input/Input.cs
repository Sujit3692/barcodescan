using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VoiceTest))]
public class Input : MonoBehaviour
{
	public Text uiText;
	public VoiceTest voicetest;
	void Awake()
    {
		/*var detail = "Material: " + SimpleDemo.barcodevalue + " \nDescription: " + SimpleDemo.desc;
		uiText.text = detail;*/
	}
	// Start is called before the first frame update

	void Start()
	{
		var detail = "Material: " + SimpleDemo.barcodevalue + " \nDescription: " + SimpleDemo.desc + " \nUnit of Measure: " + SimpleDemo.uom + " \nPlant: " + SimpleDemo.plant;
		uiText.text = detail;
		voicetest.GetSpeech();
	}

	void Update()
    {

    }

}
