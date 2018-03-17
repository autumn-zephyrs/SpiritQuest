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
        public MouseState masterState;
        public SpriteFont ItemFont;
        string tag;
        string type;

        public int posX;
        public int posY;
        public Vector2 textPos;
        public Vector2 textPos2;
        public Vector2 PlayerPos;

        public bool presentItem;
        public short textBufferX = 20;
        public short textBufferY = 5;

        Player player;

        public Interactor()
        {
        }
        public void Draw (SpriteBatch spriteBatch)
        {
            MouseState masterState = Mouse.GetState();
            if (tag != null)
            {
                if (presentItem == true)
                {
                    spriteBatch.DrawString(ItemFont, tag, textPos, Color.White);
                    //if (((PlayerPos.X - masterState.X > -86) && (PlayerPos.X - masterState.X < 86)) && ((PlayerPos.Y - masterState.Y > -86) && (PlayerPos.Y - masterState.Y < 86)))
                    {
                        spriteBatch.DrawString(ItemFont, type, textPos2, Color.White);
                    }
                }
            }
        }

        public void LoadContent (ContentManager content)
        { 
            ItemFont = content.Load<SpriteFont>("font");        
        }
 
        public void Update(GameTime gameTime, Camera cam, TexturePosition[] ItemPositions, int[] ItemNumberArray, Rectangle PlayerPos) 
        {

            
            MouseState newState = Mouse.GetState();
            posX = (int)cam.Position.X + newState.Position.X;
            posY = (int)cam.Position.Y + newState.Position.Y;
            Rectangle MousePos = new Rectangle(posX,posY, 1, 1);
            textPos = new Vector2(posX + textBufferX, posY - textBufferY);
            textPos2 = new Vector2(posX + textBufferX, posY - textBufferY - textBufferX);

            presentItem = false;
            
            for (int i = 0; i < ItemPositions.Length; i++)
            {
                if (ItemPositions[i] != null)
                {
                    if (Rectangle.Intersect(MousePos, ItemPositions[i].Position).IsEmpty == false)
                    {
                        presentItem = true;

                        int SpriteNumber = ItemNumberArray[i];
                        itemID = File.ReadAllText("database/interactable.json");
                        JsonData jsonData = JsonMapper.ToObject(itemID);
                        tag = jsonData[SpriteNumber]["tag"].ToString();
                        type = jsonData[SpriteNumber]["type"].ToString();
                        if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                            {
                            //Add item to inventory, and remove item from world.
                            }
                    }                 
                }
            }

            
            oldState = newState;
        }
    }
}
