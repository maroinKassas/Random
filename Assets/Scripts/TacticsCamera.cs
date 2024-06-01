using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsCamera : MonoBehaviour
{
    private static float zoomSpeed = 2.0f; // Vitesse de zoom
    private static float minZoom = 3.0f;   // Taille orthographique minimum
    private static float maxZoom = 5.0f;   // Taille orthographique maximum (mise � jour � 5)

    private static int dragSpeed = 1; // Vitesse de d�placement
    private Vector3 dragOrigin;
    // Limites de la cam�ra
    /*private static int minX = -7;
    private static int maxX = 1;
    private static int minY = 2;
    private static int maxY = 7;
    private bool isDragging = false;*/

    private static Vector3 repositionPosition = new Vector3(-3f, 5f, -3f);
    // Dur�e du d�placement
    private static float moveDuration = 0.75f;
    private bool isMoving = false;


    void Update()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0.0f)
        {
            Camera.main.orthographicSize -= scrollData * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }

        if (Input.GetMouseButtonDown(1)) // Changement du bouton de 0 (clic gauche) � 1 (clic droit)
        {
            // Enregistre la position de la souris en coordonn�es du monde lorsque le clic droit est enfonc�
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1)) // Changement du bouton de 0 (clic gauche) � 1 (clic droit)
        {
            // Calcule la position actuelle de la souris en coordonn�es du monde
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentMousePos;

            // Calcule des limites
           /*difference.x = Mathf.Clamp(difference.x, minX, maxX);
            difference.y = Mathf.Clamp(difference.y, minY, maxY);*/

            // D�place la cam�ra en fonction de la diff�rence de position multipli�e par la vitesse de d�placement
            Camera.main.transform.position += difference * dragSpeed;
        }

        // Si le clic de la roulette de la souris est d�tect� et la cam�ra n'est pas d�j� en mouvement
        if (Input.GetMouseButtonDown(2) && !isMoving)
        {
            // D�clencher la coroutine pour le d�placement progressif
            StartCoroutine(MoveCameraToPosition(repositionPosition, moveDuration));
        }
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition, float duration)
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;

        // D�placer progressivement la cam�ra vers la position cible
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // S'assurer que la cam�ra est � la position cible exacte
        transform.position = targetPosition;
        isMoving = false;
    }
}