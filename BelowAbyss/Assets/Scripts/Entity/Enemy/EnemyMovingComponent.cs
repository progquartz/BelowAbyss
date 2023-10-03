using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityMovingState
{
    IDLE,
    ATTACK,
    ATTACKFINISHED,
    ATTACKFALLBACK,
    HURT,
    HURTFINISHED,
    HURTFALLFRONT
}

public class EnemyMovingComponent : MonoBehaviour
{
    [SerializeField]
    private EnemyVisualOnNoneAnimation enemyVisual;
    [SerializeField]
    private Transform mostFrontPos;
    [SerializeField]
    private Transform mostBackPos;

    [SerializeField]
    private Transform centerPos;

    [SerializeField]
    private EntityMovingState state = EntityMovingState.IDLE;
    public EntityMovingState newState = EntityMovingState.IDLE;

    private float dashXPosition = 40.0f;
    private float fallBackXPosition = 20.0f;
    // 돌진 관련 시간
    private float dashTime = 0.07f;
    private float delayTime = 0.1f;
    private float backTime = 0.3f;


    [SerializeField]
    private int dashStackCounter = 0;
    private bool attackFallbackingHappening = false;
    private bool newAttackStacking = false;

    // 피격 관련 시간.
    private float hurtDashTime = 0.07f;
    private float hurtDelayTime = 0.1f;
    private float hurtFrontTime = 0.3f;

    [SerializeField]
    private int hurtStackCounter = 0;
    private bool hurtFallbackHapperning = false;
    private bool newHurtStacking = false;


    private void Update()
    {
        if(newState != state)
        {
            if(newState == EntityMovingState.ATTACK)
            {
                state = EntityMovingState.ATTACK;
                
                Attack();
            }
            else if(newState == EntityMovingState.ATTACKFINISHED)
            {
                state = EntityMovingState.ATTACKFINISHED;
                // 추가 작업 필요
                AttackFinished();
            }
            else if(newState == EntityMovingState.ATTACKFALLBACK)
            {
                state = EntityMovingState.ATTACKFALLBACK;
                FallBack();
            }
            else if (newState == EntityMovingState.IDLE)
            {
                state = EntityMovingState.IDLE;
            }
            else if(newState == EntityMovingState.HURT)
            {
                // 만약 그냥 공격 중에 피격받으면, 그냥 빨개지는걸로.
                if(state == EntityMovingState.IDLE || state == EntityMovingState.ATTACKFALLBACK || state == EntityMovingState.HURTFALLFRONT || state == EntityMovingState.HURTFINISHED)
                {
                    state = EntityMovingState.HURT;
                    Hurt();
                }
            }
            else if(newState == EntityMovingState.HURTFINISHED)
            {
                state = EntityMovingState.HURTFINISHED;
                HurtFinished();
            }
            else if(newState == EntityMovingState.HURTFALLFRONT)
            {
                state = EntityMovingState.HURTFALLFRONT;
                HurtComeFront();
            }
        }
    }

    public void MoveToOriginalPos()
    {
        transform.position = new Vector3(centerPos.position.x, transform.position.y, transform.position.z);
        newState = EntityMovingState.IDLE;
        state = EntityMovingState.IDLE;
    }

    public void AttackState()
    {
        transform.GetComponentInChildren<Animator>().SetTrigger("Attack");
        if(newState == EntityMovingState.ATTACK)
        {
            dashStackCounter++;
            // 추가 대쉬.
        }
        else if (newState == EntityMovingState.ATTACKFALLBACK)
        {
            dashStackCounter++;
            newAttackStacking = true;
        }
        else
        {
            newState = EntityMovingState.ATTACK;
            dashStackCounter++;
        }
        
    }

    public void HurtState()
    {
        enemyVisual.HitSpriteColorControl();
        // 이미 공격당하는 중임.
        if (newState == EntityMovingState.HURT)
        {
            hurtStackCounter++;
        }
        else
        {
            newState = EntityMovingState.HURT;
            hurtStackCounter++;
        }
    }

    // 공격 돌진
    public void Attack()
    {
        StartCoroutine(AttackObjectXPos(transform, transform.position.x - dashXPosition, dashTime));
    }

    // 공격 딜레이
    public void AttackFinished()
    {
        StartCoroutine(AttackDelay(transform, delayTime));
    }

    // 공격 빠지기.
    public void FallBack()
    {
        // 아직 대쉬하고 있는 애가 있지 않으니 뒤로 빠지기.
        if (dashStackCounter <= 1)
        {
            if(!attackFallbackingHappening)
            {
                attackFallbackingHappening = true;
                StartCoroutine(AttackBackObjectXPos(transform, centerPos.transform.position.x, backTime));
            }
        }
        // 돌진중이면, 아무것도 하지 않기.
        else
        {
            dashStackCounter--;
        }
    }

