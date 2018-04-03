using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/*
 * 
 * ESTE ES EL DEL PROYECTO A DONDE VA DIRIGIDO, NO LO OLVIDES
 * 
 * 
 */
public class DrawLine : MonoBehaviour
{
    public LineRenderer line;
    private bool isMousePressed;
    private List<Vector3> pointsList;
    private Vector3 mousePos;
    private bool dejarDeDibujar;
    public ChecadorLetras chL;

    

    // Structure for line points
    struct myLine
    {
        public Vector3 StartPoint;
        public Vector3 EndPoint;
    };
    //	-----------------------------------	
    void Awake()
    {
        LineRenderer line = gameObject.GetComponent("LineRenderer") as LineRenderer;

        if (line == null)
        {
            // Create line renderer component and set its property
            line = gameObject.AddComponent<LineRenderer>();

            //Debug.Log("Cuantas veces tiene que agregar el componente, sucede en: " + gameObject.name);
            
        }
        if (line != null)
        {
            //Debug.Log("Cuantas veces se inicializa las partes del componente, sucede en: " + gameObject.name);
            line.material = new Material(Shader.Find("Unlit/Color"));
            line.material.color = Color.blue;
            line.SetVertexCount(0);
            line.SetWidth(8f, 8f);
            line.SetColors(Color.red, Color.red);
            line.useWorldSpace = true;

            isMousePressed = false;
            dejarDeDibujar = true;
            pointsList = new List<Vector3>();
            //		renderer.material.SetTextureOffset(
        }

        

    }
    //	-----------------------------------	
    
    public void Dibujar(Vector3 mousePos)
    {
        //Debug.Log("Tratando de dibujar con el drawline: " + gameObject.name);
        if (dejarDeDibujar == false)
        {
            mousePos.x = mousePos.x + 2;
            mousePos.y = Screen.height - mousePos.y;
            mousePos.z = -15;
           // Debug.Log("MousePos en drawLine: " + mousePos);
            Vector3 a = GameObject.Find("Canvas").transform.TransformVector(mousePos);
            a.z = -15f;

            if (!pointsList.Contains(mousePos))
            {
                //Debug.Log("points list: " + pointsList.Count + " " +gameObject.name);
                line = gameObject.GetComponent("LineRenderer") as LineRenderer;
                pointsList.Add(mousePos);
                line.SetVertexCount(pointsList.Count);
               
                line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
                //if(isLineCollide())
                //{
                //	isMousePressed = false;
                //	line.SetColors(Color.red, Color.red);
                //}

            }
        }
        

    }

    public bool getDejarDeDibujar()
    {
        return dejarDeDibujar;
    }

    public void Alto(float tiempo)
    {
        Invoke("AltoD", tiempo);
    }

    public void AltoD()
    {
        dejarDeDibujar = true;
    }

    public void Seguir()
    {
        dejarDeDibujar = false;
        Debug.Log("El objeto: " + gameObject.name + "ya llamo a Seguir(), deberia de poder dibujar ahora");
    }

    public void Redo()
    {
        // If mouse button down, remove old line and set its color to green
        isMousePressed = true;
        line = gameObject.GetComponent("LineRenderer") as LineRenderer;
        line.SetVertexCount(0);
        pointsList.RemoveRange(0, pointsList.Count);
        line.SetColors(Color.green, Color.green);

    }

    public void mouseExit()
    {

        isMousePressed = false;

    }

    

    private void OnEnable()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("paso 2"))
        {
            chL.destroyDrawLines += destroyThisDrawLine;
        }
        
    }

    private void OnDisable()
    {

        Scene scene = SceneManager.GetActiveScene();
        try
        {
            if (scene.name.Equals("paso 2"))
            {
                chL.destroyDrawLines -= destroyThisDrawLine;
            }
        } catch(UnityException e)
        {
            Debug.Log(e.StackTrace);
        }
        
        
    }

    private void destroyThisDrawLine()
    {
        Destroy(gameObject);
    }
}