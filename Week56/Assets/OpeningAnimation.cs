using UnityEngine;
using UnityEngine.InputSystem;

public class OpeningAnimation : MonoBehaviour
{
    [Header("图片引用")]
    public GameObject image1;              // 第一张图片
    public GameObject image2;              // 第二张图片

    [Header("移出设置")]
    public float moveOutSpeed = 5f;        // 移出速度
    public float moveOutDistance = 2000f;  // 移出距离（UI单位）
    public string idleAnimationName = "Idle"; // Animator中上下移动动画的名字

    [Header("可选：禁用其他功能")]
    public MonoBehaviour[] scriptsToEnableAfter; // 开场后才启用的脚本（如相机控制）

    private bool isPlaying = true;         // 是否正在播放开场动画
    private bool isMovingOut = false;      // 是否正在移出
    private Vector3 image1StartPos;
    private Vector3 image2StartPos;
    private Animator animator1;
    private Animator animator2;
    private Mouse mouse;

    void Start()
    {
        mouse = Mouse.current;

        // 保存初始位置
        if (image1 != null)
        {
            image1StartPos = image1.transform.localPosition;
            animator1 = image1.GetComponent<Animator>();
        }

        if (image2 != null)
        {
            image2StartPos = image2.transform.localPosition;
            animator2 = image2.GetComponent<Animator>();
        }

        // 禁用其他脚本
        foreach (var script in scriptsToEnableAfter)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }
    }

    void Update()
    {
        if (!isPlaying) return;

        // 检测鼠标点击
        if (!isMovingOut && mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            StartMoveOut();
        }

        // 执行移出动画
        if (isMovingOut)
        {
            bool finished1 = MoveImageOut(image1, image1StartPos, true);
            bool finished2 = MoveImageOut(image2, image2StartPos, false);

            // 两张图都移出屏幕后
            if (finished1 && finished2)
            {
                FinishOpening();
            }
        }
    }

    void StartMoveOut()
    {
        isMovingOut = true;

        // 停止上下移动动画，切换到静止状态
        if (animator1 != null)
        {
            animator1.enabled = false;
        }
        if (animator2 != null)
        {
            animator2.enabled = false;
        }
    }

    bool MoveImageOut(GameObject image, Vector3 startPos, bool moveUp)
    {
        if (image == null) return true;

        // 向上或向下移动
        float direction = moveUp ? 1f : -1f;
        image.transform.localPosition += Vector3.up * direction * moveOutSpeed * Time.deltaTime * 100f;

        // 检查是否移出足够远
        float distance = Mathf.Abs(image.transform.localPosition.y - startPos.y);
        return distance >= moveOutDistance;
    }

    void FinishOpening()
    {
        isPlaying = false;

        // 隐藏或销毁图片
        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);

        // 启用其他脚本
        foreach (var script in scriptsToEnableAfter)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }

        Debug.Log("开场动画结束！");
    }

    // 可选：如果想让图片点击时触发
    public void OnImageClicked()
    {
        if (!isMovingOut)
        {
            StartMoveOut();
        }
    }
}