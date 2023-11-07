using System.Collections;
using System.Collections.Generic;
using Heroes;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class HeroesCombat : MonoBehaviour
{
    public List<HeroAttackObject> primaryAttack;
    public List<HeroAttackObject> primaryAbility;
    public List<HeroAttackObject> secondaryAbility;
    public List<HeroAttackObject> ultimateAbility;

    private float lastClickedTime;
    private float lastComboEnd;
    private int comboCounter;
    private Animator anim;

    private float followUpAttackTimer = 0.0f; //Not used
    private float attackDamage;
    private string currentAttack;

    public TextMeshProUGUI combatStateText;
    [SerializeField] float comboTime = 0.4f;

    private PlayerManager playerManager; //Not used
    public bool IsInCombatMode;

    private enum HeroesAttackState
    {
        Idle,
        PrimaryAttack,
        PrimaryAbility,
        SecondaryAbility,
        UltimateAbility
    }

    private HeroesAttackState currentHeroesAttackState = HeroesAttackState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        combatStateText.enabled = false; // Hide the debug text
        anim = GetComponent<Animator>();
        playerManager = GetComponentInParent<PlayerManager>(); // Get the PlayerManager reference
    }

    public void HandleAttackStateMachine()
    {
        if (currentHeroesAttackState == HeroesAttackState.Idle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentAttack = "PrimaryAttack";
                StartCoroutine(StartAttackAnimation(currentAttack, primaryAttack));
            }
            else if (Input.GetMouseButtonDown(1))
            {
                currentAttack = "PrimaryAbility";
                StartCoroutine(StartAttackAnimation(currentAttack, primaryAbility));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentAttack = "SecondaryAbility";
                StartCoroutine(StartAttackAnimation(currentAttack, secondaryAbility));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                currentAttack = "UltimateAbility";
                StartCoroutine(StartAttackAnimation(currentAttack, ultimateAbility));
            }
        }
    }

    private IEnumerator StartAttackAnimation(string attackAnimationName, List<HeroAttackObject> attackType)
    {
        IsInCombatMode = true;

        if (Time.time - lastComboEnd > 0.5f && comboCounter < attackType.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= comboTime)
            {
                anim.runtimeAnimatorController = attackType[comboCounter].animatorOV;
                anim.Play(attackAnimationName, 0, 0);
                attackDamage = attackType[comboCounter].damage;
                //attackDamage = HeroStats.Instance.combatAttributes.basicAttackDamage;
                Debug.Log($"Current attack is dealing {attackDamage} damage");
                comboCounter++;
                lastClickedTime = Time.time;
                if (comboCounter >= attackType.Count)
                {
                    comboCounter = 0;
                }
            }
        }

        // Wait for the animation to finish
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        IsInCombatMode = false;

        ExitAttack(currentAttack);
    }

    //Not used
    // void HandleIdleState()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         currentHeroesAttackState = HeroesAttackState.PrimaryAttack;
    //     }
    //     else if (Input.GetMouseButtonDown(1))
    //     {
    //         currentHeroesAttackState = HeroesAttackState.PrimaryAbility;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftShift))
    //     {
    //         currentHeroesAttackState = HeroesAttackState.SecondaryAbility;
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         currentHeroesAttackState = HeroesAttackState.UltimateAbility;
    //     }
    // }

    //Not used
    // void Attack(string attackAnimationName, List<HeroAttackObject> attackType)
    // {
    //     if (Time.time - lastComboEnd > 0.5f && comboCounter < attackType.Count)
    //     {
    //         CancelInvoke("EndCombo");

    //         if (Time.time - lastClickedTime >= comboTime)
    //         {
    //             anim.runtimeAnimatorController = attackType[comboCounter].animatorOV;
    //             anim.Play(attackAnimationName, 0, 0);
    //             attackDamage = attackType[comboCounter].damage;
    //             Debug.Log($"Current attack is dealing {attackDamage} damage");
    //             comboCounter++;
    //             lastClickedTime = Time.time;
    //             if (comboCounter >= attackType.Count)
    //             {
    //                 comboCounter = 0;
    //             }
    //         }
    //     }
    // }

    void ExitAttack(string attackTagName)
    {
        if (attackTagName != null)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f &&
                anim.GetCurrentAnimatorStateInfo(0).IsTag(attackTagName))
            {
                Invoke("EndCombo", 1);
            }
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
