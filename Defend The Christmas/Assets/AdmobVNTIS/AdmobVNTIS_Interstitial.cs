using UnityEngine;
using System.Collections;

public class AdmobVNTIS_Interstitial : MonoBehaviour {

	public string PublisherID = "YOUR_AD_UNIT_ID";
	public string TestDeviceID = "";
	public bool ShowInterstitialOnLoad = false;

	private static AndroidJavaObject jo;

	// Dont destroy on load and prevent duplicate
	private static bool created = false;
	void Awake() {
		if (!created) {
			DontDestroyOnLoad(this.gameObject);
			created = true;
			initializeInterstitial ();
		} else {
			Destroy(this.gameObject);
		}
	}

	void initializeInterstitial(){
		jo = new AndroidJavaObject ("admob.admob",PublisherID,TestDeviceID,ShowInterstitialOnLoad);
	}

	/// <summary>
	/// Load and show the interstitial.
	/// </summary>
	public void showInterstitial(){
		jo.Call ("showInterstitial");
	}
}
