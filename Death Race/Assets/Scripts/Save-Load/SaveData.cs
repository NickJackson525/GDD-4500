using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int Score1;
    public int Score2;
    public int Score3;
    public int Score4;
    public int Score5;
    private string Password;

    public SaveData(string password, int score1, int score2, int score3, int score4, int score5)
    {
        Score1 = score1;
        Score2 = score2;
        Score3 = score3;
        Score4 = score4;
        Score5 = score5;
        Password = password;
    }

    public string getPassword()
    {
        return Password;
    }
}