using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFramework.GUI;
using MyMath;

namespace MyFramework
{
	public abstract class Scene : Container
    {
        public event EventHandler closeScene;

        public event SceneEventHandler addScene;

		public Scene(Vector2 size) : base(size)
        {
            initialize();
        }

        abstract protected void initialize();

        /**
         * tells listeners to detatch from this window and close it
         */
        protected void onCloseScene(EventArgs e)
        {
            if (closeScene != null)
            {
                closeScene(this, e);
            }
        }

        /**
         * tells listeners to open a new scene
         */
        protected void onAddScene(Scene scene)
        {
            if (addScene != null)
            {
                addScene(this, scene);
            }
        }

        //abstract public void inputHandler(String key);
    }
}
