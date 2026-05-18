using UnityEngine;
using TMPro;

public class NodeVisual : MonoBehaviour
{
    public TextMeshProUGUI texto;

    public void Inicializar(int valor, Vector3 posicion)
    {
        gameObject.tag = "Nodo";
        transform.position = posicion;

        if (texto != null)
            texto.text = valor.ToString();
    }
}