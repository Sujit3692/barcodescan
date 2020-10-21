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
		var detail = SimpleDemo.barcodevalue + " / " + SimpleDemo.desc;
		uiText.text = detail;
	}
	// Start is called before the first frame update

	void Start()
	{
		var detail = SimpleDemo.barcodevalue + " / " + SimpleDemo.desc;
		uiText.text = detail;
		voicetest.GetSpeech();
	}

	void Update()
    {

    }

}
