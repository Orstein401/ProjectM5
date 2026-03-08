using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE {Supervise, Patrol, Chase, ReturnToPost}

public enum SoundID {Zombie,Player, AmbientMusic, AssaultMusic, CreepyMusic}

[Serializable]
public class SoundData
{
    public SoundID Id;
    public AudioClip[] Clips;
}
