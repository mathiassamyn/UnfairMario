using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class MovingEnemy : Enemy
    {
        public int Velocity { get; set; }
        public CollisionDetection CollDet { get; set; }

        public virtual void UpdatePosition(GameLevel level)
        {

        }
    }
}
