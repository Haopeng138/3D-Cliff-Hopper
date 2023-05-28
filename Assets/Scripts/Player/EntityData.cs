using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Entity/EntityData", order = 0)]
public class EntityData : ScriptableObject
{

    [field: SerializeField]
    public int Health { get; private set; }
    [field: SerializeField]
    public float FallMultiplier {get; private set; }
    [field: SerializeField]
    public float Gravity { get; private set;}
    [field: SerializeField]
    public float MoveSpeed { get; private set; }
    [field: SerializeField]
    public int MaxJumps { get; private set; }
    [field: SerializeField]
    public float JumpSpeed { get; private set; }
    [field: SerializeField]
    public bool GodMode { get; set; }
}
