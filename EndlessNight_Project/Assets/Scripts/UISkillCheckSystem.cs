using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class UISkillCheckSystem : MonoBehaviour {

    private UIProgressBar m_bar;

    [SerializeField] private UIZone m_greatZone, m_perfectZone;

    public Text m_hitText;

    private bool b_isMoving;
    private bool b_enableMovingHitArea;
    private bool b_barInZone;

    public GameManager GameManager { get { return m_gameManager; } }
    private GameManager m_gameManager;
    private Unit m_unit;

    public GameObject m_tapIcon;

    [SerializeField] private float m_speedRotateMin, m_speedRotateMax;
    private float m_speedRotate;
    [SerializeField] private float m_startRotatingAt;

    [SerializeField] AudioClip m_hitSound, m_missSound;

    private void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_unit = GameObject.Find("Unit").GetComponent<Unit>();
        m_bar = GetComponentInChildren<UIProgressBar>();
    }

    void Start ()
    {
        b_isMoving = true;
        b_enableMovingHitArea = false;
        SpawnAllNewZones();
	}

	void Update ()
    {
        if (m_unit.UnitAnim.UnitAnimStat == UnitAnimStatus.Idle ||
            m_unit.UnitAnim.UnitAnimStat == UnitAnimStatus.Run)
        {
            CheckBarMovingPassZones();
        }

        if (b_enableMovingHitArea == true)
        {
            m_greatZone.ZoneMoving(m_speedRotate);
            m_perfectZone.GenerateNewZone(m_greatZone.EndAngle - (m_greatZone.ZoneImg.fillAmount * 100));
        }
    }

    public void SetIsMoving(bool isMoving)
    {
        b_isMoving = isMoving;
    }

    // Check if the bar is in the hit zone
    // If player not tap until it out of zone
    // Make it MISS!
    public void CheckBarMovingPassZones()
    {
        if (m_greatZone.CheckHit(m_bar.Angle) == true && m_bar.Status == ProgressBarStatus.WaitForZone)
        {
            m_bar.Status = ProgressBarStatus.InZone;
        }

        if (m_greatZone.CheckHit(m_bar.Angle) == false && m_bar.Status == ProgressBarStatus.InZone)
        {
            m_bar.Status = ProgressBarStatus.PassedZone;
            IsHitting(false, false);
        }
    }

    // Check if the bar hit the hitting areas or not
    public void CheckHit()
    {
        bool isHit = false;
        bool isPerfect = false;

        isHit = m_greatZone.CheckHit(m_bar.Angle);

        if (isHit == true)
        {
            isPerfect = m_perfectZone.CheckHit(m_bar.Angle);
        }

        IsHitting(isHit, isPerfect);
    }

    private void IsHitting(bool isHit, bool isPerfect)
    {
        if (isHit == true)
        {
            m_hitText.text = isPerfect == true ? "PERFECT!" : "GREAT!";

            if (isPerfect == true)
            {
                m_unit.EnablePerfectSpeed();
            }

            m_gameManager.UpdateSpeedText(m_unit);
            m_gameManager.IncrementCombo();
            ReduceSizeAndIncreaseSpeed();

            m_unit.UnitAnim.SetAnimation(UnitAnimStatus.Run);
            m_unit.UnitAnim.SetSpeed();

            if (m_gameManager.Combo > m_startRotatingAt)
            {
                EnableMovingZone(true);
            }

            m_gameManager.SoundManager.PlaySFXOneShot(m_hitSound);
        }
        else
        {
            m_hitText.text = "MISS!";
            m_gameManager.SoundManager.PlaySFXOneShot(m_missSound);
            m_unit.UnitFall();
             
            ResetVariables();
        }

        SpawnAllNewZones();
    }

    public void SpawnAllNewZones()
    {
        m_greatZone.GenerateNewZone(m_bar.Angle + Random.Range(150f, 220f));
        m_perfectZone.GenerateNewZone(m_greatZone.EndAngle - (m_greatZone.ZoneImg.fillAmount * 100));
    }

    public void ReduceSizeAndIncreaseSpeed()
    {
        m_greatZone.ReduceZoneSize();
        m_perfectZone.ReduceZoneSize();

        m_bar.IncreaseBarSpeed();
    }

    public void EnableMovingZone(bool enable)
    {
        b_enableMovingHitArea = enable;

        if (enable == true)
            m_speedRotate = Random.Range(m_speedRotateMin, m_speedRotateMax);
    }

    public void SetActiveUIZones(bool setActive, bool showTapIcon)
    {
        m_greatZone.gameObject.SetActive(setActive);
        m_perfectZone.gameObject.SetActive(setActive);
        m_bar.gameObject.SetActive(setActive);

        m_tapIcon.SetActive(showTapIcon);
    }

    public void ResetVariables()
    {
        EnableMovingZone(false);

        m_bar.ResetVariables();
        m_greatZone.ResetVariables();
        m_perfectZone.ResetVariables();
        SpawnAllNewZones();
        m_gameManager.ResetCombo();
        m_gameManager.UpdateSpeedText(m_unit);
    }
}
