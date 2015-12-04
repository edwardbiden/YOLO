﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Obituary : MonoBehaviour {

	public Text tombstone;
	public int partnerCount;
	public string[] partnerNames = {"","","","","","","","","",""};

	private string[] numbers = {"zero","one","two","three","four","five","six","seven","eight","nine","ten"};
	private string[] wealthStates = {"destitute","poor","making ends meet","comfortable","well-off","rich"};
	private int[] wealthThresholds = {20,40,60,80,100,120};

	private Partner partner;
	private Display display;
	public GameObject canvas;

	// partners first; longest; last
	public string[] soNames = {"","",""};
	public int[] soLength = {0,0,0};
	public int[] soChildren = {0,0,0};
	public bool[] marriedSO = {false,false,false};
	public int previousChildren;
	private int nameGen;
	private string tempName;

	void Start ()
	{
		partnerCount = 0;
		partner = canvas.GetComponent<Partner>();
		display = canvas.GetComponent<Display>();
		previousChildren = 0;
	}

	public void Relationship ()
	{
		partnerNames[partnerCount] = partner.partnerName;
		partnerCount++;
	}

	public void Write () 
	{
		tombstone.text = display.nameText.text + ", you lived to " + display.age + ". ";

		// number of marriages
		if (display.marriageCount == 0)
		{
			tombstone.text += "You but never married";
		}
		if (display.marriageCount == 1)
		{
			tombstone.text += "You married only once";
		}
		if (display.marriageCount == 2)
		{
			tombstone.text += "You married twice";
		}
		if (display.marriageCount > 2)
		{
			tombstone.text += "You married " + numbers[display.marriageCount] + " times"; //+ display.marriageCount.ToString()
		}

		// number of children
		if (display.childrenCount == 0)
		{
			tombstone.text += ", and died childless. ";
		}
		if (display.childrenCount == 1)
		{
			tombstone.text += ", and had just one child. ";
		}
		if (display.childrenCount > 1)
		{
			tombstone.text += ", and had " + numbers[display.childrenCount] + " children. "; //+ display.childrenCount.ToString()
		}

		// career
		if (display.aspectText[4].StartsWith("A"))
		{
			tombstone.text += "You were an " + display.aspectText[4].ToLower() + " in your field and died ";
		}
		else
		{
			tombstone.text += "You were a " + display.aspectText[4].ToLower() + " in your field and died ";
		}

		// wealth
		if ( display.aspectvalue[3] < wealthThresholds[0] )
		{
			tombstone.text += wealthStates[0];
		}
		if ( display.aspectvalue[3] < wealthThresholds[1] && display.aspectvalue[3] >= wealthThresholds[0])
		{
			tombstone.text += wealthStates[1];
		}
		if ( display.aspectvalue[3] < wealthThresholds[2] && display.aspectvalue[3] >= wealthThresholds[1])
		{
			tombstone.text += wealthStates[2];
		}
		if ( display.aspectvalue[3] < wealthThresholds[3] && display.aspectvalue[3] >= wealthThresholds[2])
		{
			tombstone.text += wealthStates[3];
		}
		if ( display.aspectvalue[3] < wealthThresholds[4] && display.aspectvalue[3] >= wealthThresholds[3])
		{
			tombstone.text += wealthStates[4];
		}
		if ( display.aspectvalue[3] >= wealthThresholds[4])
		{
			tombstone.text += wealthStates[5];
		}

		tombstone.text += ". ";

		if ( display.relationshipCount > 0 ) 
		{
			significantOther( soNames[0], soLength[0], marriedSO [0], soChildren [0]);
		}
		if ( soLength[1] > 0 ) 
		{
			significantOther( soNames[1], soLength[1], marriedSO [1], soChildren [1]);
		}
		if ( soLength[2] > 0 ) 
		{
			significantOther( soNames[2], soLength[2], marriedSO [2], soChildren [2]);
		}
		if ( display.relationshipCount == 0 ) 
		{
			tombstone.text += ". \n\nYou never hooked up. Ever.";
		}
	}

	public void significantOther ( string name, int length, bool married, int children )
	{
		tombstone.text += "\n\n";
		tombstone.text += "You were ";
		if ( married == true )
		{
			tombstone.text += "married to ";
		}
		else
		{
			tombstone.text += "with ";
		}
		tombstone.text += name + " for ";
		
		if ( length > 11 )
		{
			tombstone.text += Mathf.Round(length/12).ToString() + " years, ";
		}
		else
		{
			tombstone.text += length.ToString() + " months, ";
		}
		
		if ( children == 0 )
		{
			tombstone.text += "but you never had children together. ";
		}
		if ( children == 1 )
		{
			tombstone.text += "and you had just one child together: " + nameGenerator(tempName);
		}
		if ( children == 2 )
		{
			tombstone.text += "and you had two children together: " + nameGenerator(tempName) + " and " + nameGenerator(tempName) + ". ";
		}
		if ( children > 2)
		{
			tombstone.text += "and you had " + numbers[children] + " children together: ";
			for (int i = 1; i < (children - 1); i++)
			{
				tombstone.text += nameGenerator(tempName) + ", ";
			}
			
			tombstone.text += nameGenerator(tempName) + " and " + nameGenerator(tempName) + ". ";
		}
	}

	public string nameGenerator (string name) 
	{
		if ( Random.Range(0,100) <= 50 )
		{
			nameGen = Random.Range(0, partner.femaleNames.Length);
			return name = partner.femaleNames[nameGen];
		}
		else 
		{
			nameGen = Random.Range(0, partner.maleNames.Length);
			return name += partner.maleNames[nameGen];
		}
	}
}
