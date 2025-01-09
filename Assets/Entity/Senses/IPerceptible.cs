using UnityEngine;

public interface IPerceptible
{
    public enum Faction
    {
        Allies,
        Axis,
        Neutral,
    };

    public Faction GetFaction();
    public Transform GetTransform();

    public Vector3 GetOffSetForLineOfSightCheck();
}
