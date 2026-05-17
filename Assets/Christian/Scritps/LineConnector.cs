using UnityEngine;



public class LineConnector : MonoBehaviour
{
    

    public Material m_materialLinea;

    

    public void ConectarNodos(Vector3 origen, Vector3 destino)
    {
        GameObject pLineaObj = new GameObject("Linea"); 

        LineRenderer pLinea = pLineaObj.AddComponent<LineRenderer>(); 

        pLinea.material = m_materialLinea; 
        pLinea.startWidth = 0.05f;           
        pLinea.endWidth = 0.05f;           
        pLinea.positionCount = 2;               

        pLinea.SetPosition(0, origen);  
        pLinea.SetPosition(1, destino); 
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