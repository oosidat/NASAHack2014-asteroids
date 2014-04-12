using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;

public class DiveJava : MonoBehaviour {

	private string cacheDir = "Push to get cache dir";
	private string startURI = "Push to get startURI";
	private static int start_once;

#if UNITY_ANDROID
	private static AndroidJavaClass javadivepluginclass;
	private static AndroidJavaClass javaunityplayerclass;
	private static AndroidJavaObject currentactivity;
	private static AndroidJavaObject javadiveplugininstance;

#endif


	void Start(){
		start_once=0;
		#if UNITY_EDITOR
		
		#elif UNITY_ANDROID
		//IntPtr stringPtr=getStartURI();
		//String cache = Marshal.PtrToStringAnsi(stringPtr);
		//startURI=cache;

		javadivepluginclass = new AndroidJavaClass("com.shoogee.divejava.divejava") ;
		javaunityplayerclass= new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		currentactivity = javaunityplayerclass.GetStatic<AndroidJavaObject>("currentActivity");
		javadiveplugininstance = javadivepluginclass.CallStatic<AndroidJavaObject>("instance");
		object[] args={currentactivity};
		javadiveplugininstance.Call<string>("set_activity",args);

		String answer;
		answer= javadiveplugininstance.Call<string>("setFullscreen");
		


		#elif UNITY_IPHONE

		#endif 	




	}



	public void Update(){
		if (start_once>0)start_once--;
		return;
	}

		
	
	public static void setFullscreen(){
		#if UNITY_EDITOR
		
		#elif UNITY_ANDROID

		String answer;
		answer= javadiveplugininstance.Call<string>("setFullscreen");

		
		#elif UNITY_IPHONE
		
		#endif 	
		
		return;
	}
	
}
