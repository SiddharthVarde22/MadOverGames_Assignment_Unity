using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField]
    ScrollingFunctionality scrollingFunctionality;
    [SerializeField]
    Button buttonComponent;

    [SerializeField]
    float targetPositionToPass;

    private void Start()
    {
        buttonComponent.onClick.AddListener(OnButtonPressed);
    }

    public void OnButtonPressed()
    {
        scrollingFunctionality.ChangeTargetPosition(targetPositionToPass);
    }
}
