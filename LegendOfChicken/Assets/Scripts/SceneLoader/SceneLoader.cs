using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]private AssetReference addressableSceneName; 
    [SerializeField] private AsyncOperationHandle<SceneInstance> _handle;
    public Slider loadingBar; 
    public TextMeshProUGUI progressText;
    
        public void Start()
        {
            Caching.ClearCache();
            LoadAddresableScene(addressableSceneName);
        }

        private void Update()
        {
            if (_handle.IsValid())
            {
                float currentProgress = _handle.GetDownloadStatus().Percent;
                loadingBar.value = currentProgress;
                progressText.text = Mathf.Round(currentProgress * 100) + "%";
            }

            if (Mathf.Approximately(_handle.PercentComplete, 1f))
            {
                Addressables.Release(_handle);
            }
        }

        private async void LoadAddresableScene(AssetReference addressableSceneName)
        {
            _handle = Addressables.LoadSceneAsync(addressableSceneName, LoadSceneMode.Single);
            await _handle.Task;
        }
}
