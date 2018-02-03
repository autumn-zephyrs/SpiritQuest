using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{
    class MainGameScreen : GameScreen
    {
        MapGeneration map;
        Player player;
        public override void LoadContent(ContentManager content)
        {
            map = new MapGeneration();
            player = new Player(content.Load<Texture2D>("Character"), new Rectangle(0,0,32,42),new Rectangle(0,0,16,21),4,100,0,2,0,0);
            map.CreateMap(content);       
        }


        // For Mouse Control
        private MouseState oldState;
      
        public override void Update(GameTime gameTime, Camera cam)
        {
            if (player.getMovingBool() == false)
            {
                // this tells the player whether there is an object in any direction
                player.setProperGridPosition(map.NumberOfFloorObjects);
                int gridPosition = player.getProperGridPosition();
                player.setMovementBools(map.CanPlayerMoveToTile(gridPosition - map.NumberOfFloorObjects), map.CanPlayerMoveToTile(gridPosition - 1), map.CanPlayerMoveToTile(gridPosition + 1), map.CanPlayerMoveToTile(gridPosition + map.NumberOfFloorObjects));
            }
            player.Update(gameTime);
           
            // sets the camera position to center the player
            cam.Position = new Vector2(player.SpritePOS.X - (ScreenManager.Instance().ScreenDimensions.X / 2), player.SpritePOS.Y - (ScreenManager.Instance().ScreenDimensions.Y / 2));

            base.Update(gameTime, cam);

            //Brendan's Mouse Control

            MouseState newState = Mouse.GetState();
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    // Will add functionality here later.
                }
            oldState = newState;
            int mousePosX = oldState.X + (int)cam.Position.X;
            int mousePosY = oldState.Y + (int)cam.Position.Y;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            map.Draw(spriteBatch);
            map.DrawBeforePlayer(player.getProperGridPosition(), spriteBatch);
            player.Draw(spriteBatch);
            map.DrawAfterPlayer(player.getProperGridPosition(), spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
