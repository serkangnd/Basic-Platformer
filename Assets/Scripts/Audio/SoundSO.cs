using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Audio/Sound Data", fileName ="New Sound Data")]
public class SoundSO : ScriptableObject
{
    public SoundList[] sounds;
}
