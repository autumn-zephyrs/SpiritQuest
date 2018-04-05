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
        InventoryManager inventoryManger;
        KeyboardState PKS;
        public override void LoadContent(ref ContentManager content)
        {
            inventoryManger = new InventoryManager();
            PKS = Keyboard.GetState();
            menu = new MenuManager();
            menu.LoadContent(content);
            map = new MapGeneration();
            player = new Player(content.Load<Texture2D>("Character"), new Rectangle(0,0,32,64),new Rectangle(0,0,32,64),4,100,0,4,0,0);
            map.CreateMap(ref content);
            player.setProperGridPosition(map.NumberOfFloorObjects);
            interactor = new Interactor();
            interactor.LoadContent(ref content);
            inventoryManger.LoadContent(ref content, ref player);
        }
      
        public override void Update(ref GameTime gameTime, ref Camera cam)
        {



      
                if (player.getMovingBool() == false)
                {
                    
                    player.setProperGridPosition(map.NumberOfFloorObjects);
                    int gridPosition = player.getProperGridPosition();
                    player.setMovementBools(map.CanPlayerMoveToTileUpOrDown(gridPosition - map.NumberOfFloorObjects), map.CanPlayerMoveRightTile(gridPosition - 1, player.PositionOnGrid), map.CanPlayerMoveLeftTile(gridPosition + 1, player.PositionOnGrid), map.CanPlayerMoveToTileUpOrDown(gridPosition + map.NumberOfFloorObjects));
                }
                player.Update(ref gameTime);
          
           
            cam.Position = new Vector2(player.SpritePOS.X - (ScreenManager.Instance().ScreenDimensions.X / 2), player.SpritePOS.Y - (ScreenManager.Instance().ScreenDimensions.Y / 2));

       
            inventoryManger.Update(ref gameTime, ref cam, ref PKS);
            base.Update(ref gameTime, ref cam);
            interactor.Update(ref gameTime, ref cam, map.getItemTextures(), map.getItemValues(), ref player.SpritePOS);
            PKS = Keyboard.GetState();
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {

            map.Draw(ref spriteBatch);
            map.DrawBeforePlayer(player.getProperGridPosition(), ref spriteBatch);
            player.Draw(ref spriteBatch);
            map.DrawAfterPlayer(player.getProperGridPosition(),ref spriteBatch);
            menu.Draw(spriteBatch);
            inventoryManger.Draw(ref spriteBatch);
            base.Draw(ref spriteBatch);
            interactor.Draw(ref spriteBatch);
        }
    }
}
