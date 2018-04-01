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
        public bool WithinReach;
        public SpriteFont ItemFont;
        string tag;
        string type;

        public int posX;
        public int posY;
        public Vector2 textPos;
        public Vector2 textPos2;
        public Rectangle PlayerReach;
        JsonData jsonData;

        public bool presentItem;
        public short textBufferX = 20;
        public short textBufferY = 5;

        public Interactor()
        {
            itemID = File.ReadAllText("database/interactable.json");
            jsonData = JsonMapper.ToObject(itemID);
        }
        public void Draw (ref SpriteBatch spriteBatch)
        {
            if (tag != null)
            {
                if (presentItem == true)
                {
                    spriteBatch.DrawString(ItemFont, tag, textPos, Color.White);
                    //if (WithinReach == true)
                    {
                        spriteBatch.DrawString(ItemFont, type, textPos2, Color.White);
                    }
                }
            }
        }

        public void LoadContent (ref ContentManager content)
        { 
            ItemFont = content.Load<SpriteFont>("font");        
        }
 
        public void Update(ref GameTime gameTime, ref Camera cam, TexturePosition[] ItemPositions, int[] ItemNumberArray, ref Rectangle PlayerPos) 
        {

            
            MouseState newState = Mouse.GetState();
            posX = (int)cam.Position.X + newState.Position.X;
            posY = (int)cam.Position.Y + newState.Position.Y;
            Rectangle MousePos = new Rectangle(posX,posY, 1, 1);
            //Rectangle PlayerReach = new Rectangle((PlayerPos.X), (PlayerPos.Y), 96, 96);

            textPos = new Vector2(posX + textBufferX, posY - textBufferY);
            textPos2 = new Vector2(posX + textBufferX, posY - textBufferY - textBufferX);

            presentItem = false;
            WithinReach = false;
            
            for (int i = 0; i < ItemPositions.Length; i++)
            {
                if (ItemPositions[i] != null)
                {
                    if (Rectangle.Intersect(MousePos, ItemPositions[i].Position).IsEmpty == false)
                    {
                        presentItem = true;

                        int SpriteNumber = ItemNumberArray[i];

                        tag = jsonData[SpriteNumber]["tag"].ToString();
                        type = jsonData[SpriteNumber]["type"].ToString();
                        if (Rectangle.Intersect(ItemPositions[i].Position, PlayerReach).IsEmpty == false)
                        {
                            WithinReach = true;
                        }
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
