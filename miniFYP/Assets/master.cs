using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class master : MonoBehaviour {

    public GameObject box;
    public GameObject point;
    public GameObject savePoint;

    public TextMeshProUGUI timeUI;
    public TextMeshProUGUI failUI;

    List<GameObject> Colorbox = new List<GameObject>();

    public int CountTime = 15;
    public int boxSize = 20;

    public float Timer = 15;
    public int fail = 0;

    public int maxFail = 5;

    public int Stage = 0;

    public GameObject WinUI;
    public GameObject LoseUI;
    public TextMeshProUGUI stageUI;

    bool gameEnd = false;

    public enum Boxcolor{
        red,
        green,
        blue,
    }
    

	void Start () {
        Create();
    }

    void Update()
    {
        if (!gameEnd)
        {
            Timer -= Time.deltaTime;
            timeUI.text = "Time \n" + Timer.ToString("0.0");
            if (Timer <= 0)
            {
                YouLose();
            }
            if(Colorbox.Count == 0)
            {
                YouWin();
            }
        }
    }

    void Create()
    {
        for (int i = 0; i < boxSize; i++)
        {
            Vector3 V = new Vector3(point.transform.position.x, point.transform.position.y + (i* 100), point.transform.position.z);
            GameObject B = Instantiate(box,V, Quaternion.identity);
            B.transform.SetParent(point.transform);
            B.transform.localScale = new Vector3(1, 1, 1);
            Colorbox.Add(B);
            switch (Randomcolor())
            {
                case 0: //red
                    B.GetComponent<Image>().color = new Color32(255,95,95,255);
                    B.GetComponent<mycolor>().color = Boxcolor.red;
                    break;
                case 1: // green
                    B.GetComponent<Image>().color = new Color32(136, 255, 136,255);
                    B.GetComponent<mycolor>().color = Boxcolor.green;
                    break;
                case 2: // blue
                    B.GetComponent<Image>().color = new Color32(136, 136, 255,255);
                    B.GetComponent<mycolor>().color = Boxcolor.blue;
                    break;

                default: // never
                    break;
            }
        }
    }

    public int Randomcolor()
    {
        int color = Random.Range(0, 3);
        return color;
    }

    public void Movedown()
    {
        point.transform.position = new Vector3(point.transform.position.x,point.transform.position.y-100,point.transform.position.z);
    }

    public void UpdateFail()
    {
        failUI.text = "Fail \n" + fail;
        if(fail >= 5)
        {
            YouLose();
        }
    }
    public void UpdateStage()
    {
        stageUI.gameObject.SetActive(true);
        stageUI.text = "Stage " + Stage;
    }

    public void closeButton(bool close)
    {
        List<GameObject> Button = new List<GameObject>(GameObject.FindGameObjectsWithTag("Button"));
        foreach (GameObject B in Button)
        {
            B.gameObject.GetComponent<Button>().interactable = close;
        }
    }

    public void YouWin()
    {
        gameEnd = true;
        WinUI.SetActive(true);
        UpdateStage();
        closeButton(false);
    }

    public void YouLose()
    {
        gameEnd = true;
        LoseUI.SetActive(true);
        UpdateStage();
        closeButton(false);
    }

    public void RedClick()
    {
        if (Colorbox.Count != 0)
        {
            if (Colorbox[0].GetComponent<mycolor>().color == Boxcolor.red)
            {
                Destroy(Colorbox[0]);
                Colorbox.Remove(Colorbox[0]);
                Movedown();
            }
            else
            {
                fail += 1;
                UpdateFail();
            }
        }
       
    }

    public void GreenClick()
    {
        if (Colorbox.Count != 0)
        {
            if (Colorbox[0].GetComponent<mycolor>().color == Boxcolor.green)
            {
                Destroy(Colorbox[0]);
                Colorbox.Remove(Colorbox[0]);
                Movedown();
            }
            else
            {
                fail += 1;
                UpdateFail();
            }
        }
       
    }
    public void BlueClick()
    {

        if (Colorbox[0].GetComponent<mycolor>().color == Boxcolor.blue)
        {
            Destroy(Colorbox[0]);
            Colorbox.Remove(Colorbox[0]);
            Movedown();
        }
        else
        {
            fail += 1;
            UpdateFail();
        }
    }

    public void Nextstage()
    {

        fail = 0;
        Stage += 1;
        Timer += 15;
        boxSize += 5;
        gameEnd = false;
        WinUI.SetActive(false);
        stageUI.gameObject.SetActive(false);
        closeButton(true);
        point.transform.position = savePoint.transform.position;
        Colorbox.Clear();
        Create();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
