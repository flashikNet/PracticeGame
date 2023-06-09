using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class EnemyManager
    {
        Texture2D texture;
        Stopwatch timer;
        public List<Enemy> enemyList { get; private set; }
        int count;
        float coolDown;
        float dictance;
        public int LeftCount { get => (count >= 0 ? count : 0) + enemyList.Count; }
        public EnemyManager(Texture2D texture, int count = 50, float coolDown = 5f)
        {
            this.texture = texture;
            enemyList = new List<Enemy>();
            timer = new Stopwatch();
            dictance = (float)Math.Sqrt(Math.Pow(MainGame.Width/2, 2) + Math.Pow(MainGame.Height/2, 2));
            this.count = count;
            this.coolDown = coolDown;
            timer.Start();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, Player player)
        {
            Spawn();
            Clear(gameTime);
            foreach (var enemy in enemyList)
            {
                enemy.Update(gameTime);
                if((enemy.Position - player.Position).Length() < 150)
                {
                    player.TakeDamage(20);
                }
            }
            if(LeftCount == 0)
            {
                MainGame.State = State.End;
            }
        }

        private void Clear(GameTime gameTime)
        {
            for (var i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].HealthPoint <= 0)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Spawn()
        {
            if (enemyList.Count > 15) return;
            if (timer.ElapsedMilliseconds > coolDown && count > 0)
            {
                enemyList.Add(new Enemy(texture, GetPosition(), 1, 17));
                count--;
                timer.Restart();
            }
            if (count == 0)
            {
                timer.Stop();
                count--;
            }
        }

        public Vector2 GetPosition()
        {
            var origin = MainGame.Player.Position;
            var angle = MathHelper.ToRadians(MainGame.Random.Next(360));
            var dx = dictance * (float)Math.Cos(angle);
            var dy = dictance * (float)Math.Sin(angle);
            return new Vector2(origin.X + dx, origin.Y + dy);

        }
    }
}
