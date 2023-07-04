using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetSystem : MonoBehaviour
{
    [SerializeField] public float SettingTime = 30;
    [SerializeField] GameObject Cursor;
    [SerializeField] GameObject Canbus;
    [SerializeField] TextMeshProUGUI InTargetGUI;
    [SerializeField] GameObject Obstacle;
    [SerializeField] public int NumberOfObstacle = 300;
    [SerializeField] float minVelocity = 2;
    [SerializeField] float maxVelocity = 5;
    private List<MoveTarget> Obstacles = new List<MoveTarget>();
    private List<bool> alreadyIngnitions = new List<bool>();
    public int CollisionNumber { get; set; } = 0;
    private float nowTime;
    public bool isTracing = false;
    private float Interval;



    // Start is called before the first frame update
    void Start()
    {
        Interval = SettingTime / NumberOfObstacle;
        Canbus.SetActive(false);
        for(int i = 0;i < NumberOfObstacle; i++)
        {
            int theta = Random.Range(0, 359);
            int dx = Random.Range(-1, 1);
            int dy = Random.Range(-1, 1);
            float velocity = Random.Range(minVelocity, maxVelocity);
            GameObject cloneObject = Instantiate(Obstacle, new Vector3(10.0f, i, 0.0f), Quaternion.identity);
            MoveTarget _moveTarget = cloneObject.AddComponent<MoveTarget>();
            _moveTarget.Initialize(new Vector3(Mathf.Cos(theta) * 8, Mathf.Sin(theta) * 8, 0) ,velocity, new Vector3((-1) * Mathf.Cos(theta+dx), (-1) * Mathf.Sin(theta+dy), 0));
            cloneObject.transform.parent = this.transform; // GameManagerを親に指定
            Obstacles.Add(_moveTarget);
            alreadyIngnitions.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTracing)
        {
            isTracing = true;
            Canbus.SetActive(false);

        }
        if (isTracing)
        {
            int n = Mathf.FloorToInt(nowTime / Interval);
            if (!alreadyIngnitions[n])
            {
                Obstacles[n].ignition();
                alreadyIngnitions[n] = true;
            }
            nowTime += Time.deltaTime;
            if (nowTime > SettingTime)
            {
                isTracing = false;
                nowTime = 0;
                Canbus.SetActive(true);
                InTargetGUI.text = CollisionNumber.ToString();
                CollisionNumber = 0;
                Destroy(this.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isTracing = false;
            nowTime = SettingTime;
            CollisionNumber = 0;
        }
    }
}
