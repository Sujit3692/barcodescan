using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using UnityLibrary;

public class Input : MonoBehaviour
{

	[SerializeField]
	private Text m_Hypotheses;

	[SerializeField]
	private Text m_Recognitions;

	private DictationRecognizer m_DictationRecognizer;

	// Start is called before the first frame update
	void Start()
    {

		

	string sayAtStart = "Tell the material quantity to transfer";
		Speech.instance.Say(sayAtStart, TTSCallback);

		m_DictationRecognizer = new DictationRecognizer();

		m_DictationRecognizer.DictationResult += (text, confidence) =>
		{
			Debug.LogFormat("Dictation result: {0}", text);
			//m_Recognitions.text += text + "\n";
		};

		m_DictationRecognizer.DictationHypothesis += (text) =>
		{
			Debug.LogFormat("Dictation hypothesis: {0}", text);
			//m_Hypotheses.text += text;
		};

		m_DictationRecognizer.DictationComplete += (completionCause) =>
		{
			if (completionCause != DictationCompletionCause.Complete)
				Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
		};

		m_DictationRecognizer.DictationError += (error, hresult) =>
		{
			Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
		};

		m_DictationRecognizer.Start();
	}

    // Update is called once per frame
    void Update()
    {
		/*string sayAtStart = "Tell the material quantity to transfer";
		Speech.instance.Say(sayAtStart, TTSCallback);*/
	}

	void TTSCallback(string message, AudioClip audio)
	{
		AudioSource source = GetComponent<AudioSource>();
		if (source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
		}

		source.clip = audio;
		source.Play();
	}
}
