using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class Stage : GameObject2D
    {
        //四方
        Wall[] walls = new Wall[4];
        Vector2 windowSize;

        public Stage(string name, GameData gameData)
            :base(name, gameData)
        {
            for (int i = 0; i < walls.Length; i++)
            {
                Wall.Direction direction = (Wall.Direction)i;
                walls[i] = new Wall(direction + "Wall", gameData, direction);
            }

            windowSize = new Vector2(GameData.windowWidth, GameData.windowHeight);
        }

        //当たった場合は壁を返す
        public Wall Intersects(SphereCollider2D collider)
        {
            if (collider.center.Y - collider.radius > windowSize.Y)
            {
                //下に当たった
                return walls[(int)Wall.Direction.Down];
                //return walls[1];
            }

            if (collider.center.Y - collider.radius < 0.0f)
            {
                //上に当たった
                return walls[(int)Wall.Direction.Up];
                //return walls[0];
            }

            if (collider.center.X + collider.radius > windowSize.X)
            {
                //左に当たった
                return walls[(int)Wall.Direction.Left];
                //return walls[2];
            }

            if (collider.center.X - collider.radius < 0.0f)
            {
                //右に当たった
                return walls[(int)Wall.Direction.Right];
                //return walls[3];
            }
            return null;
        }
    }
}
