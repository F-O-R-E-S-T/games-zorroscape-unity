using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Requests : MonoBehaviour
{
    [SerializeField] private Endpoints _endpoints;
    private ApiData previousData;

    [System.Serializable]
    public class ApiData
    {
        public int x;
        public int y;
        public bool jumping;
        public int attacking;
    }

    private IEnumerator Start()
    {
        previousData = new ApiData();

        yield return StartCoroutine(GET());
    }

    private IEnumerator GET()
    {
        string uri = _endpoints.DevelopEndpoint + _endpoints.StateEndpoint;
        Debug.Log("uri= " + uri);
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonData = www.downloadHandler.text;
                ApiData data = JsonUtility.FromJson<ApiData>(jsonData);

                // Compara el contenido del JSON actual con el anterior
                if (!DataHasChanged(data))
                {
                    // El contenido no ha cambiado, realiza una nueva solicitud despu�s de 1 segundo
                    yield return new WaitForSeconds(1f);
                    yield return StartCoroutine(GET());
                }
                else
                {
                    // El contenido ha cambiado, puedes continuar con el siguiente paso de la recursi�n
                    Debug.Log("Contenido del JSON ha cambiado");

                    // Accede a los valores obtenidos de la API
                    Debug.Log("Valor de x: " + data.x);
                    Debug.Log("Valor de y: " + data.y);
                    Debug.Log("Estado de jumping: " + data.jumping);
                    Debug.Log("Valor de attacking: " + data.attacking);
                }
            }
            else
            {
                Debug.Log("Error en la solicitud: " + www.error);
            }
        }
    }

    private bool DataHasChanged(ApiData newData)
    {
        if (newData.x != previousData.x ||
            newData.y != previousData.y ||
            newData.jumping != previousData.jumping ||
            newData.attacking != previousData.attacking)
        {
            previousData = newData;
            return true;
        }

        return false;
    }
}
