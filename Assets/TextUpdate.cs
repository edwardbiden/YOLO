using UnityEngine;
using System.Collections;

public class TextUpdate : MonoBehaviour {

	public string[] wealthRate = {"$","$$","$$$","$$$$","$$$$$"};
	public string[] careerStates = {"student","intern","junior","competent","professional","expert","guru"};
	public string[] looksStates = {"repulsive","ugly","plain","attractive","hot"};
	public string[] personalityStates = {"boring","awkward","pleasant","charming","inspiring"};
	public int[] careerThresholds = {10,25,45,70,100,130};

	// Use this for initialization
	void Start () {
	
	}
	
	public void Process (float[] values, string[] texts)
	{
		// looks
		if ( values[0] < 3 )
		{
			texts[0] = looksStates[0];
		}
		if ( values[0] < 4 && values[0] >= 3 )
		{
			texts[0] = looksStates[1];
		}
		if ( values[0] < 6 && values[0] >= 4 )
		{
			texts[0] = looksStates[2];
		}
		if ( values[0] < 8 && values[0] >= 6 )
		{
			texts[0] = looksStates[3];
		}	
		if ( values[0] >= 8 )
		{
			texts[0] = looksStates[4];
		}

		// personality
		if ( values[1] < 3 )
		{
			texts[1] = personalityStates[1];
		}
		if ( values[1] < 4 && values[1] >= 3 )
		{
			texts[1] = personalityStates[1];
		}
		if ( values[1] < 6 && values[1] >= 4 )
		{
			texts[1] = personalityStates[2];
		}
		if ( values[1] < 8 && values[1] >= 6 )
		{
			texts[1] = personalityStates[3];
		}	
		if ( values[1] >= 8 )
		{
			texts[1] = personalityStates[4];
		}


		// wealth
		if ( values[2] < 3 )
		{
			texts[2] = wealthRate[0];
		}
		if ( values[2] < 4 && values[2] >= 3 )
		{
			texts[2] = wealthRate[1];
		}
		if ( values[2] < 6 && values[2] >= 4 )
		{
			texts[2] = wealthRate[2];
		}
		if ( values[2] < 8 && values[2] >= 6 )
		{
			texts[2] = wealthRate[3];
		}	
		if ( values[2] >= 8 )
		{
			texts[2] = wealthRate[4];
		}

		// career
		if ( values[3] < careerThresholds[0] )
		{
			texts[3] = careerStates[0];
		}
		if ( values[3] < careerThresholds[1] && values[3] >= careerThresholds[0] )
		{
			texts[3] = careerStates[1];
		}
		if ( values[3] < careerThresholds[2] && values[3] >= careerThresholds[1] )
		{
			texts[3] = careerStates[2];
		}
		if ( values[3] < careerThresholds[3] && values[3] >= careerThresholds[2] )
		{
			texts[3] = careerStates[3];
		}	
		if ( values[3] < careerThresholds[4] && values[3] >= careerThresholds[3] )
		{
			texts[3] = careerStates[4];
		}
		if ( values[3] < careerThresholds[5] && values[3] >= careerThresholds[4] )
		{
			texts[3] = careerStates[5];
		}
		if ( values[3] >= careerThresholds[5] )
		{
			texts[3] = careerStates[6];
		}
	}
}
