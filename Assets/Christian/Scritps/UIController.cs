using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public BSTManager m_arbol;
    public LineConnector m_lineConnector;

    public GameObject canvasRef;
    public GameObject m_nodoPrefab;
    public TMP_InputField m_inputField;
    public TextMeshProUGUI m_textoEstado;

    private List<int> m_valoresInsertados = new List<int>();

    public void OnBotonAgregar()
    {
        string sTexto = m_inputField.text;

        if (sTexto == "")
        {
            m_textoEstado.text = "Escriba un numero.";
            return;
        }

        int iValor;

        if (!int.TryParse(sTexto, out iValor))
        {
            m_textoEstado.text = "Solo se permiten numeros enteros.";
            return;
        }

        bool bInsertado = m_arbol.Insertar(iValor, Vector3.zero);

        if (bInsertado == false)
        {
            m_textoEstado.text = "Error: valor repetido o arbol lleno.";
            m_inputField.text = "";
            return;
        }

        m_valoresInsertados.Add(iValor);

        RedibujarArbol();

        m_textoEstado.text = "Nodo " + iValor + " insertado.";
        m_inputField.text = "";
    }

    public void OnBotonLimpiar()
    {
        m_arbol.LimpiarArbol();

        m_lineConnector.LimpiarLineas();

        GameObject[] pNodos = GameObject.FindGameObjectsWithTag("Nodo");

        foreach (GameObject pNodo in pNodos)
        {
            Destroy(pNodo);
        }

        m_valoresInsertados.Clear();

        m_textoEstado.text = "Arbol limpiado.";
    }

    private void RedibujarArbol()
    {
        GameObject[] pNodos = GameObject.FindGameObjectsWithTag("Nodo");

        foreach (GameObject pNodo in pNodos)
        {
            Destroy(pNodo);
        }

        m_lineConnector.LimpiarLineas();

        foreach (int valor in m_valoresInsertados)
        {
            Vector3 pos = m_arbol.ObtenerPosicion(valor);

            GameObject pNodoObj = Instantiate(m_nodoPrefab, canvasRef.transform);

            NodeVisual pVisual = pNodoObj.GetComponent<NodeVisual>();

            pVisual.Inicializar(valor, pos);

            if (m_arbol.TienePadre(valor))
            {
                Vector3 parentPos = m_arbol.ObtenerPosicionPadre(valor);

                m_lineConnector.CrearLinea(parentPos, pos);
            }
        }
    }
}