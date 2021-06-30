using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
	private string LoginID=" ";
	private string passWord= " ";
	private int mwidth = 0;
	private int mheight = 0;
	private bool login_state=false;
	public GameObject NewIdCreate;
	public GameObject lobby;
	private InputField InputID;
	private InputField InputPW;
	private Button IDCreateButton;
	private Button LoginButton;
	// Use this for initialization
	void Start ()
	{
		mwidth = Screen.width;
		mheight = Screen.height;

		InputID = GameObject.Find("Login_Canvas").transform.Find("ID/InputField").GetComponent<InputField>();
		InputPW = GameObject.Find("Login_Canvas").transform.Find("PW/InputField").GetComponent<InputField>();
		IDCreateButton = GameObject.Find("Login_Canvas").transform.Find("ID_Create/Button").GetComponent<Button>();
		LoginButton = GameObject.Find("Login_Canvas").transform.Find("Login/Button").GetComponent<Button>();

		
		IDCreateButton.onClick.AddListener(() =>
		{
			gameObject.SetActive(false);
			NewIdCreate.SetActive(true);
		});

		LoginButton.onClick.AddListener(() =>
		{
			//
			//lobby.SetActive(true);
			//gameObject.SetActive(false);
			//
			LoginID = InputID.text;
			passWord = InputPW.text;
			LoginID = LoginID.TrimStart(); // 공백문자 제거해서 입력
			passWord.Trim(new char[] { ' ', '\n', '0' });
			StartCoroutine("LoginStart");
		});
	}
	public void LoginState_func(bool state)
	{
		login_state = state;
	}

	IEnumerator LoginStart()
	{

		Packet_Auth_REQ packet= new Packet_Auth_REQ(LoginID,passWord);
		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));
		yield return new WaitForSeconds(3.0f);
		if( !login_state)
		{
			Debug.Log("Login Fail");
			StopCoroutine("LoginStart");
		}
		else
		{
			lobby.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Login : MonoBehaviour
//{
//	private string LoginID = " ";
//	private string passWord = " ";
//	private int mwidth = 0;
//	private int mheight = 0;
//	private bool login_state = false;
//	public GameObject NewIdCreate;
//	public GameObject lobby;
//	// Use this for initialization
//	void Start()
//	{
//		mwidth = Screen.width;
//		mheight = Screen.height;
//	}
//	public void LoginState_func(bool state)
//	{
//		login_state = state;
//	}
//	private void OnApplicationQuit()
//	{
//		gameObject.SetActive(false);
//	}
//	void OnGUI()
//	{
//		//가로 위치, 세로 위치, 가로크기 , 세로크기
//		GUI.Box(new Rect((mwidth / 3.2f), (mheight / 5), (mwidth / 3.5f), (mheight / 2.5f)), "Login & Password In");

//		GUI.TextArea(new Rect((mwidth / 3f), (mheight / 3.0f), (mwidth / 20), (mheight / 27)), "   ID   ");
//		GUI.TextArea(new Rect((mwidth / 3f), (mheight / 2.5f), (mwidth / 20), (mheight / 27)), "   PW   ");
//		LoginID = GUI.TextArea(new Rect((mwidth / 2.5f), (mheight / 3.0f), (mwidth / 10), (mheight / 27)), LoginID);
//		passWord = GUI.PasswordField(new Rect((mwidth / 2.5f), (mheight / 2.5f), (mwidth / 10), (mheight / 30)), passWord, "*"[0], 25);

//		if (GUI.Button(new Rect((mwidth / 1.9f), (mheight / 3.0f), (mwidth / 20.5f), (mheight / 31)), "ID 생성"))
//		{
//			gameObject.SetActive(false);
//			NewIdCreate.SetActive(true);
//		}

//		if (GUI.Button(new Rect(mwidth / 1.9f, mheight / 2.5f, mwidth / 20.5f, mheight / 31), "Login"))
//		{
//			//
//			//lobby.SetActive(true);
//			//gameObject.SetActive(false);
//			//
//			LoginID = LoginID.TrimStart(); // 공백문자 제거해서 입력
//			passWord.Trim(new char[] { ' ', '\n', '0' });
//			StartCoroutine("LoginStart");
//		}


//	}
//	IEnumerator LoginStart()
//	{

//		Packet_Auth_REQ packet = new Packet_Auth_REQ(LoginID, passWord);
//		NetWork_s.Instance.ProcessSend(NetWork_s.Instance.structToByte((object)packet));

//		yield return new WaitForSeconds(3.0f);
//		if (!login_state)
//		{
//			Debug.Log("Login Fail");
//			StopCoroutine("LoginStart");
//		}
//		else
//		{
//			lobby.SetActive(true);
//			gameObject.SetActive(false);
//		}
//	}
//}

