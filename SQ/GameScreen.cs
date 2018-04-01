using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SQ
{
    public class GameScreen
    {
        #region variables
        private ContentManager Content;
        #endregion

        #region BasicFunctions
        public virtual void LoadContent(ref ContentManager content)
        {
            Content = new ContentManager(ScreenManager.Instance().Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(ref GameTime gameTime, ref Camera cam)
        {

        }

        public virtual void Draw(ref SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
