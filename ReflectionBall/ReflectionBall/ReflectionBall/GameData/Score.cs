using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectionBall
{
    public class Score
    {
        bool isCalc = false;

        /// <summary>
        /// ボールを弾き返した回数
        /// </summary>
        public int reflectCount { get; private set; }

        /// <summary>
        /// ボールを弾き返したのスピードに応じてスコアが上がる
        /// </summary>
        public float speedBounus { get; private set; }

        /// <summary>
        /// ボールが一番早い時の速度(ゲーム中ボールは生きている限り加速していく）
        /// </summary>
        public float maxSpeed { get; private set; }

        int totalScore = 0;
        public int TotalScore
        {
            get
            {
                if (!isCalc) return totalScore;
                isCalc = false;
                //todo:点数配分は要調整
                totalScore += reflectCount * 1000;
                totalScore += (int)speedBounus * 100;
                totalScore += (int)maxSpeed * 1000;

                return totalScore;
            }
        }
        
        public void Reflected(Ball ball)
        {
            isCalc = true;
            reflectCount++;
            UpdateMaxSpeed(ball.speed);
        }

        void UpdateMaxSpeed(float speed)
        {
            if (maxSpeed < speed) maxSpeed = speed;
            speedBounus += speed;
        }

        public void Unload()
        {
            isCalc = true;
            reflectCount = 0;
            speedBounus = 0.0f;
            maxSpeed = 0.0f;
            totalScore = 0;
        }
    }
}
