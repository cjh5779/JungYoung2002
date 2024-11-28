using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public GameObject jung_ball;
    public GameObject fireball_spawn;
    public GameObject young_ball;
    public GameObject youngball_spawn;

    public bool move_flag = true;
    bool hill_flag = false;

    bool jung_flag = false;
    bool jung_ballflag = true;
    float jung_balltime = 4.0f;
    public float jung_ballt = 0.0f;

    bool young_flag = false;
    bool young_ballflag = true;
    float young_balltime = 12.0f;
    public float young_ballt = 0.0f;

    float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!jung_ballflag)
        {
            jung_ballt -= Time.deltaTime;
            if (jung_ballt - Time.deltaTime < 0)
            {
                jung_ballt = 0;
                jung_ballflag = true;
            }
        }

        if (!young_ballflag)
        {
            young_ballt -= Time.deltaTime;
            if (young_ballt - Time.deltaTime < 0)
            {
                young_ballt = 0;
                young_ballflag = true;
            }
        }

        // 스킬 사용 중 시간 카운트
        if (!move_flag)
        {
            time += Time.deltaTime;
        }
        // 일정 시간이 지나면 움직임 활성화
        if (time > 2.4f && jung_flag)
        {
            time = 0.0f;
            move_flag = true;
            jung_flag = false;
        }
        // 일정 시간이 지나면 움직임 활성화
        if (time > 2.2f && young_flag)
        {
            time = 0.0f;
            move_flag = true;
            young_flag = false;
        }
        // 일정 시간이 지나면 움직임 활성화
        if (time > 1.7f && hill_flag)
        {
            time = 0.0f;
            move_flag = true;
            hill_flag = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Rotate(0, 180, 0);
        }
        // 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftControl) && move_flag && jung_ballflag)
        {
                Instantiate(jung_ball, fireball_spawn.transform.position, this.transform.rotation);
                anim.SetTrigger("fireball_shoot");
                move_flag = false;
                jung_flag = true;
                jung_ballflag = false;
                jung_ballt = jung_balltime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && move_flag)
        {
            GameObject tmp = Instantiate(young_ball, youngball_spawn.transform.position, this.transform.rotation) as GameObject;
            tmp.transform.parent = tmp.transform;
            anim.SetTrigger("ultra_shoot");
            move_flag = false;
            young_flag = true;
            young_ballflag = false;
            young_ballt = young_balltime;
        }

        if(Input.GetKeyDown(KeyCode.E) && move_flag)
        {
            anim.SetTrigger("hill");
            move_flag = false;
            hill_flag = true;
        }
    }
}
