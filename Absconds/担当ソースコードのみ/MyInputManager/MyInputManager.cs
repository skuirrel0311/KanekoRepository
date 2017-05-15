using System;
using UnityEngine;
using GamepadInput;

public class MyInputManager : BaseManager<MyInputManager>
{
    public enum Button { A, B, Y, X, RightShoulder, LeftShoulder, RightStick, LeftStick, Back, Start }
    //上記のボタンと同じ順番で記述
    public static string[] Key = { "Space", "E", "Q", "F", "null", "null", "null", "null", "Backspace", "Escape" };
    public enum StickDirection { LeftStickRight, LeftStickLeft, LeftStickUp, LeftStickDown, RightStickRight, RightStickLeft, RightStickUp, RightStickDown }
    public static string[] Stick = { "RightArrow", "LeftArrow", "UpArrow", "DownArrow", "null", "null", "null", "null" };
    public enum Trigger { LeftTrigger, RightTrigger }
    public enum Axis { LeftStick, RightStick, Dpad }

    private static GamepadState[] currentState = new GamepadState[4];
    private static GamepadState[] oldState = new GamepadState[4];

    public static bool IsConnectJoyPad = false;
    private float timer = 0.0f;

    public void Awake()
    {
        GamePad.GamePadInitialize();
        //見つけてきたInstanceが自身でない場合はManagerが２つ存在している
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < currentState.Length; i++)
        {
            currentState[i] = GamePad.GetState((GamePad.Index)(i + 1));
        }

        for (int i = 0; i < currentState.Length; i++)
        {
            oldState[i] = currentState[i];
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;
        //1秒間隔で更新
        if (timer > 0.2f)
        {
            timer = 0.0f;
            string[] temp = Input.GetJoystickNames();
            if(temp.Length != 0)
            {
                IsConnectJoyPad = Input.GetJoystickNames()[0] != "";
            }
        }

        if (!IsConnectJoyPad) return;

        for (int i = 0; i < currentState.Length; i++)
        {
            currentState[i] = GamePad.GetState((GamePad.Index)(i + 1));
        }
    }

    void LateUpdate()
    {
        if (!IsConnectJoyPad) return;

        for (int i = 0; i < currentState.Length; i++)
        {
            oldState[i] = currentState[i];
        }
    }

    public static bool GetButton(Button button, GamePad.Index index = GamePad.Index.One)
    {
        if (IsConnectJoyPad)
        {
            return GamePad.GetButton((GamePad.Button)button, index);
        }
        
        bool keyDown = false;
        if (Key[(int)button] != "null")
        {
            KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), Key[(int)button]);
            keyDown = Input.GetKey(code);
        }

        return keyDown;
    }

    public static bool GetButtonDown(Button button, GamePad.Index index = GamePad.Index.One)
    {
        if (IsConnectJoyPad)
        {
            return GamePad.GetButtonDown((GamePad.Button)button, index);
        }
        bool keyDown = false;
        if (Key[(int)button] != "null")
        {
            KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), Key[(int)button]);
            keyDown = Input.GetKeyDown(code);
        }

        return keyDown;
    }

    public static bool GetButtonUp(Button button, GamePad.Index index = GamePad.Index.One)
    {
        return GamePad.GetButtonUp((GamePad.Button)button, index);
    }

    public static Vector2 GetAxis(Axis axis, GamePad.Index index = GamePad.Index.One)
    {
        if (IsConnectJoyPad)
        {
            return GamePad.GetAxis((GamePad.Axis)axis, index);
        }
        switch (axis)
        {
            case Axis.RightStick:
                return new Vector2(Input.GetAxis("RightHorizontal"), Input.GetAxis("RightVertical"));
            case Axis.LeftStick:
                return new Vector2(Input.GetAxis("LeftHorizontal"), Input.GetAxis("LeftVertical"));
        }
        return Vector2.zero;
    }

    public static float GetTrigger(Trigger trigger, GamePad.Index index = GamePad.Index.One)
    {
        return GamePad.GetTrigger((GamePad.Trigger)trigger, index);
    }

    public static bool IsStickDown(StickDirection direction, GamePad.Index index = GamePad.Index.One)
    {
        Vector2 stick;
        if (direction >= (StickDirection)4)
            stick = GamePad.GetAxis(GamePad.Axis.RightStick, index);
        else
            stick = GamePad.GetAxis(GamePad.Axis.LeftStick, index);
        float dead = 0.3f;
        switch (direction)
        {
            case StickDirection.LeftStickRight:
            case StickDirection.RightStickRight:
                return stick.x > dead;
            case StickDirection.LeftStickLeft:
            case StickDirection.RightStickLeft:
                return stick.x < -dead;
            case StickDirection.LeftStickUp:
            case StickDirection.RightStickUp:
                return stick.y > dead;
            case StickDirection.LeftStickDown:
            case StickDirection.RightStickDown:
                return stick.y < -dead;
        }
        return false;
    }

    public static bool IsJustStickDown(StickDirection direction, GamePad.Index index = GamePad.Index.One)
    {
        if(!IsConnectJoyPad)
        {
            string stickName = Stick[(int)direction];
            bool stickDown = false;
            if (stickName != "null")
            {
                KeyCode code = (KeyCode)Enum.Parse(typeof(KeyCode), Stick[(int)direction]);
                stickDown = Input.GetKeyDown(code);
            }

            return stickDown;
        }

        Vector2 stick, oldStick;
        float dead = 0.3f;
        int i = (int)index - 1;

        if (direction >= StickDirection.RightStickRight)
        {
            stick = currentState[i].rightStickAxis;
            oldStick = oldState[i].rightStickAxis;
        }
        else
        {
            stick = currentState[i].LeftStickAxis;
            oldStick = oldState[i].LeftStickAxis;
        }

        switch (direction)
        {
            case StickDirection.LeftStickRight:
            case StickDirection.RightStickRight:
                return stick.x > dead && oldStick.x <= dead;
            case StickDirection.LeftStickLeft:
            case StickDirection.RightStickLeft:
                return stick.x < -dead && oldStick.x >= -dead;
            case StickDirection.LeftStickUp:
            case StickDirection.RightStickUp:
                return stick.y > dead && oldStick.y <= dead;
            case StickDirection.LeftStickDown:
            case StickDirection.RightStickDown:
                return stick.y < -dead && oldStick.y >= -dead;
        }
        return false;
    }

    public static bool IsTriggerDown(Trigger trigger, GamePad.Index index = GamePad.Index.One)
    {
        float t = GamePad.GetTrigger((GamePad.Trigger)trigger, index);
        float dead = 0.05f;

        return t > dead;
    }

    public static bool IsJustTriggerDown(Trigger trigger, GamePad.Index index = GamePad.Index.One)
    {
        float t, oldT;
        float dead = 0.05f;

        if (trigger == Trigger.LeftTrigger)
        {
            t = currentState[(int)index].LeftTrigger;
            oldT = oldState[(int)index].LeftTrigger;
        }
        else
        {
            t = currentState[(int)index].RightTrigger;
            oldT = oldState[(int)index].RightTrigger;
        }

        return t > dead && oldT <= dead;
    }

}