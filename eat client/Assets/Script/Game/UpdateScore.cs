using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateScore : MonoBehaviour {

	public Image m_hpBar_Image;
	private float m_totalHp;
	private float m_minusHp;
	// Use this for initialization
	void Start () {
		m_hpBar_Image = gameObject.GetComponent<Image>();
		m_totalHp = 100;
		m_minusHp = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			m_hpBar_Image.fillAmount -= m_minusHp / m_totalHp;
		}
	}
}
