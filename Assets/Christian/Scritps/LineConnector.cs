using UnityEngine;
using UnityEngine.UI;

public class LineConnector : MonoBehaviour
{
    public GameObject m_lineaPrefab;
    public Transform canvasRef;
    public float m_grosor = 5f;

    public void CrearLinea(Vector3 origen, Vector3 destino)
    {
        GameObject pLineaObj = Instantiate(m_lineaPrefab, canvasRef);

        pLineaObj.tag = "Linea";

        RectTransform pRect = pLineaObj.GetComponent<RectTransform>();

        Vector3 diferencia = destino - origen;

        float distancia = diferencia.magnitude;

        Vector3 puntoMedio = (origen + destino) / 2f;

        pRect.position = puntoMedio;

        pRect.sizeDelta = new Vector2(distancia, m_grosor);

        pRect.pivot = new Vector2(0.5f, 0.5f);

        float angulo = Mathf.Atan2(diferencia.y, diferencia.x) * Mathf.Rad2Deg;

        pRect.rotation = Quaternion.Euler(0, 0, angulo);

        Transform panel = canvasRef.Find("Panel");

        if (panel != null)
        {
            pLineaObj.transform.SetSiblingIndex(panel.GetSiblingIndex() + 1);
        }
    }

    public void LimpiarLineas()
    {
        GameObject[] pLineas = GameObject.FindGameObjectsWithTag("Linea");

        foreach (GameObject pLinea in pLineas)
        {
            Destroy(pLinea);
        }
    }
}