using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Win : MonoBehaviour {
	public GameObject chat;
	private Button next;
	// Use this for initialization
	void Start () {
		next = GameObject.Find("Win_Score_Canvas").transform.Find("Button").GetComponent<Button>();
		next.onClick.AddListener(() =>
		{
			chat.SetActive(true);
			gameObject.SetActive(false);
		
		});
	}
	
	// Update is called once per frame
	void Update () {
	}
}
