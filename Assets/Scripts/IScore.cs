using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public interface IScore
{
    int GetScore();
    void AddScore(int score);
    void Reset();
}
