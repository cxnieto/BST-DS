using UnityEngine;
using TMPro;



public class NodeVisual : MonoBehaviour
{
    

    public TextMeshPro m_texto;

    

    public void Inicializar(int valor, Vector3 posicion)
    {
        transform.position = posicion;  

        m_texto.text = valor.ToString(); 
    }
}