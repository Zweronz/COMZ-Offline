using UnityEngine;

public class PasswordPanelManager : MonoBehaviour
{
	public bool createOrJoin = true;

	public TUITextField textInput;

	public TUIMeshSprite passwordActive;

	private void Start()
	{
		if (createOrJoin)
		{
			textInput.style.overflow.left *= textInput.ResetResolution();
			textInput.style.padding.right *= textInput.ResetResolution();
			textInput.style.padding.left *= textInput.ResetResolution();
			textInput.callback = OnTextFieldActive;
		}
		else
		{
			textInput.style.overflow.left *= textInput.ResetResolution();
			textInput.style.overflow.right *= textInput.ResetResolution();
			textInput.style.padding.right *= textInput.ResetResolution();
			textInput.style.padding.left *= textInput.ResetResolution();
		}
	}

	private void Update()
	{
	}

	private void OnTextFieldActive()
	{
		if (textInput.GetText().Length > 0)
		{
			passwordActive.frameName_Accessor = "mima3";
		}
		else
		{
			passwordActive.frameName_Accessor = "mima2";
		}
	}
}
