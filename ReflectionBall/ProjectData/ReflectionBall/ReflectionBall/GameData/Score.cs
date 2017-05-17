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
        /// 全体にかかるスコアの倍率
        /// </summary>
        const float totalDiameter = 100.0f;

        /// <summary>
        /// 玉をはじき返した回数にかかるスコアの倍率
        /// </summary>
        const float reflectPointDiameter = 5.0f;

        /// <summary>
        /// スピードボーナスにかかるスコアの倍率
        /// </summary>
        const float speedBounusDiameter = 3.0f;

        /// <summary>
        /// 最大速度にかかるスコアの倍率
        /// </summary>
        const float maxSpeedBounusDiameter = 20.0f;


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

                float temp = 0.0f;
                temp += reflectCount * reflectPointDiameter;
                temp += speedBounus * speedBounusDiameter;
                temp += maxSpeed * maxSpeedBounusDiameter;
                totalScore =(int)(temp * totalDiameter);

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
