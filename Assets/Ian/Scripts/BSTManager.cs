using System.Collections.Generic;
using UnityEngine;

public class BSTManager : MonoBehaviour
{
    // Clase interna para representar la estructura del nodo a nivel de lógica pura
    private class Node
    {
        public int value;
        public Vector3 position;
        public Node left;
        public Node right;
        public Node parent;

        public Node(int val, Vector3 pos, Node parentNode = null)
        {
            value = val;
            position = pos;
            left = null;
            right = null;
            parent = parentNode;
        }
    }

    private Node root;
    private int totalNodes = 0;

    [Header("Configuración de Espaciado Automático")]
    [SerializeField] private float horizontalSpacing = 3.5f;
    [SerializeField] private float verticalSpacing = 2.0f;
    [SerializeField] private Vector3 rootPosition = new Vector3(0, 4, 0);

    // Diccionario rápido para optimizar la búsqueda de posiciones requeridas por la UI
    private Dictionary<int, Node> nodeLookup = new Dictionary<int, Node>();

    /// <summary>
    /// Inserta un valor en el árbol binario de búsqueda respetando las reglas de ordenamiento, 
    /// no repetición y límite de 20 nodos. El parámetro 'posicion' puede pasarse desde la UI o calcularse automáticamente.
    /// </summary>
    public bool Insertar(int valor, Vector3 posicion)
    {
        // Regla 1: Validar límite de 20 nodos
        if (totalNodes >= 20)
        {
            Debug.LogError("Error: No se pueden agregar más de 20 nodos al árbol.");
            return false;
        }

        // Regla 2: Validar si el valor ya existe (No repetidos)
        if (nodeLookup.ContainsKey(valor))
        {
            Debug.LogError($"Error: El valor {valor} ya se encuentra repetido en el árbol.");
            return false;
        }

        // Si el árbol está vacío, creamos la raíz
        if (root == null)
        {
            // Si la posición externa que mandan es Zero, usamos la por defecto de la raíz
            Vector3 finalPos = (posicion == Vector3.zero) ? rootPosition : posicion;
            
            root = new Node(valor, finalPos);
            nodeLookup.Add(valor, root);
            totalNodes++;
            return true;
        }

        // De lo contrario, buscamos recursivamente dónde acomodarlo
        return InsertarRecursivo(root, valor, 1);
    }

    private bool InsertarRecursivo(Node current, int valor, int depth)
    {
        // Factor de reducción para que los nodos no se solapen en niveles profundos
        float currentOffset = horizontalSpacing / Mathf.Pow(1.5f, depth);

        if (valor < current.value)
        {
            if (current.left == null)
            {
                Vector3 newPos = current.position + new Vector3(-currentOffset, -verticalSpacing, 0);
                Node newNode = new Node(valor, newPos, current);
                
                current.left = newNode;
                nodeLookup.Add(valor, newNode);
                totalNodes++;
                return true;
            }
            return InsertarRecursivo(current.left, valor, depth + 1);
        }
        else // valor > current.value
        {
            if (current.right == null)
            {
                Vector3 newPos = current.position + new Vector3(currentOffset, -verticalSpacing, 0);
                Node newNode = new Node(valor, newPos, current);

                current.right = newNode;
                nodeLookup.Add(valor, newNode);
                totalNodes++;
                return true;
            }
            return InsertarRecursivo(current.right, valor, depth + 1);
        }
    }

    /// <summary>
    /// Devuelve la posición calculada en el espacio 3D de un nodo específico.
    /// </summary>
    public Vector3 ObtenerPosicion(int valor)
    {
        if (nodeLookup.TryGetValue(valor, out Node node))
        {
            return node.position;
        }
        
        Debug.LogWarning($"BSTManager: Intentando obtener posición de {valor} pero no existe.");
        return Vector3.zero;
    }

    /// <summary>
    /// Devuelve la posición en el espacio 3D del padre de un nodo específico.
    /// </summary>
    public Vector3 ObtenerPosicionPadre(int valor)
    {
        if (nodeLookup.TryGetValue(valor, out Node node) && node.parent != null)
        {
            return node.parent.position;
        }

        Debug.LogWarning($"BSTManager: {valor} no tiene padre o no existe.");
        return Vector3.zero;
    }

    /// <summary>
    /// Retorna true si el valor especificado tiene un nodo padre asignado.
    /// </summary>
    public bool TienePadre(int valor)
    {
        if (nodeLookup.TryGetValue(valor, out Node node))
        {
            return node.parent != null;
        }
        return false;
    }

    /// <summary>
    /// Resetea por completo la estructura lógica del árbol binario.
    /// </summary>
    public void LimpiarArbol()
    {
        root = null;
        totalNodes = 0;
        nodeLookup.Clear();
        Debug.Log("BSTManager: Lógica del árbol limpiada correctamente.");
    }
}
