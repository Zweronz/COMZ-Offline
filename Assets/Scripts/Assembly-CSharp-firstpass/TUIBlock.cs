using UnityEngine;

public class TUIBlock : TUIControlImpl
{
	public new void Start()
	{
		if (Mathf.Max(Screen.width, Screen.height) >= 960)
		{
			int num = Screen.width;
			int num2 = Screen.height;
			if (Mathf.Max(Screen.width, Screen.height) == 2048)
			{
				num /= 2;
				num2 /= 2;
			}
			float x = (float)num / 960f;
			float y = (float)num2 / 640f;
			base.transform.localScale = new Vector3(x, y, base.transform.localScale.z);
		}
	}

	public override bool HandleInput(TUIInput input)
	{
		if (PtInControl(input.position))
		{
			return true;
		}
		return false;
	}
}
