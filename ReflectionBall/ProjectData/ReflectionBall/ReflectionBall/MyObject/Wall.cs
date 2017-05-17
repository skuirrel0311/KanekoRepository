using Microsoft.Xna.Framework;

namespace ReflectionBall
{
    public class Wall : GameObject2D
    {
        public enum Direction { Up, Down, Left, Right }

        public Direction direction { get; private set; }
        //壁の法線ベクトル
        public Vector2 normal { get; private set; }

        public Wall(string name, GameData gameData, Direction direction)
            : base(name, gameData)
        {
            this.direction = direction;
            SetTransform(direction);
        }

        void SetTransform(Direction direction)
        {
            Vector2 temp = Vector2.Zero;
            Vector2 screenSize = new Vector2(GameData.windowWidth, GameData.windowHeight);

            //screenSizeに掛ける値を入れていく
            switch (direction)
            {
                case Direction.Up:
                    temp = new Vector2(0.5f, 0.0f);
                    break;
                case Direction.Left:
                    temp = new Vector2(1.0f, 0.5f);
                    break;
                case Direction.Right:
                    temp = new Vector2(0.0f, 0.5f);
                    break;
            }

            transform.position = temp * screenSize;
            //画面の中央に向かうベクトルが法線ベクトル
            normal = Vector2.Normalize((screenSize * 0.5f) - transform.position);
        }
    }
}
