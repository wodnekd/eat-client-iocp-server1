using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour {

	private int mwidth = 0;
	private int mheight = 0;
	public List<string> chatHistory = new List<string>();
	public Transform textLines;
    
	private string currentMessage = string.Empty;
	public GameObject game;
	private Text textField;
	private InputField inputField;
	private Button send;
	private Button next;

	void Start()
	{
		mwidth = Screen.width;
		mheight = Screen.height;

		textField = GameObject.Find("Chat_Canvas").transform.Find("Chat/Text").GetComponent<Text>();
		inputField = GameObject.Find("Chat_Canvas").transform.Find("Chat/InputField").GetComponent<InputField>();
		send = GameObject.Find("Chat_Canvas").transform.Find("Chat/Send").GetComponent<Button>();
		next = GameObject.Find("Chat_Canvas").transform.Find("Chat/Next").GetComponent<Button>();

		send.onClick.AddListener(() => 
		{
			string inputText = inputField.text;
			string text = textField.text;

			textField.text += text.Length == 0 ? inputText : "\n" + inputText;

			PacketTALK_REQ packet = new PacketTALK_REQ(inputText);
			NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
		});

		next.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
			game.SetActive(true);
		});
	}

	private void OnApplicationQuit()
	{
		gameObject.SetActive(false);
	}

}
