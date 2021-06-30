using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour {

	public Text m_score_Bar;
	public int m_total_Score;
	// Use this for initialization
	void Start () {
		m_score_Bar = gameObject.GetComponent<Text>();
		m_total_Score = 0;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "enemy")
		{
			AddScore_And_PrintScore(50);
			Destroy(other.gameObject);
		}
	}
	// Update is called once per frame
	void Update ()
	{

		//if (Input.GetMouseButton(0))
		//{
		//	AddScore_And_PrintScore(50);
		//}
	}

	public void AddScore_And_PrintScore(int _score)
	{
		m_total_Score += _score;
		m_score_Bar.text = "Score : " + m_total_Score.ToString();
		
	}
}
