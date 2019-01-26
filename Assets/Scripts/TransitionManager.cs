using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour, IService
{
    [Header("Dependencies")]
    public Image fillImage;

    [Header("Misc")]
	public bool inTransition = false;
    [SerializeField]
	private AnimationCurve curve;

	private float minimumLogoTime = 1.0f; //Minimum time of the preloader

    public void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    public void Start()
    {
        StartCoroutine(PreloadFade());
	}

	public IEnumerator PreloadFade() {
		//Stalls logo for minimumLogoTime if the preload is longer than the minimumLogoTime
		if (Time.time < minimumLogoTime)
			yield return new WaitForSeconds (minimumLogoTime);
		else
			yield return new WaitForSeconds (Time.time);

		TransitionTo ("Menu");
	}

    public void TransitionTo(string scene)
    {
        TransitionTo(scene, 0.25f);
    }

    public void TransitionTo(string scene, float delay) {
		if (!inTransition)
			StartCoroutine(TransitionEnum (scene, delay));
	}

    private IEnumerator TransitionEnum(string scene, float delay) {
        fillImage.gameObject.SetActive(true);
        inTransition = true;

        float time = 0;
        while (time <= delay)
        {
            fillImage.fillAmount = curve.Evaluate(time / delay);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(scene);

        while (!loadingOperation.isDone)
            yield return new WaitForEndOfFrame();

        time = delay;
        while (time >= 0)
        {
            fillImage.fillAmount = curve.Evaluate(time / delay);
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }
        
        inTransition = false;
        fillImage.gameObject.SetActive(false);
    }
}
