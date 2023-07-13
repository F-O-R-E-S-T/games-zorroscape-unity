using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Requests : MonoBehaviour
{
    [SerializeField] private Endpoints _endpoints;
    public ApiData PreviousData;

    private IEnumerator Start()
    {
        PreviousData = new ApiData();

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
                if (DataHasChanged(data))
                {
                    // El contenido ha cambiado, puedes continuar con el siguiente paso de la recursión
                    Debug.Log("Contenido del JSON ha cambiado");

                    // Accede a los valores obtenidos de la API
                    Debug.Log("Valor de x: " + data.x);
                    Debug.Log("Valor de y: " + data.y);
                    Debug.Log("Estado de jumping: " + data.jumping);
                    Debug.Log("Valor de attacking: " + data.attacking);

                    PreviousData = data;
                }

                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(GET());
            }
            else
            {
                Debug.Log("Error en la solicitud: " + www.error);
            }
        }
    }

    private bool DataHasChanged(ApiData newData)
    {
        if (newData.x != PreviousData.x ||
            newData.y != PreviousData.y ||
            newData.jumping != PreviousData.jumping ||
            newData.attacking != PreviousData.attacking)
        {
            PreviousData = newData;
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class ApiData
{
    public int x;
    public int y;
    public bool jumping;
    public int attacking;
}
