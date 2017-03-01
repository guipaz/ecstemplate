using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace alchemist_mono
{
    public static class Input
    {
        #region Fields
        // keyboard control
        static KeyboardState keyboardLast;
        static KeyboardState keyboardCurrent;

        // mouse control
        static MouseState mouseLast;
        static MouseState mouseCurrent;

        // mouse dragging
        static Point initialPosition;
        static Point currentPosition;
        static bool mouseBeingPressed;
        static float dragTolerance = 5.0f;
        
        public static int CurrentScroll
        {
            get
            {
                if (mouseCurrent.ScrollWheelValue > mouseLast.ScrollWheelValue)
                    return 1;
                else if (mouseCurrent.ScrollWheelValue < mouseLast.ScrollWheelValue)
                    return -1;
                return 0;
            }
        }

        public static Point MousePosition
        {
            get
            {
                return mouseCurrent.Position;
            }
        }

        public static Point Drag
        {
            get
            {
                return currentPosition - initialPosition;
            }
        }
        
        #endregion

        #region Mouse control
        public static bool WasMousePressed(int button)
        {
            return LastStateForButton(button) == ButtonState.Released && CurrentStateForButton(button) == ButtonState.Pressed;
        }

        public static bool WasMouseReleased(int button)
        {
            return LastStateForButton(button) == ButtonState.Pressed && CurrentStateForButton(button) == ButtonState.Released;
        }

        public static bool IsMouseDown(int button)
        {
            return CurrentStateForButton(button) == ButtonState.Pressed;
        }

        static ButtonState LastStateForButton(int button)
        {
            switch (button)
            {
                case 1:
                    return mouseLast.RightButton;
                case 2:
                    return mouseLast.MiddleButton;
                default:
                    return mouseLast.LeftButton;
            }
        }

        static ButtonState CurrentStateForButton(int button)
        {
            switch (button)
            {
                case 1:
                    return mouseCurrent.RightButton;
                case 2:
                    return mouseCurrent.MiddleButton;
                default:
                    return mouseCurrent.LeftButton;
            }
        }

        public static bool IsMouseDragging()
        {
            float dist = Vector2.Distance(currentPosition.ToVector2(), initialPosition.ToVector2());
            return mouseBeingPressed && dist >= dragTolerance;
        }
        #endregion

        #region Keyboard control
        /// <summary>
        /// Returns if the key was just pressed
        /// </summary>
        public static bool WasKeyPressed(Keys key)
        {
            return !keyboardLast.IsKeyDown(key) && keyboardCurrent.IsKeyDown(key);
        }

        /// <summary>
        /// Returns if the key was just released
        /// </summary>
        public static bool WasKeyReleased(Keys key)
        {
            return !keyboardLast.IsKeyUp(key) && keyboardCurrent.IsKeyUp(key);
        }

        public static bool WasAnyKeyPressed()
        {
            return keyboardLast.GetPressedKeys().Length == 0 && keyboardCurrent.GetPressedKeys().Length > 0;
        }

        /// <summary>
        /// Returns if the key is currently down
        /// </summary>
        public static bool IsKeyDown(Keys key)
        {
            return keyboardCurrent.IsKeyDown(key);
        }
        #endregion

        #region Life cycle
        public static void Update(GameTime time)
        {
            keyboardLast = keyboardCurrent;
            keyboardCurrent = Keyboard.GetState();

            mouseLast = mouseCurrent;
            mouseCurrent = Mouse.GetState();
            
            // if just pressed the left button, tracks
            // the position to know if is dragging
            if (WasMousePressed(0))
            {
                mouseBeingPressed = true;
                initialPosition = mouseCurrent.Position;
                currentPosition = initialPosition;
            }
            else if (mouseBeingPressed)
            {
                if (!IsMouseDown(0))
                {
                    mouseBeingPressed = false;
                }
                else
                {
                    currentPosition = mouseCurrent.Position;
                }
            }
        }
        #endregion
    }
}
