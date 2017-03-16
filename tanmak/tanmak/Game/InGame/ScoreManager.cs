using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tanmak.Game
{
    public class ScoreManager
    {
        public static int NormalMissileDamage { get; set; } = 1;

        public static int NormalBulletDamage { get; set; } = 1;
        public static int GuidedBulletDamage { get; set; } = 5;

        public int MaxHP { get; set; } = 250;
        public int HP { get; set; } = 250;
        public int Combo { get; set; } = 0;
        public int MaxCombo { get; set; } = 0;
        public int Score { get; set; } = 0;

        public bool IsDied { get; set; } = false;

        public event EventHandler<ScoreManager> Dieded;
        public event EventHandler<ScoreManager> Comboed;

        public void HeroHitted(int Damage)
        {
            Combo = 0;

            HP -= Damage;

            if(HP <= 0)
            {
                HP = 0;

                IsDied = true;

                Dieded?.Invoke(this, this);
            }
        }

        public void EnemyHiited(int Score)
        {
            this.Score += Score + Combo;

            Combo++;
            MaxCombo = Math.Max(MaxCombo, Combo);

            Comboed?.Invoke(this, this);
        }
    }
}
