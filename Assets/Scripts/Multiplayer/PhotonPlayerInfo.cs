using System;

[Serializable]
public class PhotonPlayerInfo
{
    public string PlayerName;
    public int PlayerKills;
    public int PlayerDeaths;
    public int PlayerScore;
    public int PlayerID;

    public PhotonPlayerInfo()
    {
        PlayerName = "";
        PlayerKills = 0;
        PlayerDeaths = 0;
        PlayerScore = 0;
        PlayerID = 0;
    }

    public PhotonPlayerInfo(string playerName, int playerKills, int playerDeaths, int playerScore, int playerID)
    {
        PlayerName = playerName;
        PlayerKills = playerKills;
        PlayerDeaths = playerDeaths;
        PlayerScore = playerScore;
        PlayerID = playerID;
    }
}
