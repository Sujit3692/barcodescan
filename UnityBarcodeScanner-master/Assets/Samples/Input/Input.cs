using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(VoiceTest))]
public class Input : MonoBehaviour
{
	public Text uiText;
	public VoiceTest voicetest;
	// Start is called before the first frame update
	void Start()
	{	

		voicetest.GetSpeech();
	}

	void Update()
    {

    }

}
