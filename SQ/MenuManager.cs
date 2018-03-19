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
        public SpriteFont ItemFont;

        Rectangle menu1 = new Rectangle(32, 32, 640, 600);
        Rectangle menu2 = new Rectangle(0, 0, 640, 600);

        new Vector2 DrawTarget;

        public string HealthName = "Health";
        public string StaminaName = "Stamina";
        public string MagicName = "Magic";

        public string STRName = "Strength";
        public string STRValue;
        public string DEXName = "Dexterity";
        public string DEXValue;
        public string WILName = "Willpower";
        public string WILValue;
        public string INTName = "Intelligence";
        public string INTValue;
        public string CHAName = "Charisma";
        public string CHAValue;
        public string VITName = "Vitality";
        public string VITValue;
        public string LCKName = "Luck";
        public string LuckValue;


        public MenuManager() {}


        public void LoadContent(ContentManager content)
            {
                menu = new Menu(content.Load<Texture2D>("menu"), menu1 , menu2);
                ItemFont = content.Load<SpriteFont>("font");
            }

        public void Draw(SpriteBatch spriteBatch)
            {
                if (isMenuOpen)
                {
                    menu.Draw(spriteBatch);
                    spriteBatch.DrawString(ItemFont, STRName, DrawTarget, Color.White);
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
