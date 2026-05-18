using UnityEngine;
using System.Collections.Generic;

public class LineConnector : MonoBehaviour
{
    public Material m_materialLinea;

    public void ConectarNodos(Vector3 origen, Vector3 destino)
    {
        GameObject pLineaObj = new GameObject("Linea");
        pLineaObj.tag = "Linea";

        LineRenderer pLinea = pLineaObj.AddComponent<LineRenderer>();

        pLinea.material = m_materialLinea;
        pLinea.startWidth = 0.05f;
        pLinea.endWidth = 0.05f;
        pLinea.positionCount = 2;
        pLinea.sortingOrder = -1;

        pLinea.SetPosition(0, origen);
        pLinea.SetPosition(1, destino);
    }

    public void RedibujarTodo(List<Vector3[]> conexiones)
    {
        LimpiarLineas();

        foreach (Vector3[] par in conexiones)
        {
            ConectarNodos(par[0], par[1]);
        }
    }

    public void LimpiarLineas()
    {
        GameObject[] lineas = GameObject.FindGameObjectsWithTag("Linea");

        foreach (GameObject l in lineas)
        {
            Destroy(l);
        }
    }
}