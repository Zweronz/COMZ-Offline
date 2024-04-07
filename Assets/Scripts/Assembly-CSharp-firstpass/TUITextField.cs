using UnityEngine;

public class TUITextField : TUIControlImpl
{
	public TextFieldType text_type;

	public Vector2 positon = Vector2.zero;

	public int length = 6;

	public GUIStyle style;

	protected string textToEdit = string.Empty;

	public Vector2 tex_off = Vector2.zero;

	public Vector2 content_off = Vector2.zero;

	public OnTextFieldActive callback;

	public new void Start()
	{
		base.Start();
	}

	public void OnEnable()
	{
	}

	public void OnDisable()
	{
	}

	private void OnGUI()
	{
		int num = ResetResolution();
		float left = (float)(Screen.width / 2) - (size.x / 2f - base.transform.localPosition.x - positon.x) * (float)num;
		float top = (float)(Screen.height / 2) - (size.y / 2f + base.transform.localPosition.y + positon.y) * (float)num;
		if (text_type == TextFieldType.text)
		{
			textToEdit = GUI.TextArea(new Rect(left, top, size.x * (float)num, size.y * (float)num), textToEdit, length, style);
		}
		else if (text_type == TextFieldType.password)
		{
			textToEdit = GUI.PasswordField(new Rect(left, top, size.x * (float)num, size.y * (float)num), textToEdit, "*"[0], length, style);
		}
		if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
		{
			Debug.Log("Sending login request");
		}
		if (textToEdit != null && callback != null)
		{
			callback();
		}
	}

	public Vector2 AutoSize(Vector2 size)
	{
		int num = ResetResolution();
		if (num == 2 || num == 4)
		{
			return new Vector2(size.x * (float)num, size.y * (float)num);
		}
		return new Vector2(size.x, size.y);
	}

	public int ResetResolution()
	{
		int num = 2;
		if (Screen.width == 960 || Screen.width == 640)
		{
			return 2;
		}
		if (Screen.width == 480 || Screen.width == 320)
		{
			return 1;
		}
		if (Screen.width == 2048 || Screen.width == 1536)
		{
			return 4;
		}
		return 2;
	}

	public string GetText()
	{
		return textToEdit;
	}

	public void ResetText()
	{
		textToEdit = string.Empty;
	}
}
