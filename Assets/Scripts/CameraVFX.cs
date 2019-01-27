using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraVFX : MonoBehaviour, IService
{
	[SerializeField]
	private Material Mat;
	private Camera camera;
    public Image chargeUpImageFill;
    public Image healthFill;

    private void Awake() {
        ServiceLocator.Instance.AddService(this);
		camera = GetComponent<Camera> ();
		originalZoom = camera.orthographicSize;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, Mat);
	}

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<CameraVFX>();
    }



    //Chromatic Aberration
    public void ChromaticAberration(float initDistance, float duration) {
		StopCoroutine ("ChromaticAberration");
		StartCoroutine (EnumChromaticAbberation (initDistance, duration));
	}

	public void ChromaticAberration() {
		StartCoroutine (EnumChromaticAbberation (0.05f, 0.5f));
	}

	private IEnumerator EnumChromaticAbberation(float initDistance, float duration) {
		float time = 0;
		float _RedX = initDistance;
		float _BlueX = -initDistance;
		float percentage = 0;

		while (time < duration) {
			percentage = time / duration;
			percentage = Mathf.Pow(percentage, 2);
			_RedX = Mathf.Lerp (initDistance, 0, percentage);
			_BlueX = Mathf.Lerp (-initDistance, 0, percentage);
			Mat.SetFloat ("_RedX", _RedX);
			Mat.SetFloat ("_BlueX", _BlueX);

			time += Time.deltaTime;
			yield return null;
		}

		Mat.SetFloat ("_RedX", 0);
		Mat.SetFloat ("_BlueX", 0);
	}



	//Camera Shake
	public void Shake() {
		StartCoroutine (ShakeEnum(1f, 0.7f, 1f));
	}

	public void Shake(float duration, float shakeAmount, float decreaseFactor) {
		StopCoroutine ("Shake");
		StartCoroutine (ShakeEnum(duration, shakeAmount, decreaseFactor));
	}

	private IEnumerator ShakeEnum(float duration, float shakeAmount, float decreaseFactor) {
		float time = duration;
		Vector3 originalPos = transform.localPosition;
		while (time > 0) {
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			time -= Time.deltaTime * decreaseFactor;

			yield return null;
		}
		transform.localPosition = originalPos;
	}



	//Camera Zoom
	public float originalZoom;

	public void Zoom() {
		StartCoroutine (ZoomEnum(0.01f, 1f));
	}

	public void Zoom(float duration, float zoomAmount) {
		StopCoroutine ("Zoom");
		StartCoroutine (ZoomEnum(duration, zoomAmount));
	}

	private IEnumerator ZoomEnum(float duration, float amount) {
		camera.orthographicSize += amount;

		float percentage = 1;
		float time = duration;
		while (time >= 0) {
			percentage = time / duration;
			time -= Time.deltaTime;
			camera.orthographicSize = Mathf.Lerp (originalZoom, originalZoom + amount, percentage);
			yield return null;
		}

		camera.orthographicSize = originalZoom;
	}
}