    // 피격 빠지기
    public void Hurt()
    {
        StartCoroutine(HurtObjectXPos(transform, transform.position.x + fallBackXPosition, hurtDashTime));
    }

    // 피격 딜레이
    public void HurtFinished()
    {
        StartCoroutine(HurtDelay(transform, hurtDelayTime));
    }
    // 피격 돌아오기
    public void HurtComeFront()
    {
        // 아직 대쉬하고 있는 애가 있지 않으니 뒤로 빠지기.
        if (hurtStackCounter <= 1)
        {
            if (!hurtFallbackHapperning)
            {
                hurtFallbackHapperning = true;
                StartCoroutine(HurtBackObjectXPos(transform, centerPos.transform.position.x, hurtFrontTime));
            }
        }
        // 돌진중이면, 아무것도 하지 않기.
        else
        {
            hurtStackCounter--;
        }
    }

    public void Idle()
    {

    }

    public static float EaseIn(float t)
    {
        return t * t * t;
    }

    public static float BackEaseIn(float t)
    {
        return t * t * t;
    }

    // 공격 시 앞으로 돌진
    public IEnumerator AttackObjectXPos(Transform transform, float x_target, float duration)
    {
        float elapsed_time = 0; //Elapsed time

        Vector3 pos = transform.position; //Start object's position

        float x_start = pos.x; //Start "y" value

        while (elapsed_time <= duration) //Inside the loop until the time expires
        {
            pos.x = Mathf.Lerp(x_start, x_target, EaseIn(elapsed_time / duration));
            transform.position = pos;//Changes the object's position

            yield return null; //Waits/skips one frame

            elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
        }
        newState = EntityMovingState.ATTACKFINISHED;
    }

    // 공격 이후 돌아오는 부분.
    public IEnumerator AttackBackObjectXPos(Transform transform, float x_target, float duration)
    {
        float elapsed_time = 0; //Elapsed time

        Vector3 pos = transform.position; //Start object's position

        float x_start = pos.x; //Start "y" value

        while (elapsed_time <= duration) //Inside the loop until the time expires
        {
            if(newAttackStacking)
            {
                newAttackStacking = false;
                attackFallbackingHappening = false;
                newState = EntityMovingState.ATTACK;
                yield break;
            }
            pos.x = Mathf.Lerp(x_start, x_target, EaseIn(elapsed_time / duration));
            //pos.x = Mathf.Lerp(x_start, x_target, elapsed_time / duration);
            transform.position = pos;//Changes the object's position

            yield return null; //Waits/skips one frame

            elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
        }
        attackFallbackingHappening = false;
        newState = EntityMovingState.IDLE;
    }

    // 공격 이후 딜레이 시간.
    public IEnumerator AttackDelay(Transform transform, float duration)
    {
        yield return new WaitForSeconds(delayTime);
        newState = EntityMovingState.ATTACKFALLBACK;
        dashStackCounter--;
        yield return null;
    }

    // 피격당한 몹 뒤로 가는 애니메이션
    public IEnumerator HurtObjectXPos(Transform transform, float x_target, float duration)
    {
        float elapsed_time = 0; //Elapsed time

        Vector3 pos = transform.position; //Start object's position

        float x_start = pos.x; //Start "y" value

        while (elapsed_time <= duration) //Inside the loop until the time expires
        {
            pos.x = Mathf.Lerp(x_start, x_target, EaseIn(elapsed_time / duration));
            transform.position = pos;//Changes the object's position

            yield return null; //Waits/skips one frame

            elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
        }
        newState = EntityMovingState.HURTFINISHED;
    }

    // 피격 공격 이후 돌아오는 부분.
    public IEnumerator HurtBackObjectXPos(Transform transform, float x_target, float duration)
    {
        float elapsed_time = 0; //Elapsed time

        Vector3 pos = transform.position; //Start object's position

        float x_start = pos.x; //Start "y" value

        while (elapsed_time <= duration) //Inside the loop until the time expires
        {
            if (newHurtStacking)
            {
                newHurtStacking = false;
                hurtFallbackHapperning = false;
                newState = EntityMovingState.HURT;
                yield break;
            }
            pos.x = Mathf.Lerp(x_start, x_target, EaseIn(elapsed_time / duration));
            //pos.x = Mathf.Lerp(x_start, x_target, elapsed_time / duration);
            transform.position = pos;//Changes the object's position

            yield return null; //Waits/skips one frame

            elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
        }
        hurtFallbackHapperning = false;
        newState = EntityMovingState.IDLE;
    }

    // 피격 이후 딜레이 시간.
    public IEnumerator HurtDelay(Transform transform, float duration)
    {
        yield return new WaitForSeconds(delayTime);
        newState = EntityMovingState.HURTFALLFRONT;
        hurtStackCounter--;
        yield return null;
    }

}
