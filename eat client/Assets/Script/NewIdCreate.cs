using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewIdCreate : MonoBehaviour {
	public GameObject Login;
	private string NewID=" ";
	private string NewpassWord= " ";
	private int mwidth = 0;
	private int mheight = 0;
	private bool Create_state = false;
	private InputField InputID;
	private InputField InputPW;
	private Button IDCreateButton;

	// Use this for initialization
	void Start ()
	{
		mwidth = Screen.width;
		mheight = Screen.height;
		InputID = GameObject.Find("IDCreate_Canvas").transform.Find("ID/InputField").GetComponent<InputField>();
		InputPW = GameObject.Find("IDCreate_Canvas").transform.Find("PW/InputField").GetComponent<InputField>();
		IDCreateButton = GameObject.Find("IDCreate_Canvas").transform.Find("ID_Create/Button").GetComponent<Button>();

		IDCreateButton.onClick.AddListener(() =>
		{
			//
			//Login.SetActive(true);
			//gameObject.SetActive(false);
			//
			NewID = InputID.text;
			NewpassWord = InputPW.text;
			NewID = NewID.TrimStart(); // 공백문자 제거해서 입력
			NewpassWord.Trim(new char[] { ' ', '\n', '0' });
			StartCoroutine("NewIdCreateStart");
		});
	}
	public void LoginState_func(bool state)
	{
		Create_state = state;
	}

	IEnumerator NewIdCreateStart()
	{

		Packet_NewUser_REQ packet = new Packet_NewUser_REQ(NewID, NewpassWord);
		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));

		yield return new WaitForSeconds(3.0f);
		if( !Create_state)
		{
			Login.SetActive(true);
			gameObject.SetActive(false);
			
		}
		else
		{
			Debug.Log("Crete ID Fail");
			StopCoroutine("LoginStart");

		}
	}

}
