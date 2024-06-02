using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "ScriptableObjects/MovementSettings", order = 1)]
public class HumanSettings : ScriptableObject
{
    public float movementSpeed = 50f;
    public float startDelay = 0.1f;
    public float positionThreshold = 0.01f;
    public AudioClip soundClip;
    public AnimationClip animationClip;
}