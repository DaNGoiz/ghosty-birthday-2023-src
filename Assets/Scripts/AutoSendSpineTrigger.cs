using UnityEngine;

public class AutoSendSpineTrigger : MonoBehaviour
{
    [Header("配置")]
    [Tooltip("连续点击超过 n 秒就跳舞；连续 n 秒不点击就回到 idle")]
    public float nSeconds = 5f;

    [Tooltip("Animator 的触发器名")]
    public string danceTrigger = "Dance";
    public string idleTrigger  = "Idle";

    [Tooltip("是否在 Start 时强制触发一次 Idle（可选）")]
    public bool forceIdleOnStart = false;

    private Animator anim;
    private bool isDancing = false;

    // 当前“连续点击”这段的起点时间；<0 表示当前没有连续段
    private float streakStartTime = -1f;

    // 最近一次点击的时间（用于判断“是否中断/是否超时”）
    private float lastClickTime = float.NegativeInfinity;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (forceIdleOnStart && anim != null)
        {
            anim.ResetTrigger(danceTrigger);
            anim.SetTrigger(idleTrigger);
        }
    }

    /// <summary>
    /// 每次点击时调用（UI按钮OnClick、或你的输入回调里调用）
    /// </summary>
    public void Click()
    {
        float now = Time.time;

        // 与上次点击间隔过大：认为上一个“连续段”已经断开，重新起一段
        if (now - lastClickTime > nSeconds || streakStartTime < 0f)
        {
            streakStartTime = now;
        }

        lastClickTime = now;

        // 仍处于“连续点击”中，且累计时长达到阈值，则进入跳舞
        if (!isDancing && (now - streakStartTime >= nSeconds))
        {
            StartDance();
        }
    }

    void Update()
    {
        float now = Time.time;

        // 兜底：如果已经连续点击达到阈值，且最近一次点击仍在 n 秒内，自动进入跳舞
        if (!isDancing &&
            streakStartTime >= 0f &&
            (now - streakStartTime >= nSeconds) &&
            (now - lastClickTime  <  nSeconds))
        {
            StartDance();
        }

        // 在跳舞状态下，如果 n 秒没有任何点击，回到 idle
        if (isDancing && (now - lastClickTime >= nSeconds))
        {
            StartIdle();
        }
    }

    private void StartDance()
    {
        isDancing = true;
        if (anim != null)
        {
            anim.ResetTrigger(idleTrigger);
            anim.SetTrigger(danceTrigger);
        }
    }

    private void StartIdle()
    {
        isDancing = false;
        if (anim != null)
        {
            anim.ResetTrigger(danceTrigger);
            anim.SetTrigger(idleTrigger);
        }
        // 回到 idle 后，下一次点击重新开始新的“连续段”
        streakStartTime = -1f;
    }
}
