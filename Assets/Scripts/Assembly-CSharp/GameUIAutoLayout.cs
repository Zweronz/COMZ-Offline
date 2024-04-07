using UnityEngine;

public class GameUIAutoLayout : MonoBehaviour
{
	public bool left;

	public bool right;

	public bool top;

	public bool bottom;

	private void Start()
	{
		if (Mathf.Max(Screen.width, Screen.height) >= 960)
		{
			float x = 0f;
			float y = 0f;
			int num = Screen.width;
			int num2 = Screen.height;
			if (Mathf.Max(Screen.width, Screen.height) == 2048)
			{
				num /= 2;
				num2 /= 2;
			}
			if (left)
			{
				x = base.transform.position.x - (float)(num - 960) / 4f;
			}
			if (right)
			{
				x = base.transform.position.x + (float)(num - 960) / 4f;
			}
			if (top)
			{
				y = base.transform.position.y + (float)(num2 - 640) / 4f;
			}
			if (bottom)
			{
				y = base.transform.position.y - (float)(num2 - 640) / 4f;
			}
			base.transform.position = new Vector3(x, y, base.transform.position.z);
		}
	}
}
