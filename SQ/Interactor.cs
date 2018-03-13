using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using LitJson;
namespace SQ
{

    class Interactor
    {

        private string itemID;
        private MouseState oldState;

        public Interactor()
        {

        }
        public void Update(GameTime gameTime, Camera cam, TexturePosition[] ItemPositions, int[] ItemNumberArray)
        {

            MouseState newState = Mouse.GetState();
            Rectangle MousePos = new Rectangle((int)cam.Position.X + newState.Position.X, (int)cam.Position.Y + newState.Position.Y, 1, 1);
            
            for (int i = 0; i < ItemPositions.Length; i++)
            {
                if (ItemPositions[i] != null)
                {
                    if (Rectangle.Intersect(MousePos, ItemPositions[i].Position).IsEmpty == false)
                    {
                        int SpriteNumber = ItemNumberArray[i];
                        itemID = File.ReadAllText("database/interactable");
                        // if item, if door, if book
                    }
                }

            }

            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
            }
                oldState = newState;
        }
    }
}
