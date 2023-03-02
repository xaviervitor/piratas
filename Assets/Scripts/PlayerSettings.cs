public static class PlayerSettings {
    public static readonly float defaultMatchTime = 2 * 60f;
    public static readonly float defaultEnemySpawnTime = 4f;
    public static readonly int defaultGameMode = (int) MatchManager.GameMode.Infinite;

    public static readonly string MatchTime = "MatchTime";
    public static readonly string EnemySpawnTime = "EnemySpawnTime";
    public static readonly string GameMode = "GameMode";
}
