using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bow : MonoBehaviour
{
    public static Bow instance;
    [SerializeField] public Image icon;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float maxPullTime = 1.5f;
    [SerializeField] float shootForceMax = 30f;
    [SerializeField] float shootForceMin = 10f;
    [SerializeField] public float shootDelay = 2f;
    [SerializeField] public float dame;
    [SerializeField] Slider pullTimeSlider;
    [SerializeField] Image countdownImage;
    [SerializeField] float maxArrowLifetime = 2f;
    bool isPulling = false;
    bool canShoot = true;
    Vector3 originalScale;
    Vector3 mouseDownPosition;
    float pullTime;
    Animator anim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        originalScale = transform.localScale;
        anim = GetComponent<Animator>();
        pullTimeSlider = GameObject.FindWithTag("BowSlider").GetComponent<Slider>();
        countdownImage = GameObject.FindWithTag("TimeAttack").GetComponent<Image>();
        if (pullTimeSlider != null)
        {
            pullTimeSlider.maxValue = maxPullTime;
            pullTimeSlider.value = 0f;
        }

        if (countdownImage != null)
        {
            countdownImage.fillAmount = 0f;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartPulling();
            AudioManager.instance.PlayLoopingSFX(AudioManager.instance.bowLoading);
        }

        if (Input.GetMouseButtonUp(0) && isPulling)
        {
            StopPulling();
            AudioManager.instance.StopLoopingSFX();
            AudioManager.instance.PlaySFX(AudioManager.instance.bowShoot);
        }

        if (isPulling)
        {
            UpdatePull();
        }
    }

    void StartPulling()
    {
        isPulling = true;
        anim.SetBool("IsAttack", isPulling);
        mouseDownPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pullTime = 0f;
        if (pullTimeSlider != null)
        {
            pullTimeSlider.value = 0f;
        }
    }

    void UpdatePull()
    {
        pullTime += Time.deltaTime;

        if (pullTimeSlider != null)
        {
            pullTimeSlider.value = Mathf.Clamp(pullTime, 0f, maxPullTime);
        }

        float normalizedPullTime = Mathf.Clamp01(pullTime / maxPullTime);
        float scaleModifier = normalizedPullTime;
        transform.localScale = new Vector3(originalScale.x + scaleModifier, originalScale.y, originalScale.z);
    }

    void StopPulling()
    {
        isPulling = false;
        anim.SetBool("IsAttack", isPulling);

        StartCoroutine(ShootArrow());

        transform.localScale = originalScale;
        if (pullTimeSlider != null)
        {
            pullTimeSlider.value = 0f;
        }
    }

    IEnumerator ShootArrow()
    {
        canShoot = false;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Vector3 direction = (endPoint.position - firePoint.position).normalized;
        float normalizedPullTime = Mathf.Clamp01(pullTime / maxPullTime);
        float shootForce = Mathf.Lerp(shootForceMin, shootForceMax, normalizedPullTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * shootForce, ForceMode2D.Impulse);

        float arrowLifetime = Mathf.Lerp(1f, maxArrowLifetime, normalizedPullTime);
        Destroy(arrow, arrowLifetime);
        float newAttackSpeed = shootDelay - PlayerStats.instance.atkSpeed;
        if (countdownImage != null)
        {
            countdownImage.fillAmount = 1f;
            for (float i = 0; i < newAttackSpeed; i += Time.deltaTime)
            {
                countdownImage.fillAmount = 1f - Mathf.Clamp01(i / newAttackSpeed);
                yield return null;
            }
            countdownImage.fillAmount = 0f;
        }
        else
        {
            yield return new WaitForSeconds(newAttackSpeed);
        }

        canShoot = true;
    }
}
