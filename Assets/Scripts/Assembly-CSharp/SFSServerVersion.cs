using System.Collections;
using System.IO;
using UnityEngine;
using Zombie3D;

public class SFSServerVersion : MonoBehaviour
{
	public string url = "http://account.trinitigame.com/game/callofminizombies/CoMZombies_VS_version.bytes";

	protected string content = string.Empty;

	public OnSFSServerVersion callback;

	public OnSFSServerVersionError callback_error;

	public string VsDomain = string.Empty;

	public string VsStandbyServerIP = string.Empty;

	public int VsServerPort;

	public int VsGroupIdMin;

	public int VsGroupIdMax;

	public string CoopDomain = string.Empty;

	public string CoopStandbyServerIP = string.Empty;

	public int CoopServerPort;

	public int CoopMapIdMin;

	public bool test;

	private IEnumerator Start()
	{
		if (test)
		{
			yield return 1;
			content = Utils.GetTextAsset("CoMZombies_VS_version");
			string path = Application.dataPath + "/../Documents/";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			content = GameState.GameSaveStringEncipher(content, 30);
			FileWrite(path + "CoMZombies_VS_version.bytes", content);
			Debug.Log("CoMZombies_VS_version.bytes output is ok.");
			yield break;
		}
		url = "http://account.trinitigame.com/game/callofminizombies/CoMZombies_VS_Android.bytes";
		WWW www = new WWW(url + "?rand=" + Random.Range(10, int.MaxValue));
		yield return www;
		if (www.error != null)
		{
			Debug.Log("error load url: " + www.error);
			if (callback_error != null)
			{
				callback_error();
			}
			yield break;
		}
		content = www.text;
		content = GameState.GameSaveStringEncipher(content, 30);
		Debug.Log(content);
		Configure cfg = new Configure();
		cfg.Load(content);
		float ver = float.Parse(cfg.GetSingle("CoMZ", "Ver"));
		float ver_test = float.Parse(cfg.GetSingle("CoMZ", "TestVer"));
		GameApp.GetInstance().GetGameState().show_zombies2_link = int.Parse(cfg.GetSingle("CoMZ", "AdLink")) == 1;
		if (Mathf.Abs(ver - 4.4f) < 0.0001f)
		{
			Debug.Log("to normal server.");
			VsDomain = cfg.GetSingle("CoMZ", "VsDomain");
			VsStandbyServerIP = cfg.GetSingle("CoMZ", "VsStandbyServerIP");
			VsServerPort = int.Parse(cfg.GetSingle("CoMZ", "VsServerPort"));
			VsGroupIdMin = int.Parse(cfg.GetSingle("CoMZ", "VsGroupIdMin"));
			VsGroupIdMax = int.Parse(cfg.GetSingle("CoMZ", "VsGroupIdMax"));
			CoopDomain = cfg.GetSingle("CoMZ", "CoopDomain");
			CoopStandbyServerIP = cfg.GetSingle("CoMZ", "CoopStandbyServerIP");
			CoopServerPort = int.Parse(cfg.GetSingle("CoMZ", "CoopServerPort"));
			CoopMapIdMin = int.Parse(cfg.GetSingle("CoMZ", "CoopGroupIdMin"));
			if (callback != null)
			{
				callback(true);
			}
		}
		else if (Mathf.Abs(ver_test - 4.4f) < 0.0001f)
		{
			Debug.Log("to test server.");
			VsDomain = cfg.GetSingle("CoMZ", "TestVsDomain");
			VsStandbyServerIP = cfg.GetSingle("CoMZ", "TestVsStandbyServerIP");
			VsServerPort = int.Parse(cfg.GetSingle("CoMZ", "TestVsServerPort"));
			VsGroupIdMin = int.Parse(cfg.GetSingle("CoMZ", "TestVsGroupIdMin"));
			VsGroupIdMax = int.Parse(cfg.GetSingle("CoMZ", "TestVsGroupIdMax"));
			CoopDomain = cfg.GetSingle("CoMZ", "TestCoopDomain");
			CoopStandbyServerIP = cfg.GetSingle("CoMZ", "TestCoopStandbyServerIP");
			CoopServerPort = int.Parse(cfg.GetSingle("CoMZ", "TestCoopServerPort"));
			CoopMapIdMin = int.Parse(cfg.GetSingle("CoMZ", "TestCoopGroupIdMin"));
			if (callback != null)
			{
				callback(true);
			}
		}
		else if (callback != null)
		{
			callback(false);
		}
	}

	public void FileWrite(string FileName, string WriteString)
	{
		FileStream fileStream = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
		StreamWriter streamWriter = new StreamWriter(fileStream);
		streamWriter.WriteLine(WriteString);
		streamWriter.Flush();
		streamWriter.Close();
		fileStream.Close();
	}
}
