using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedNotifications : MonoBehaviour
{
    public Vector3 nextPosition;
    public Canvas canvas;

    private Camera gameCamera;

    private void Awake() {
        gameCamera = Camera.main;
        nextPosition = this.transform.position;
    }

    public void AddNotification(GameObject notificationObject) {
        var rectTransform = notificationObject.transform as RectTransform;
        notificationObject.transform.position = nextPosition;
        notificationObject.transform.SetParent(this.transform);
        //todo: get animator and set it off to next position :)
        nextPosition = new Vector3(nextPosition.x, nextPosition.y + rectTransform.rect.height * rectTransform.localScale.y, nextPosition.z);
    }
    //todo: if you remove from the middle then move the rest down/up/whatever
    public void RemoveNotification(GameObject notificationObject) {
        Destroy(notificationObject);
        var rectTransform = notificationObject.transform as RectTransform;
        var positionDiff = rectTransform.rect.height * rectTransform.localScale.y;
        foreach (Transform child in this.transform) {
            child.position = new Vector3(child.position.x, child.position.y - positionDiff, child.position.z);
        }
        //var rectTransform = notificationObject.transform as RectTransform;
        //nextPosition = new Vector3(nextPosition.x, nextPosition.y - rectTransform.rect.height * rectTransform.localScale.y, nextPosition.z);
    }
}
