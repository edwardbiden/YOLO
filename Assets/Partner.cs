using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Partner : MonoBehaviour {

	public Text hotnessText;
	public Text intelligenceText;
	public Text kindnessText;
	public Text wealthText;
	public Text careerText;
	public Text nameText;

	public int partnerAge;
	private float partnerFloat;
	public int partnerMonth;

	public int partnerChildren;
	public int partnerDivorces;

	public string[] aspectRate = {"Poor","Average","Good","Excellent"};
	public float[] aspectvalue = {0,0,0,0,0};
	public float[] exvalue = {0,0,0,0,0};
	public float[] maxaspectvalue = {0,0,0,0,0};
	public string[] aspectText = {"","","","",""};
	private float wealthGap;
	
	private string[] femaleNames = {"Alison","Anna","Anne","Amelia","Beth","Beatrix","Claire","Charlotte","Deborah","Eve","Emma","Emily","Faye","Gemma","Holly","Harriet","Juliet","Jess","Jessica","Lucy","Monica","Nancy","Nicole","Olive","Pia","Rose","Rebecca","Stephanie","Sabine","Sarah","Tanja","Violet"};
	private string[] maleNames = {"Adam","Brian","Charlie","Dave","Ed","Fred","Georg","George","Harry","Jack","Jonathan","John","James","Luke","Liam","Mike","Nigel","Nick","Oliver","Pablo","Patrick","Roger","Simon","Tom","Vincent"};
	public string partnerName;
	public string exName;
	public int exDuration;
	private int nameCount;

	private Relationship relationship;
	private Display display;
	private Career career;
	public GameObject relationshipText;
	public GameObject canvas;

	void Start ()
	{
		relationship = relationshipText.GetComponent<Relationship>();
		display = canvas.GetComponent<Display>();
		partnerName = "";
		partnerDivorces = 0;
		partnerChildren = 0;
		exDuration = 0;
		wealthGap = 0;
	}

	public void Birth() 
	{
		for (int i = 0; i < 4; i++)
		{
			aspectvalue[i] = Random.Range(1, 11);
			maxaspectvalue[i] = 0;
			if ( aspectvalue[i] < 4 )
			{
				aspectText[i] = aspectRate[0];
			}
			if ( aspectvalue[i] < 7 && aspectvalue[i] >= 4 )
			{
				aspectText[i] = aspectRate[1];
			}
			if ( aspectvalue[i] < 9 && aspectvalue[i] >= 7)
			{
				aspectText[i] = aspectRate[2];
			}
			if ( aspectvalue[i] >= 9 )
			{
				aspectText[i] = aspectRate[3];
			}



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
		}
		aspectvalue[3] = Random.Range (0, 10 - (partnerAge / 10));
		aspectvalue[4] = Random.Range (18, partnerAge * 2) - 18;

		partnerChildren = 0;
		partnerDivorces = 0;
		
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
	}

	public void CreateEx()
	{
		exName = partnerName;
		exDuration = display.duration;
		for (int i =0; i <4; i++)
		{
			exvalue[i] = aspectvalue[i];
		}
	}

	public void StatsUpdate( float[] statsArray, float[] statsMaxArray, int statsAge, bool male, int children, int divorces, bool player)
	{
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
		if (player == false)
		{
			statsArray[4] += Random.Range(0f,0.5f);
		}

		// adjust wealth
		wealthGap = (statsArray[4] / 15) - statsArray[3];

		if (wealthGap < 3)
		{
			statsArray[3] += Random.Range(0f,0.1f);
		}
		if (wealthGap > 0 && wealthGap <= 3)
		{
			statsArray[3] += Random.Range(0f,0.05f);
		}
		if (wealthGap > -3 && wealthGap <= 0)
		{
			statsArray[3] += Random.Range(-0.05f,0f);
		}
		if (wealthGap <= -3 )
		{
			statsArray[3] += Random.Range(-0.1f,0f);
		}

		statsArray[3] -= children * 0.02f;
		statsArray[3] -= divorces * 0.02f;

		for (int i = 0; i < 4; i++)
		{
			if ( statsArray[i] >= statsMaxArray[i] )
			{
				statsMaxArray[i] = statsArray[i];
			}
		}
	}
}
