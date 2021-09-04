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
        var scale = notificationObject.transform.localScale;
        notificationObject.transform.SetParent(this.transform);
        notificationObject.transform.localScale = scale;
        notificationObject.transform.localPosition = nextPosition;
        //todo: get animator and set it off to next position :)
        nextPosition = new Vector3(nextPosition.x, nextPosition.y + rectTransform.rect.height * rectTransform.localScale.y, nextPosition.z);
    }
    //todo: if you remove from the middle then move the rest down/up/whatever
    public void RemoveNotification(GameObject notificationObject) {
        Destroy(notificationObject);
        var rectTransform = notificationObject.transform as RectTransform;
        var positionDiff = rectTransform.rect.height * rectTransform.localScale.y;
        foreach (Transform child in this.transform) {
            child.localPosition = new Vector3(child.localPosition.x, child.localPosition.y - positionDiff, child.localPosition.z);
        }
        var lastChildPosition = transform.GetChild(transform.childCount - 1).localPosition;
        nextPosition = new Vector3(lastChildPosition.x, lastChildPosition.y + positionDiff, lastChildPosition.z);
        //nextPosition = new Vector3(nextPosition.x, nextPosition.y - rectTransform.rect.height * rectTransform.localScale.y, nextPosition.z);
    }
}
