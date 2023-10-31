using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MairaCombat : MonoBehaviour
{
    public List<HeroAttackObject> primaryAttack;
    public List<HeroAttackObject> primaryAbility;
    public List<HeroAttackObject> secondaryAbility;
    public List<HeroAttackObject> ultimateAbility;

    private float lastClickedTime;

    private float lastComboEnd;

    private int comboCounter;

    private Animator anim;

    private MairaAttackState currentMairaAttackState;
    private float followUpAttackTimer = 0.0f;
    private float attackDamage;

    private string currentAttack;
    
    public TextMeshProUGUI combatStateText;
    [SerializeField] float comboTime;

    private PlayerManager playerManager;
    public bool IsInCombatMode;
    
    private enum MairaAttackState
    {
        Idle,
        PrimaryAttack,
        PrimaryAbility,
        SecondaryAbility,
        UltimateAbility
    }

    void Start()
    {
        comboTime = 0.4f;
        combatStateText.enabled = false; // Hide the debug text

        anim = GetComponent<Animator>();
        currentMairaAttackState = MairaAttackState.Idle;

        playerManager = GetComponentInParent<PlayerManager>(); // Get the PlayerManager reference
    }

    public void HandleAttackStateMachine()
    {
        if (currentMairaAttackState == MairaAttackState.Idle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentAttack = "MairaPrimaryAttack";
                StartCoroutine(StartAttackAnimation(currentAttack, primaryAttack));
            }
            else if (Input.GetMouseButtonDown(1))
            {
                currentAttack = "MairaPrimaryAbility";
                StartCoroutine(StartAttackAnimation(currentAttack, primaryAbility));
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentAttack = "MairaSecondaryAbility";
                StartCoroutine(StartAttackAnimation(currentAttack, secondaryAbility));
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                currentAttack = "MairaUltimateAbility";
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


    void HandleIdleState()
    {
        if (Input.GetMouseButtonDown(0))
            {
                currentMairaAttackState = MairaAttackState.PrimaryAttack;
            }
        else if (Input.GetMouseButtonDown(1))
        {
            currentMairaAttackState = MairaAttackState.PrimaryAbility;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentMairaAttackState = MairaAttackState.SecondaryAbility;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currentMairaAttackState = MairaAttackState.UltimateAbility;
        }
    }

    void Attack(string attackAnimationName, List<HeroAttackObject> attackType)
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter < attackType.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= comboTime)
            {
                anim.runtimeAnimatorController = attackType[comboCounter].animatorOV;
                anim.Play(attackAnimationName, 0, 0);
                attackDamage = attackType[comboCounter].damage;
                Debug.Log($"Current attack is dealing {attackDamage} damage");
                comboCounter++;
                lastClickedTime = Time.time;
                if (comboCounter >= attackType.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

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
