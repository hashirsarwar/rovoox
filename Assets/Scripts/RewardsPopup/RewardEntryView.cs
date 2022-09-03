using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RewardEntryView : MonoBehaviour
{
    [SerializeField] private Image _avatar;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private TextMeshProUGUI _coinCount;
    [SerializeField] private Button _collectButton;

    private IAddCoinsProduction _addCoinsProduction;
    
    public void SetData(Reward reward, IAddCoinsProduction addCoinsProduction)
    {
        StartCoroutine(SetAvatar(reward.avatarUrl));
        _title.text = reward.title;
        _message.text = reward.message;
        _coinCount.text = reward.coins.ToString();
        _addCoinsProduction = addCoinsProduction;
        
        _collectButton.onClick.RemoveAllListeners();
        _collectButton.onClick.AddListener(OnCollectButtonClicked);
    }

    public void SetButtonLockedState()
    {
        _collectButton.interactable = false;
        _collectButton.GetComponent<DefaultButton>().GreyOutButton();
    }
    
    private void SetButtonUnlockedState()
    {
        _collectButton.interactable = true;
        _collectButton.GetComponent<DefaultButton>().ReturnColorToNormal();
    }

    private void OnCollectButtonClicked()
    {
        Utils.PlayButtonImpact();
        _addCoinsProduction.PlayAddCoinsProduction(_coinCount.transform.position, int.Parse(_coinCount.text));
    }

    IEnumerator SetAvatar(string url)
    {   
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.DataProcessingError ||
            request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
        {
            var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f));
            _avatar.sprite = sprite;
        }
    } 
    
}
