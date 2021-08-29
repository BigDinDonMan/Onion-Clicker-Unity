using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedNotifications : MonoBehaviour
{
    public Vector3 nextPosition;

    private void Awake() {
        nextPosition = this.transform.position;
    }

    public void AddNotification(GameObject notificationObject) {
        notificationObject.transform.SetParent(this.transform);
        notificationObject.transform.position = nextPosition;
        //todo: get animator and set it off to next position :)
        var rectTransform = notificationObject.transform as RectTransform;
        nextPosition = new Vector3(nextPosition.x, nextPosition.y + rectTransform.rect.height, nextPosition.z);
    }
}
