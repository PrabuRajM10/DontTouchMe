namespace Enums
{
    public class Enum
    {
        
        public enum SoundType
        {
            Fire,
            EnemyHit,
            EnemyDeath,
            PlayerDeath,
            Coin,
            Spell,
            Ability,
            MainBg,
            GameBg,
            ButtonClick
        }
        public enum GameScreen
        {
            Gameplay,
            Setting,
            Home,
            GameResult,
            CardPicker,
            Pause
        }
    
        public enum CollectablesType
        {
            Coins,
            Heal,
            Spell
        }
        
        public enum InGameParticleType
        {
            BulletHit,
            EnemyHit
        }
        public enum PopUpType
        {
            YES_NO,
            ONLY_YES,
            ONLY_NO,
            CONFIRM,
            CONTINUE
        }
        public enum PoolObjectTypes
        {
            Enemy,
            Bullet,
            Coin,
            Spell,
            Bomb,
            Audio,
            BulletHitParticle,
            EnemyHitParticle
        }
        public enum PowerCardsId
        {
            PC1 = 1,
            PC2,
            PC3,
            PC4,
            PC5,
            PC6,
            PC7,
            PC8,
            PC9,
            PC10
        }

        public enum CardRarity
        {
            Common,
            Rare,
            Epic,
            Legendary
        }
    }
}