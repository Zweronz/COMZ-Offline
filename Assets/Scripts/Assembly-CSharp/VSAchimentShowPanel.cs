using System.Collections.Generic;
using UnityEngine;
using Zombie3D;

public class VSAchimentShowPanel : MonoBehaviour
{
	public List<GameObject> achievement_buttons = new List<GameObject>();

	public void Awake()
	{
		int num = 0;
		string empty = string.Empty;
		foreach (VsAchievementCfg item in GameApp.GetInstance().GetGameConfig().Vs_AchievementConfTable)
		{
			if (item.level == 1)
			{
				GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/TUI/Achivment_Button")) as GameObject;
				gameObject.name = "Achivment_Button" + item.m_class;
				gameObject.transform.parent = base.gameObject.transform;
				gameObject.GetComponent<AchievementData>().vs_achievement_data = item;
				achievement_buttons.Add(gameObject);
				int num2 = item.icon.IndexOf('_');
				empty = item.icon.Substring(0, num2 + 1) + "0";
				gameObject.GetComponent<TUIButtonClick>().frameNormal.GetComponent<TUIMeshSprite>().frameName_Accessor = empty;
				gameObject.GetComponent<TUIButtonClick>().framePressed.GetComponent<TUIMeshSprite>().frameName_Accessor = empty;
				gameObject.GetComponent<TUIButtonClick>().frameDisabled.GetComponent<TUIMeshSprite>().frameName_Accessor = empty;
				gameObject.transform.localPosition = new Vector3(-120 + num % 4 * 80, 70 - num / 4 * 80, gameObject.transform.localPosition.z);
				num++;
			}
		}
		GameObject gameObject2 = null;
		num = 0;
		foreach (VsAchievementCfg item2 in GameApp.GetInstance().GetGameConfig().Vs_AchievementConfTable)
		{
			gameObject2 = FindAchivmentItem(item2.m_class);
			if (gameObject2 != null && item2.finish)
			{
				gameObject2.GetComponent<AchievementData>().vs_achievement_data = item2;
				gameObject2.GetComponent<TUIButtonClick>().frameNormal.GetComponent<TUIMeshSprite>().frameName_Accessor = item2.icon;
				gameObject2.GetComponent<TUIButtonClick>().framePressed.GetComponent<TUIMeshSprite>().frameName_Accessor = item2.icon;
				gameObject2.GetComponent<TUIButtonClick>().frameDisabled.GetComponent<TUIMeshSprite>().frameName_Accessor = item2.icon;
			}
			else if (gameObject2 != null && !item2.finish && gameObject2.GetComponent<AchievementData>().vs_achievement_data.finish && item2.level - gameObject2.GetComponent<AchievementData>().vs_achievement_data.level == 1)
			{
				gameObject2.GetComponent<AchievementData>().vs_achievement_data = item2;
			}
			gameObject2 = null;
		}
	}

	public void Start()
	{
	}

	public void Update()
	{
	}

	public GameObject FindAchivmentItem(string class_name)
	{
		GameObject result = null;
		foreach (GameObject achievement_button in achievement_buttons)
		{
			if (achievement_button.GetComponent<AchievementData>().vs_achievement_data.m_class == class_name)
			{
				return achievement_button;
			}
		}
		return result;
	}
}
