using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;

public enum SceneName
{
    Main,
}

public enum GameStatus
{
    Start,
    Playing,
    End,
}

public class GameManager : MonoBehaviour {

    public SoundManager SoundManager { get { return m_soundManager; } }
    private SoundManager m_soundManager;

    public TimeManager TimeManager { get { return m_timeManager; } }
    private TimeManager m_timeManager;

    public Record Record { get { return m_record; } }
    private Record m_record;

    public GameStatus GameStatus { get { return m_gameStatus; } }
    private GameStatus m_gameStatus;

    private Unit m_unit;
    private EnemyUnit m_enemy;

    public int Combo { get { return m_combo; } set { m_combo = value; } }
    private int m_combo;

    public GameObject m_bossIcon;

    public Text m_comboText;
    public Text m_speedText;
    public Text m_rangeText;
    public Text m_winText;

    public GameObject m_popUp;

    private void Awake()
    {
        m_soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        m_timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        m_record = Camera.main.GetComponent<Record>();
        m_unit = GameObject.Find("Unit").GetComponent<Unit>();
        m_enemy = GameObject.Find("Enemy").GetComponent<EnemyUnit>();
    }

    void Start ()
    {
        m_gameStatus = GameStatus.Playing;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadNewScene(SceneName.Main);
        }
	}

    public void UpdateSpeedText(Unit unit)
    {
        m_speedText.text = "Speed: " + unit.GetTotalRunSpeed() + " m/s" + "\n" +
                           "Acce: " + unit.Accelelation + "\n";
    }

    public void IncrementCombo()
    {
        m_combo++;
        m_comboText.text = "Combo\n" + m_combo.ToString();
    }

    public void ResetCombo()
    {
        m_combo = 0;
        m_comboText.text = "Combo\n" + m_combo.ToString();
    }

    public void SetGameStatus(GameStatus gameStat)
    {
        m_gameStatus = gameStat;
    }

    public static void GamePause()
    {
        Time.timeScale = 0;
    }
    
    public void GameEnd(bool win)
    {
        SetGameStatus(GameStatus.End);
        m_winText.text = win == true ? "You Win!!" : "You Lose!!";
        m_popUp.SetActive(true);
    }

    public void LoadNewScene(SceneName sName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sName.ToString());
    }

    public void ShowBossIcon(bool show)
    {
        m_bossIcon.SetActive(show);
        m_rangeText.text = Vector3.Distance(m_enemy.transform.position, m_unit.transform.position).ToString("F1") + " m.";
    }

    // Remove all log in console
    /*static public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }*/
}
