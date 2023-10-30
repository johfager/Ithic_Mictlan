using UnityEngine;

[CreateAssetMenu(menuName = "HeroAttackObject")]
public class HeroAttackObject : ScriptableObject
{
    //Takes in the animation that is supposed to be played.
    public AnimatorOverrideController animatorOV;
    //Damage variable of attack.
    public float damage = 5;
}
