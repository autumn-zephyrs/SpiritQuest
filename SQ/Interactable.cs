using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LitJson;
namespace SQ
{

    class Interactable
    {

        private MouseState oldState;

        public Interactable()
        {

        }
        public void Update(GameTime gameTime, Camera cam, TexturePosition[] ItemPositions, int[] ItemNumberArray)
        {

            MouseState newState = Mouse.GetState();
            Rectangle MousePos = new Rectangle((int)cam.Position.X + newState.Position.X, (int)cam.Position.Y + newState.Position.Y, 1, 1);
            

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                for (int i = 0; i < ItemPositions.Length; i++)
                {
                    if (ItemPositions[i] != null)
                    {
                        if (Rectangle.Intersect(MousePos, ItemPositions[i].Position).IsEmpty == false)
                        {
                            int SpriteNumber = ItemNumberArray[i];
                            // do json shit

                            // if item, if door, if book
                        }
                    }

                }
            }
            oldState = newState;
        }
    }
}
