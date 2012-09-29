using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace ScrapWars3
{
    static class ExtendedKeyboard
    {
        private static KeyboardState previousKeyboardState;
        private static KeyboardState currentKeyboardState;

        public static void Update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        public static bool IsKeyDownAfterUp(Keys key)
        {
            return IsKeyDown(key) && WasKeyUp(key);
        }
        public static bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }
        public static bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }
        public static bool WasKeyUp(Keys key)
        {
            return previousKeyboardState.IsKeyUp(key);
        }
        public static bool WasKeyDown(Keys key)
        {
            return previousKeyboardState.IsKeyDown(key);
        }
        public static KeyboardState CurrentKeyboardState
        {
            get { return currentKeyboardState; }
        }
        public static KeyboardState PreviousKeyboardState
        {
            get { return previousKeyboardState; }
        }
    }
}
