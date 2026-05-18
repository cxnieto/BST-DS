using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public BSTManager m_arbol;
    public LineConnector m_lineConnector;

    public GameObject m_nodoPrefab;
    public TMP_InputField m_inputField;
    public TextMeshProUGUI m_textoEstado;

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

        m_textoEstado.text = "Arbol limpiado.";
    }

    private void RedibujarArbol()
    {
        GameObject[] pNodos = GameObject.FindGameObjectsWithTag("Nodo");
        foreach (GameObject pNodo in pNodos)
        {
            Destroy(pNodo);
        }

        DibujarNodos(m_arbol);

        m_lineConnector.RedibujarTodo(m_arbol.ObtenerConexiones());
    }

    private void DibujarNodos(BSTManager arbol)
    {
        foreach (int valor in arbol.ObtenerTodosLosValores())
        {
            Vector3 pos = arbol.ObtenerPosicion(valor);

            GameObject pNodoObj = Instantiate(m_nodoPrefab);
            NodeVisual pVisual = pNodoObj.GetComponent<NodeVisual>();
            pVisual.Inicializar(valor, pos);
        }
    }
}