using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Obituary : MonoBehaviour {

	public Text tombstone;
	public int partnerCount;
	public string[] partnerNames = {"","","","","","","","","",""};

	private Partner partner;
	public GameObject canvas;

	void Start ()
	{
		partnerCount = 0;
		partner = canvas.GetComponent<Partner>();
	}

	public void Relationship ()
	{
		partnerNames[partnerCount] = partner.partnerName;
		Debug.Log (partnerNames[partnerCount]);
		partnerCount++;
	}

	public void Write () 
	{
		tombstone.text = "Your first parter was " + partnerNames[0];
	}
}
