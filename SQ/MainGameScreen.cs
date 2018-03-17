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
        MenuManager menu;
        MapGeneration map;
        Player player;
        Interactor interactor;

        public override void LoadContent(ContentManager content)
        {
            menu = new MenuManager();
            menu.LoadContent(content);
            map = new MapGeneration();
            player = new Player(content.Load<Texture2D>("Character"), new Rectangle(0,0,32,42),new Rectangle(0,0,16,21),4,100,0,2,0,0);
            map.CreateMap(content);
            player.setProperGridPosition(map.NumberOfFloorObjects);
            interactor = new Interactor();
            interactor.LoadContent(content);
        }
      
        public override void Update(GameTime gameTime, Camera cam)
        {

            if (menu.isMenuOpen == false)
            {

                if (player.getMovingBool() == false)
                {
                    
                    player.setProperGridPosition(map.NumberOfFloorObjects);
                    int gridPosition = player.getProperGridPosition();
                    player.setMovementBools(map.CanPlayerMoveToTileUpOrDown(gridPosition - map.NumberOfFloorObjects), map.CanPlayerMoveRightTile(gridPosition - 1, player.PositionOnGrid), map.CanPlayerMoveLeftTile(gridPosition + 1, player.PositionOnGrid), map.CanPlayerMoveToTileUpOrDown(gridPosition + map.NumberOfFloorObjects));
                }
                player.Update(gameTime);
            }
           
            cam.Position = new Vector2(player.SpritePOS.X - (ScreenManager.Instance().ScreenDimensions.X / 2), player.SpritePOS.Y - (ScreenManager.Instance().ScreenDimensions.Y / 2));

            menu.Update(gameTime, cam);
            base.Update(gameTime, cam);
            interactor.Update(gameTime, cam, map.getItemTextures(), map.getItemValues());
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            map.Draw(spriteBatch);
            map.DrawBeforePlayer(player.getProperGridPosition(), spriteBatch);
            player.Draw(spriteBatch);
            map.DrawAfterPlayer(player.getProperGridPosition(), spriteBatch);
            menu.Draw(spriteBatch);
            base.Draw(spriteBatch);
            interactor.Draw(spriteBatch);
        }
    }
}
