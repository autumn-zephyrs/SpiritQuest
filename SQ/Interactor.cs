using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using LitJson;

namespace SQ
{

    class Interactor
    {

        private string itemID;
        private MouseState oldState;
        public SpriteFont ItemFont;
        string tag;
        public Vector2 textPos;

        public bool presentItem;

        public Interactor()
        {
        }
        public void Draw (SpriteBatch spriteBatch)
        {
            if (tag != null)
            {
                spriteBatch.DrawString(ItemFont, tag, textPos, Color.White);
            }
        }

        public void LoadContent (ContentManager content)
        {
            
            ItemFont = content.Load<SpriteFont>("font");
        }

        public void Update(GameTime gameTime, Camera cam, TexturePosition[] ItemPositions, int[] ItemNumberArray)
        {

            MouseState newState = Mouse.GetState();
            Rectangle MousePos = new Rectangle((int)cam.Position.X + newState.Position.X, (int)cam.Position.Y + newState.Position.Y, 1, 1);
            textPos = new Vector2((int)cam.Position.X + oldState.Position.X, (int)cam.Position.Y + oldState.Position.Y);

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < ItemPositions.Length; i++)
                {
                    if (ItemPositions[i] != null)
                    {
                        if (Rectangle.Intersect(MousePos, ItemPositions[i].Position).IsEmpty == false)
                        {
                            int SpriteNumber = ItemNumberArray[i];
                            itemID = File.ReadAllText("database/interactable.json");
                            JsonData jsonData = JsonMapper.ToObject(itemID);
                            tag = jsonData[SpriteNumber]["tag"].ToString();

                            // if item, if door, if book
                        }
                    }

                }
            }

         
                oldState = newState;
        }
    }
}
