using UnityEngine;
using System.Collections;

namespace UnityChan
{
    [RequireComponent(typeof(Animator))]
    public class IdleChanger : MonoBehaviour
    {
        private Animator anim;                      // Animatorへの参照
        private AnimatorStateInfo currentState;     // 現在のステート状態を保存する参照
        private AnimatorStateInfo previousState;    // ひとつ前のステート状態を保存する参照
        public bool _random = false;                // ランダム判定スタートスイッチ
        public float _threshold = 0.5f;             // ランダム判定の閾値
        public float _interval = 10f;               // ランダム判定のインターバル

        // Use this for initialization
        void Start()
        {
            // 各参照の初期化
            anim = GetComponent<Animator>();
            currentState = anim.GetCurrentAnimatorStateInfo(0);
            previousState = currentState;
            // ランダム判定用関数をスタートする
            StartCoroutine("RandomChange");
        }

        // Update is called once per frame
        void Update()
        {
            // ↑キー/スペースが押されたら、ステートを次に送る処理
            if (Input.GetKeyDown("up") || Input.GetButton("Jump"))
            {
                // ブーリアンNextをtrueにする
                anim.SetBool("Next", true);
            }

            // ↓キーが押されたら、ステートを前に戻す処理
            if (Input.GetKeyDown("down"))
            {
                // ブーリアンBackをtrueにする
                anim.SetBool("Back", true);
            }

            // "Next"フラグがtrueの時の処理
            if (anim.GetBool("Next"))
            {
                // 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
                currentState = anim.GetCurrentAnimatorStateInfo(0);
                if (previousState.fullPathHash != currentState.fullPathHash) // 변경된 부분
                {
                    anim.SetBool("Next", false);
                    previousState = currentState;
                }
            }

            // "Back"フラグがtrueの時の処理
            if (anim.GetBool("Back"))
            {
                // 現在のステートをチェックし、ステート名が違っていたらブーリアンをfalseに戻す
                currentState = anim.GetCurrentAnimatorStateInfo(0);
                if (previousState.fullPathHash != currentState.fullPathHash) // 변경된 부분
                {
                    anim.SetBool("Back", false);
                    previousState = currentState;
                }
            }
        }

        // ランダム判定用関数
        IEnumerator RandomChange()
        {
            // 無限ループ開始
            while (true)
            {
                // ランダム判定スイッチオンの場合
                if (_random)
                {
                    // ランダムシードを取り出し、その大きさによってフラグ設定をする
                    float _seed = Random.Range(0.0f, 1.0f);
                    if (_seed < _threshold)
                    {
                        anim.SetBool("Back", true);
                    }
                    else if (_seed >= _threshold)
                    {
                        anim.SetBool("Next", true);
                    }
                }
                // 次の判定までインターバルを置く
                yield return new WaitForSeconds(_interval);
            }
        }
    }
}
