using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image Loandingbar_back;
    public Image Loadingbar_front;
    public GameObject Tip1;
    public GameObject Tip2;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        Tip2.SetActive(false);
        StartCoroutine(SceneCoroutine());
        Invoke("TipChange", 1f);
    }

    IEnumerator SceneCoroutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        while (!op.isDone)
        {

            yield return null;

            op.allowSceneActivation = false;

            float timer = 0;
            yield return new WaitForSeconds(0.3f);
            while (!op.isDone)
            {
                
                timer += Time.deltaTime;
                if (op.progress < 0.9f)
                {
                    Loadingbar_front.fillAmount = Mathf.Lerp(Loadingbar_front.fillAmount, op.progress, timer);
                    
                    if (Loadingbar_front.fillAmount >= op.progress)
                    {
                        timer += Time.deltaTime;
                        Debug.Log("timer: " + timer);
                    }
                }
                else
                {
                    Loadingbar_front.fillAmount = Mathf.Lerp(Loadingbar_front.fillAmount, 1f, timer);
                    if (Loadingbar_front.fillAmount == 1.0f)
                    {
                        op.allowSceneActivation = true;
                        SceneManager.LoadSceneAsync(sceneName);
                        Debug.Log("timer: " + timer);
                    }

                }
                yield return new WaitForSeconds(0.2f);
                yield return null;
            }
        }
    }
    void TipChange()
    {
        Tip1.SetActive(false);
        Tip2.SetActive(true);
    }
}


    
      

