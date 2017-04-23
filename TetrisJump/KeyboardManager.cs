using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace TetrisJump
{
    public static class KeyboardManager
    {
        private static KeyboardState state { get; set; }

        public static void Update()
        {
            cooldownKeys = pressedKeys;
            state = Keyboard.GetState();
            pressedKeys = state.GetPressedKeys().ToList();
        }

        private static List<Keys> pressedKeys = new List<Keys>();
        private static List<Keys> cooldownKeys = new List<Keys>();

        public static bool IsKeyPressed(string key, bool allowLoop = false)
        {
            var keyenum = (Keys)Enum.Parse(typeof(Keys), key, true);
            return pressedKeys.Contains(keyenum) && (!cooldownKeys.Contains(keyenum) || allowLoop);
        }

        public static string PressedKey
        {
            get
            {
                if (pressedKeys.Count > 0 && !cooldownKeys.Contains(pressedKeys.First()))
                {
                    if (pressedKeys.Contains(Keys.LeftShift) || pressedKeys.Contains(Keys.RightShift))
                        return pressedKeys.First().ToString().ToUpper();
                    else
                        return pressedKeys.First().ToString().ToLower();
                }
                else
                    return "";
            }
        }

        public static void Reset()
        {
            pressedKeys = new List<Keys>();
            cooldownKeys = new List<Keys>();
            state = new KeyboardState();
        }
    }
}
