using System.Collections;
using UnityEngine;

public class TrinitiUIScript : MonoBehaviour
{
	private IEnumerator Start()
	{
		FlurryPlugin.StartSession("SVUM3MKWJ9LRFPFQJ1F4");
		if (Utils.IsJailbreak())
		{
			FlurryPlugin.logEvent("Jailbreak");
		}
		OpenClikPlugin.Initialize("567D21BF-DA59-41F2-B7CC-9951F6187640");
		PushLocalNotification.SetPushNotification();
		LocalNotificationWrapper.CancelAll();
		Debug.Log("save file: " + Application.persistentDataPath + "/");
		if (Application.isEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			Application.runInBackground = true;
		}
		else
		{
			Application.runInBackground = false;
		}
		yield return 1;
		Application.targetFrameRate = 60;
		Application.LoadLevel("StartMenuTUI");
	}
}
