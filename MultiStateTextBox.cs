using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMath;

namespace MyFramework.GUI.GUIElements
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

        public MultiStateTextBox(Vector2 size)
            : base("", size)
        {
            currentState = 0;
            content = new string[0];
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
                changeText(content[currentState]);
            }
        }

        public void previousState()
        {
            if (currentState > 0)
            {
                currentState--;
                changeText(content[currentState]);
            }
        }

        public bool hasNext()
        {
            return currentState < content.Length - 1;
        }

        public void queueText(string[] c)
        {
            String[] newContent = new string[content.Length + c.Length];

            for (int i = 0; i < content.Length; i++)
            {
                newContent[i] = content[i];
            }
            for (int i = content.Length; i < newContent.Length; i++)
            {
                newContent[i] = c[i - content.Length];
            }

            content = newContent;
            changeText(content[currentState]);
        }
    }
}
