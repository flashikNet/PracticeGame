using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class InputManager 
    {
        KeyboardState currentState;
        KeyboardState oldState;

        public void Update()
        {
            oldState = currentState;
            currentState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public bool IsKeyPressed(Keys key)
        {
            return oldState.IsKeyDown(key) && currentState.IsKeyDown(key);
        }

        public bool IsKeyClicked(Keys key)
        {
            return oldState.IsKeyUp(key) && currentState.IsKeyDown(key);
        }
    }
}
