using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using System.Collections;
public class ScenesSwitcher : MonoBehaviour
{
    [SerializeField] private string _sceneAddress; 
    [SerializeField, Min(0f)] private float _fakeTailSeconds = 0.2f;

    // маленький «хвост», чтобы полоса красиво дошла до 100%
    
    public void LoadThisScene() 
    { 
        StartCoroutine(LoadSceneByAddress(_sceneAddress)); 
    } 
    
    private IEnumerator LoadSceneByAddress(string address) 
    { 
        LoadingScreen.Instance?.Show(); 
        Debug.Log($"Пробуем загрузить сцену {address}");
        var handle = Addressables.LoadSceneAsync(address, LoadSceneMode.Single, false); 

        // Обновляем прогресс до 0.9
        while (handle.PercentComplete < 0.9f) 
        { 
            LoadingScreen.Instance?.SetProgress(handle.PercentComplete); 
            yield return null; 
        } 

        // Дожидаемся полной загрузки (но прогресс уже не растёт сам)
        yield return handle; 

        if (handle.Status != AsyncOperationStatus.Succeeded) 
        { 
            Debug.LogError($"Ошибка загрузки сцены {address}: {handle.Status}"); 
            LoadingScreen.Instance?.Hide(); yield break; 
        }

        // Активируем сцену
        var activation = handle.Result.ActivateAsync();

        // Пока сцена активируется — можно плавно дотянуть прогресс до 1
        float elapsed = 0f; 
        while (!activation.isDone) 
        { 
            elapsed += Time.deltaTime; 
            float fakeProgress = Mathf.Lerp(0.9f, 1f, elapsed / _fakeTailSeconds); 
            LoadingScreen.Instance?.SetProgress(fakeProgress); yield return null; 
        } 
    }

    // Если хочешь, чтобы меню тоже грузилось с полоской — используй адрес Addressables
    public void LoadMainMenuAddressable(string addressOfMenu) 
    { 
        StartCoroutine(LoadSceneByAddress(addressOfMenu)); 
    }

    // Старый способ (без полосы). Лучше не использовать.
    public void LoadMainMenuBuildIndex() 
    { 
        SceneManager.LoadScene(0); 
    } 
}
