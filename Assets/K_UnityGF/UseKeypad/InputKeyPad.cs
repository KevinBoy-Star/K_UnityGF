using K_UnityGF;

/// <summary>
/// 小键盘输入框
/// </summary>
public class InputKeyPad : UIInputField
{
    protected override void OnStartAction()
    {
        base.OnStartAction();
        Keypad.Event_UpdateValue += UpdateValue;
    }

    protected override void OnDestroyAction()
    {
        base.OnDestroyAction();
        Keypad.Event_UpdateValue -= UpdateValue;
    }

    /// <summary>
    /// 输入框点击
    /// </summary>
    public void InputFieldClick()
    {
        //Delegation.AddEvent(Delegation.Event_ShowItem,"UseKeypad");
    }

    private void UpdateValue(string _value)
    {
        widget.text = _value;
    }
}
