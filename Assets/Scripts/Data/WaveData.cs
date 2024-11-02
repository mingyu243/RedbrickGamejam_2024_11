using System;

[Serializable]
public class WaveData
{
    public int Id;
    public int Monster1Weight;
    public int Monster2Weight;
    public int Monster3Weight;
    public int SpawnCount;

    public int SumWeight => (Monster1Weight + Monster2Weight + Monster3Weight);
}