using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetitems : MonoBehaviour
{
    private string SceneName = "SampleScene"; //场景名称

    public void ResetScene()//调用此函数获得场景信息
    {
        //SceneName = SceneManager.GetActiveScene().name;//获取场景名称
        //index = SceneManager.GetActiveScene().buildIndex;//获取场景所在序号
        SceneManager.LoadScene(SceneName);//加载所需场景,SceneName为场景名

    }
}
