using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework
{
    namespace GUI
    {
        public class MultiStateTextBox : TextBox
        {
            string[] content;
            int currentState;

            public MultiStateTextBox(string[] content, Vector2 size)
                : base(content[0], size)
            {
                currentState = 0;
                this.content = content;
            }

            public void changeState(int i)
            {
                if(i < content.Length)
                {
                    currentState = i;
                    changeText(content[i]);
                }
            }

            public void nextState()
            {
                if (currentState < content.Length - 1)
                {
                    currentState++;
                }
            }

            public void previousState()
            {
                if (currentState > 0)
                {
                    currentState--;
                }
            }

            public bool hasNext()
            {
                return currentState < content.Length - 1;
            }
        }
    }
}
