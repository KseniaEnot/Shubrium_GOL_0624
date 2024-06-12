using Assets._Scripts.Minigames.EatKASHA;
using System;
using UnityEngine;

internal class MiniGamesManager : MonoBehaviour
{
    public static MiniGamesManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void PlayGame(MiniGame game)
    {
        switch (game) {
            case MiniGame.Ksilophone:
                throw new NotImplementedException();
            case MiniGame.ThrowABall:

                throw new NotImplementedException();

        }
        MiniGamesManager.play();
}

    private static void play()
    {

    }
}
public enum MiniGame
{
    Ksilophone,
    ThrowABall,
    EatKASHA


}
