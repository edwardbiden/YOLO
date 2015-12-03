using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Obituary : MonoBehaviour {

	public Text tombstone;
	public int partnerCount;
	public string[] partnerNames = {"","","","","","","","","",""};

	private string[] wealthStates = {"destitute","poor","making ends meet","comfortable","well-off","rich"};
	private int[] wealthThresholds = {20,40,60,80,100,120};

	private Partner partner;
	private Display display;
	public GameObject canvas;

	void Start ()
	{
		partnerCount = 0;
		partner = canvas.GetComponent<Partner>();
		display = canvas.GetComponent<Display>();
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
			tombstone.text += "You married " + display.marriageCount.ToString() + " times";
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
			tombstone.text += ", and had " + display.childrenCount.ToString() + " children. ";
		}

		// career
		tombstone.text += "You were a " + display.aspectText[4].ToLower() + " in your field and died ";

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
	}
}
