namespace K_UnityGF
{
    /// <summary>
    /// 游戏模式
    /// </summary>
    public enum GameMode
    {
        /// <summary>
        /// 标准模式
        /// </summary>
        Normal,

        /// <summary>
        /// 演示模式
        /// </summary>
        Demonstrate,

        /// <summary>
        /// 练习模式
        /// </summary>
        Practice,

        /// <summary>
        /// 考核模式
        /// </summary>
        Check,
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// 游戏未开始
        /// </summary>
        NotStarted,

        /// <summary>
        /// 游戏开始
        /// </summary>
        GameStart,

        /// <summary>
        /// 游戏结束
        /// </summary>
        GameOver
    }

    /// <summary>
    /// 动画状态
    /// </summary>
    public enum AnimState
    {
        /// <summary>
        /// 播放
        /// </summary>
        Play,

        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
    }

    /// <summary>
    /// 渲染模式
    /// </summary>
    public enum RenderingMode
    {
        /// <summary>
        /// 
        /// </summary>
        Opaque,

        /// <summary>
        /// 
        /// </summary>
        Cutout,

        /// <summary>
        /// 
        /// </summary>
        Fade,

        /// <summary>
        /// 
        /// </summary>
        Transparent,
    }
}

