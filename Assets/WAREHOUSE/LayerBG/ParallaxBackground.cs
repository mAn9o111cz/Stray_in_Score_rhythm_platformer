using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform; // background's layer transform
        public float parallaxEffectMultiplier; // background move speed
    }
    public float verticalMultilier = 1;
    public ParallaxLayer[] parallaxLayers; // background layer list
    private Vector3 previousCameraPosition; // previous cam position

    void Start()
    {
        previousCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        Vector3 currentCameraPosition = Camera.main.transform.position;
        Vector3 deltaMovement = currentCameraPosition - previousCameraPosition; // camera delta movement

        // iterate each background layer
        foreach (var layer in parallaxLayers)
        {
            if (layer.layerTransform != null)
            {
                Vector3 newPosition = layer.layerTransform.position;
                newPosition.x += deltaMovement.x * layer.parallaxEffectMultiplier;
                newPosition.y += deltaMovement.y * layer.parallaxEffectMultiplier * verticalMultilier; // for vertical parallax
                layer.layerTransform.position = newPosition;
            }
        }

        // update previous cam position
        previousCameraPosition = currentCameraPosition;
    }
}
