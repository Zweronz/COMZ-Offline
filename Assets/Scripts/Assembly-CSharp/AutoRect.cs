using UnityEngine;

public class AutoRect
{
	public static Rect AutoPos(Rect rect)
	{
		ResetResolution();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			return new Rect(rect.x * ResolutionConstant.R, rect.y * ResolutionConstant.H, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.H);
		}
		if (Screen.width != 960 && Screen.width != 640 && Screen.width != 480 && Screen.width != 320)
		{
			if ((Screen.width == 1136 && Screen.height == 640) || (Screen.height == 1136 && Screen.width == 640))
			{
				Vector2 vector = new Vector2(960f, 640f);
				Vector2 vector2 = new Vector2(1136f, 640f);
				float left = rect.x + (vector2.x - vector.x) / 2f;
				float top = rect.y + (vector2.y - vector.y) / 2f;
				return new Rect(left, top, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
			}
			if (Screen.width == 2048 || Screen.width == 1536)
			{
				Vector2 vector3 = new Vector2(960f, 640f);
				Vector2 vector4 = new Vector2(1024f, 768f);
				float left2 = vector4.x - vector3.x + rect.x * 2f;
				float top2 = vector4.y - vector3.y + rect.y * 2f;
				return new Rect(left2, top2, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
			}
			Vector2 vector5 = new Vector2(480f, 320f);
			Vector2 vector6 = new Vector2(512f, 384f);
			float left3 = vector6.x + (rect.x - vector5.x) * 1f;
			float top3 = vector6.y + (rect.y - vector5.y) * 1f;
			return new Rect(left3, top3, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
		}
		return new Rect(rect.x * ResolutionConstant.R, rect.y * ResolutionConstant.R, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
	}

	public static Rect AutoValuePos(Rect rect)
	{
		ResetResolution();
		return new Rect(rect.x * ResolutionConstant.R, rect.y * ResolutionConstant.R, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
	}

	public static Vector2 AutoValuePos(Vector2 v)
	{
		ResetResolution();
		return new Vector2(v.x * ResolutionConstant.R, v.y * ResolutionConstant.R);
	}

	public static Vector2 AutoSize(Rect rect)
	{
		ResetResolution();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			return new Vector2(rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.H);
		}
		if (ResolutionConstant.R == 1f)
		{
			return new Vector2(rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
		}
		if (ResolutionConstant.R == 2f)
		{
			return new Vector2(rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
		}
		return new Vector2(rect.width, rect.height);
	}

	public static Vector2 AutoSize(Rect rect, float zoom)
	{
		ResetResolution();
		if (ResolutionConstant.R == 1f || ResolutionConstant.R == 2f)
		{
			return new Vector2(rect.width * ResolutionConstant.R * zoom, rect.height * ResolutionConstant.R * zoom);
		}
		return new Vector2(rect.width * zoom, rect.height * zoom);
	}

	public static Vector2 AutoSize(Vector2 v)
	{
		ResetResolution();
		if (ResolutionConstant.R == 1f || ResolutionConstant.R == 2f)
		{
			return new Vector2(v.x * ResolutionConstant.R, v.y * ResolutionConstant.R);
		}
		return v;
	}

	public static Rect AutoTex(Rect rect)
	{
		ResetResolution();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			return new Rect(rect.x * 1f, rect.y * 1f, rect.width * 1f, rect.height * 1f);
		}
		if (ResolutionConstant.R == 2f)
		{
			return new Rect(rect.x * 1f, rect.y * 1f, rect.width * 1f, rect.height * 1f);
		}
		return new Rect(rect.x * ResolutionConstant.R, rect.y * ResolutionConstant.R, rect.width * ResolutionConstant.R, rect.height * ResolutionConstant.R);
	}

	public static Vector2 AutoTex(Vector2 v)
	{
		ResetResolution();
		if (ResolutionConstant.R == 2f)
		{
			return new Vector2(v.x * 1f, v.y * 1f);
		}
		return new Vector2(v.x * ResolutionConstant.R, v.y * ResolutionConstant.R);
	}

	public static float AutoValue(float x)
	{
		return x * ResolutionConstant.R;
	}

	public static void ResetResolution()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			ResolutionConstant.R = (float)Screen.width / 960f;
			ResolutionConstant.H = (float)Screen.height / 640f;
		}
		else if (Mathf.Max(Screen.width, Screen.height) == 960)
		{
			ResolutionConstant.R = 1f;
		}
		else if (Mathf.Max(Screen.width, Screen.height) == 480)
		{
			ResolutionConstant.R = 0.5f;
		}
		else if (Mathf.Max(Screen.width, Screen.height) == 2048)
		{
			ResolutionConstant.R = 2f;
		}
		else if (Mathf.Max(Screen.width, Screen.height) == 1136)
		{
			ResolutionConstant.R = 1f;
		}
		else
		{
			ResolutionConstant.R = 1f;
		}
	}

	public static Platform GetPlatform()
	{
		return Platform.Android;
	}

	public static float GetFakeScreenWidth()
	{
		if (GetPlatform() == Platform.iPhone5)
		{
			return 960f;
		}
		return Screen.width;
	}

	public static float GetFakeScreenHeight()
	{
		if (GetPlatform() == Platform.iPhone5)
		{
			return 640f;
		}
		return Screen.height;
	}

	public static float GetFakeScreenWidthHeightRatio()
	{
		float num = 0f;
		float num2 = 0f;
		if (GetPlatform() == Platform.iPhone5)
		{
			num = 960f;
			num2 = 640f;
		}
		else
		{
			num = Screen.width;
			num2 = Screen.height;
		}
		return num2 / num;
	}
}
