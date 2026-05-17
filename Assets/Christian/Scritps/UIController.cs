using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ----- UI CONTROLLER -----

public class UIController : MonoBehaviour
{
    // ----- REFERENCIAS -----

    //public BSTManager m_arbol;          
    public LineConnector m_lineConnector;  

    public GameObject m_nodoPrefab;     
    public TMP_InputField m_inputField;   
    public TextMeshProUGUI m_textoEstado; 

    // ----- BOTONES -----

    public void OnBotonInsertar()
    {
        string sTexto = m_inputField.text; 

        if (sTexto == "") 
        {
            m_textoEstado.text = "Escriba un numero.";
            return;
        }

        int iValor = int.Parse(sTexto); 

        //Vector3 posicion = m_arbol.ObtenerPosicion(iValor); 

        //bool bInsertado = m_arbol.Insertar(iValor, posicion); 

        //if (bInsertado == false) 
        {
            m_textoEstado.text = "Error: valor repetido o arbol lleno.";
            return;
        }

        GameObject pNodoObj = Instantiate(m_nodoPrefab); 

        NodeVisual pVisual = pNodoObj.GetComponent<NodeVisual>(); 
        //pVisual.Inicializar(iValor, posicion);                    

        //if (m_arbol.TienePadre(iValor)) 
        {
            //Vector3 posPadre = m_arbol.ObtenerPosicionPadre(iValor); 
           // m_lineConnector.ConectarNodos(posPadre, posicion);        
        }

        m_textoEstado.text = "Nodo " + iValor + " insertado."; 
        m_inputField.text = "";                                
    }

    public void OnBotonLimpiar()
    {
       // m_arbol.LimpiarArbol(); 

        m_lineConnector.LimpiarLineas(); 

        GameObject[] pNodos = GameObject.FindGameObjectsWithTag("Nodo"); 

        foreach (GameObject pNodo in pNodos) 
        {
            Destroy(pNodo); 
        }

        m_textoEstado.text = "Arbol limpiado."; 
    }
}