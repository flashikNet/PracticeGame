using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Managers
{
    public class ProjectileManager
    {
        public List<Projectile> projectiles { get; private set; }
        public ProjectileManager()
        {
            projectiles = new List<Projectile>();
        }

        public void Add(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            foreach (var projectile in projectiles)
            {
                projectile.Update(gameTime);
                foreach (var enemy in enemies)
                {
                    if (enemy.HealthPoint <= 0) continue;
                    if ((projectile.Position - enemy.Position).Length() < 120)
                    {
                        enemy.TakeDamage(projectile.Damage);
                        projectile.Destroy();
                        break;
                    }
                }
            }
            projectiles.RemoveAll(p => p.IsDone);
        }
    }
}
