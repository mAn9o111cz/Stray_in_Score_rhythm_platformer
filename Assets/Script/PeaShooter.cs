using DG.Tweening;
using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [Header("射击设置")]
    public GameObject bulletPrefab; // 子弹预制体
    public Transform firePoint;     // 子弹生成位置
    public float bulletSpeed = 5f;  // 子弹速度
    public bool aimAtPlayer = false; // 是否追踪玩家

    [Header("节拍设置")]
    public int beatsPerShot = 4;    // 每几拍发射一次
    private int currentBeat = 0;

    private Transform player;       // 玩家引用
    private Vector2 defaultDirection = Vector2.right; // 默认向右射击

    void Start()
    {
        // 查找玩家（假设玩家有"Player"标签）
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        defaultDirection = transform.right; // 根据炮台朝向初始化方向
    }

    void OnEnable()
    {
        bpmRhythm.onBeat += OnBeat;
    }

    void OnDisable()
    {
        bpmRhythm.onBeat -= OnBeat;
    }

    void OnBeat()
    {
        currentBeat++;

        // 每4拍发射一次
        if (currentBeat % beatsPerShot == 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // 创建子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        // 设置子弹方向
        Vector2 shootDirection = aimAtPlayer && player != null ?
            (player.position - firePoint.position).normalized :
            defaultDirection;

        bulletScript.Initialize(shootDirection, bulletSpeed);
    }
}