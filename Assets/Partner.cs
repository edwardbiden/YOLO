using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Partner : MonoBehaviour {

	public Text hotnessText;
	public Text personalityText;
	public Text wealthText;
	public Text careerText;
	public Text nameText;

	public int partnerAge;
	private float partnerFloat;
	public int partnerMonth;

	public int partnerChildren;
	public int partnerDivorces;

//	public string[] aspectRate = {"Poor","Average","Good","Excellent"};
	public float[] aspectvalue = {0,0,0,0};
	public float[] exvalue = {0,0,0,0};
	public float[] maxaspectvalue = {0,0,0,0};
	public string[] aspectText = {"","","",""};
	private float wealthGap;
	
	public string[] femaleNames = {"Alison","Anna","Anne","Amelia","Beth","Beatrix","Catherine","Claire","Charlotte","Deborah","Eve","Emma","Emily","Faye","Gemma","Holly","Harriet","Juliet","Jess","Jessica","Lucy","Monica","Nancy","Nicole","Olive","Pia","Rose","Rebecca","Stephanie","Sabine","Sarah","Tanja","Violet"};
	public string[] maleNames = {"Adam","Andy","Andrew","Brian","Charlie","Dave","Ed","Fred","Georg","George","Harry","Jack","Jonathan","John","James","Luke","Liam","Mike","Nigel","Nick","Oliver","Pablo","Patrick","Roger","Simon","Tom","Vincent"};
	public string partnerName;
	public string exName;
	public int exDuration;
	private int nameCount;

	private Relationship relationship;
	private Display display;
	private Career career;
	private TextUpdate textupdate;
	public GameObject canvas;
	public float exHappiness;

	// invest 0: relationship, 1: do nothing, 2: career
	public int invest;
	private float decide;
	public float[] investRange = {33.5f,66.5f};
	private int investCount;

	void Start ()
	{
		relationship = canvas.GetComponent<Relationship>();
		display = canvas.GetComponent<Display>();
		textupdate = canvas.GetComponent<TextUpdate>();
		partnerName = "";
		partnerDivorces = 0;
		partnerChildren = 0;
		exDuration = 0;
		wealthGap = 0;
		exHappiness = 0;

	}

	public void Birth() 
	{
		partnerMonth = 0;
		if ( display.playerIsMale == true )
		{
			partnerFloat = Random.Range( display.age / 2 + 7, display.age * 1.1f);	
		}
		if ( display.playerIsMale == false )
		{
			partnerFloat = Random.Range( display.age / 1.1f , (display.age - 7) * 2);	
		}
		partnerAge = (int)partnerFloat;

		for (int i = 0; i < 2; i++)
		{
			aspectvalue[i] = Random.Range(1, 11);
		}
		aspectvalue[2] = Random.Range (0, 10 - (partnerAge / 10));
		aspectvalue[3] = Random.Range (18, partnerAge * 2) - 18;
		display.careerFillMaxPartner = textupdate.careerThresholds[0];
		display.careerFillMinPartner = 0;
		display.careerStepPartner = 0;

		partnerChildren = 0;
		partnerDivorces = 0;
		investRange[0] = 33.5f;
		investRange[1] = 66.5f;
		relationship.partnerChemistry = Random.Range (relationship.chemistryMin,relationship.chemistryMin);
		
		if ( display.playerIsMale == true )
		{
			nameCount = Random.Range(0,femaleNames.Length);
			partnerName = femaleNames[nameCount];
		}
		else
		{
			nameCount = Random.Range(0,maleNames.Length);
			partnerName = maleNames[nameCount];		
		}
		nameText.text = partnerName;
		relationship.relationshipStrength = 50;
		investCount = 0;
	}

	public void CreateEx()
	{
		exName = partnerName;
		exDuration = display.duration;
		exHappiness = display.maxHappiness;
		for (int i =0; i <3; i++)
		{
			exvalue[i] = aspectvalue[i];
		}
	}

	public void StatsUpdate( float[] statsArray, float[] statsMaxArray, int statsAge, bool male, int children, int divorces, bool player)
	{
		// Decide what to do
		if ( investCount == 0 )
		{
			decide = Random.Range(0,100f);
			if ( decide <= investRange[0] ) 
			{
				investRange[0] += 1.0f;
				invest = 0;
				Debug.Log ("focus on relationship " + investRange[0]);
			}
			if ( decide > investRange[0] && decide <= investRange[1] ) 
			{
				investRange[0] -= 0.5f;
				investRange[1] += 0.5f;
				invest = 1;
				Debug.Log ("do nothing " + investRange[0] + " : " + investRange[1]);
			}
			if ( decide > investRange[1] ) 
			{
				investRange[1] -= 0.1f;
				invest = 2;
				Debug.Log ("focus on career " + investRange[1]);
			}
			investCount = 3;
		}
		else
		{
			investCount--;
		}

		// adjust hotness
		if ( male == true )
		{
			if ( statsAge < 28 && statsArray[0] <= 10 )
			{
				statsArray[0] += ( 4f / ( 12f * 14f ));
			}
			if ( statsAge > 35 && statsAge <= 50 && statsArray[0] >= 0 )
			{
				statsArray[0] -= ( 4f / ( 12f * 15f ));
			}
			if ( statsAge > 50 && statsArray[0] >= 0 )
			{
				statsArray[0] -= ( 6f / ( 12f * 30f ));
			}
		}
		else
		{
			if ( statsAge < 23 && statsArray[0] <= 10 )
			{
				statsArray[0] += ( 2f / ( 12f * 5f ));
			}
			if ( statsAge > 25 && statsAge <= 35 && statsArray[0] >= 0 )
			{
				statsArray[0] -= ( 3f / ( 12f * 10f ));
			}
			if ( statsAge > 35 && statsArray[0] >= 0 )
			{
				statsArray[0] -= ( 6f / ( 12f * 45f ));
			}
		}

		// adjust Career
		if (player == false && invest == 2 )
		{
			statsArray[3] += 0.5f;
		}

		// adjust wealth
		wealthGap = (statsArray[3] / 15) - statsArray[2];

		if (wealthGap < 3)
		{
			statsArray[2] += Random.Range(0f,0.1f);
		}
		if (wealthGap > 0 && wealthGap <= 3)
		{
			statsArray[2] += Random.Range(0f,0.05f);
		}
		if (wealthGap > -3 && wealthGap <= 0)
		{
			statsArray[2] += Random.Range(-0.05f,0f);
		}
		if (wealthGap <= -3 )
		{
			statsArray[2] += Random.Range(-0.1f,0f);
		}

		statsArray[2] -= children * 0.02f;
		statsArray[2] -= divorces * 0.02f;

		for (int i = 0; i < 3; i++)
		{
			if ( statsArray[i] >= statsMaxArray[i] )
			{
				statsMaxArray[i] = statsArray[i];
			}
		}
	}
}
