using System;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualGameObject;
    
    private void Start()
    {
        // Access singletons in start, not awake, since singletons are set in Awake, before start has been called (aka you could get a null reference error)
        // Use awake for initialization, use start for access
        Player.Instance.OnSelectedCounterChanged += InstanceOnSelectedCounterChanged;
    }

    private void InstanceOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
