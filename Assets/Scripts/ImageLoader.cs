using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
	#region Fields
	[SerializeField]
	RawImage image;
	Button button;
	static UnityEvent noMoreLoading = new UnityEvent();
	string url;
	#endregion

	#region Properties

	#endregion

	#region Methods
	private void Awake()
	{
		button = GetComponent<Button>();
	}
	private void Start()
	{
		EventManager.NoMoreImagesToLoad.AddInvoker(noMoreLoading);
	}
	public async Task LoadImage(int index)
	{
		url = "http://data.ikppbb.com/test-task-unity-data/pics/" + index + ".jpg";
		using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
		{
			UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();
			while (!operation.isDone)
			{
				await Task.Yield();
			}

			if (webRequest.result == UnityWebRequest.Result.Success)
			{
				DownloadHandlerTexture downloader = webRequest.downloadHandler as DownloadHandlerTexture;
				image.texture = downloader.texture;
			}
			else
			{
				Debug.Log(webRequest.error);
				noMoreLoading.Invoke();
				Destroy(gameObject);
			}
		}
		button.onClick.AddListener(TransitionToView);
	}

	void TransitionToView()
	{
		TransitionManager.Instance.TransitionToView(url);
	}
	#endregion
}

