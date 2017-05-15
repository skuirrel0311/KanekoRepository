using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ReflectionBall
{
    public enum MouseButton { RightButton, LeftButton, MiddleButton }

    public class MyInputManager : InputManager
    {
        static MouseState oldMouseState;
        static MouseState mouseState;
        static Vector2 mousePosition;
        static float wheelValue;

        public static void MouseInitialize()
        {
            Update();
            MouseUpdate();
        }

        public static void MouseUpdate()
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;
            wheelValue = mouseState.ScrollWheelValue;
            Update();
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Pressed;
                case MouseButton.MiddleButton:
                    return mouseState.MiddleButton == ButtonState.Pressed;
            }
            return false;
        }

        public static bool IsJustMouseButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Pressed &&
                        oldMouseState.LeftButton == ButtonState.Released;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Pressed &&
                        oldMouseState.RightButton == ButtonState.Released;
                case MouseButton.MiddleButton:
                    return mouseState.MiddleButton == ButtonState.Pressed &&
                        oldMouseState.MiddleButton == ButtonState.Released;
            }
            return false;
        }

        public static float WheelValue { get { return wheelValue; } }

        public static Vector2 MousePosition { get { return mousePosition; } }
    }
}
