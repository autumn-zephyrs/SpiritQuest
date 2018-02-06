using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{
    class MenuManager
    {
        Menu menu;
        public bool menuLock = false;
        public bool isMenuOpen = false;
        public MenuManager() {}
        public void LoadContent(ContentManager content)
        {
            menu = new Menu(content.Load<Texture2D>("menu"), new Rectangle(0,0,640,600),new Rectangle(0,0,640,600));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isMenuOpen)
            {
                menu.Draw(spriteBatch);
            }

        }
        public void Update(GameTime gameTime, Camera cam)
            {
            KeyboardState MenuKey = Keyboard.GetState();

            if (MenuKey.IsKeyUp(Keys.Tab) && menuLock == true)
            {
                menuLock = false;
            }

            if (MenuKey.IsKeyDown(Keys.Tab) && menuLock == false)
            {
                isMenuOpen = !isMenuOpen;
                menuLock = true;
            }
            if (isMenuOpen)
            {
                menu.Update(gameTime, cam);
            }

        }
    }

    class Menu
    {
        public Menu(Texture2D texture, Rectangle position, Rectangle source)
        {
            this.texture = texture;
            this.position = position;
            this.source = source;
        }
        Rectangle absolutePosition;
        Texture2D texture;
        Rectangle position;
        Rectangle source;
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, absolutePosition, source, Color.White);
        }
        public void Update(GameTime gameTime, Camera cam)
        {
            absolutePosition = new Rectangle((int)cam.Position.X + position.X, (int)cam.Position.Y + position.Y, position.Width, position.Height);
        }
        
    }
}
