using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    float deltaTime = 0.0f;


    [SerializeField] Transform player;
    [SerializeField] PlayerData _playerData;


    [Header("-----UIManager-----")]
    [SerializeField] Text textScore;
    [SerializeField] Text textTimePlay;
    [SerializeField] Text textMessage;

    ////// thời gian chơi
    private float startTime;
    private int minutesTimePlay;
    private int secondsTimePlay;

    public int Score => _playerData.currentScore;

    public int HightScore => _playerData.hightScore;

    private void OnEnable()
    {
        ObserverManager.AddListener(ObserverKey.addScore,AddScore);
    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener(ObserverKey.addScore, AddScore);
    }

    private void Start()
    {
        LoadDataPlayer();
        this.startTime = Time.time;
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        this._playerData.positionPlayer = player.position;

        //// hiện thời gian
        this.TimePlay();
    }
    public void AddScore(params object[] datas)
    {
        this._playerData.currentScore += (int)datas[0];
        this.textScore.text = "Score: " + this._playerData.currentScore.ToString();
        if (this._playerData.currentScore > this._playerData.hightScore)
        {
            this._playerData.hightScore = this._playerData.currentScore;

        }
        SaveDataPlayer();
    }

    public void AddEnemyDied()
    {
        this._playerData.numberEnemyDie++;
    }

    private void OnDestroy()
    {
        SaveDataPlayer() ;
    }

    public void LoadDataPlayer()
    {
        string dataLoader =  PlayerPrefs.GetString(typeof(PlayerData).ToString(),"{}");
        if (dataLoader != null)
        {
            this._playerData = JsonUtility.FromJson<PlayerData>(dataLoader);
        }
        this._playerData.currentScore = 0;
    }

    public void SaveDataPlayer()
    {
        PlayerPrefs.SetString(typeof(PlayerData).ToString(), JsonUtility.ToJson(this._playerData));
    }

    public void ResetDataPlayer()
    {
        PlayerData playerData = new PlayerData();
    }

    /////////////////////////////////////// Timer//////////////////////////////////////
    void TimePlay()
    {
        float t = Time.time - startTime;
        minutesTimePlay = (int)t / 60;
        secondsTimePlay = (int)t % 60;
        this.textTimePlay.text = string.Format("Time: " + "{0:00}:{1:00}", minutesTimePlay, secondsTimePlay);
    }
}
[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int hightScore;
    public int currentScore;
    public int numberEnemyDie;
    public Vector3 positionPlayer;

    public PlayerData()
    {
        this.playerName = "JoJo";
        this.currentScore = 0;
        this.numberEnemyDie = 0;
        this.positionPlayer = new Vector3(-9f,-0.5f,0f);
    }

    public PlayerData(string playerName, int currentScore, int numberEnemyDie, Vector3 pos)
    {
        this.playerName = playerName;
        this.currentScore = currentScore;
        this.numberEnemyDie = numberEnemyDie;
        this.positionPlayer = pos;
    }
}
