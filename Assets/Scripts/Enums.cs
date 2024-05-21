using System;

[Flags]
public enum Immunity
{
    None = 0,
    Freeze = 1,
    AllTraps = 2,
    Pierce = 4,
    StickyTrap = 8,
    Poison = 16,
}

public enum TargetPriority
{
    First,
    Last,
}

public enum EnemyAttackType
{
    Melee,
    Ranged,
}

public enum GatherState
{
    Gatherable,
    Gathering,
    Gathered,
}

[Flags]
public enum ResourceDropTypes
{
    None = 0,
    Wood = 1,
    Rock = 2,
}

