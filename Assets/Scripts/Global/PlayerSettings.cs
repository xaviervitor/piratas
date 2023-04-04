public static class PlayerSettings {
    public static class GameModes { 
        public static readonly int Infinite = 0;
        public static readonly int TimeLimit = 1; 
    }

    public static class PlayerSkins {
        public static readonly int Yellow = 0;
        public static readonly int Green = 1;
        public static readonly int Blue = 2;
    }

    public static readonly float defaultMatchTime = 2 * 60f;
    public static readonly float defaultEnemySpawnTime = 3f;
    public static readonly int defaultGameMode = GameModes.Infinite;
    public static readonly int defaultPlayerSkin = PlayerSkins.Yellow;
    
    public static readonly string MatchTime = "MatchTime";
    public static readonly string EnemySpawnTime = "EnemySpawnTime";
    public static readonly string GameMode = "GameMode";
    public static readonly string PlayerSkin = "PlayerSkin";
}
