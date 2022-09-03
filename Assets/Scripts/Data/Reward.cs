using System;

[Serializable]
public class Reward
{
    public string title;
    public string message;
    public string avatarUrl;
    public int coins;
}

[Serializable]
public class Rewards
{
    public Reward[] rewards;
}
