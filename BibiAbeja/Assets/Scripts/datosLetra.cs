using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class datosLetra : MonoBehaviour
{
    public GameObject[] puntos;
    public GameObject[] flechas;
    public string tipoLetra;
    public int puntoIdEspecial;
    public Sprite newSprite;
    private SpriteRenderer mySpriteRenderer;
    Material m;

    //indice de flechas
    int i = -1;

    //indice de puntos
    int j = 0;

    //indice de veces llamadas a "desactivar todo"
    int t = 0;

    // Use this for initialization
    void Start()
    {
        
        m = Resources.Load("Materials/punto") as Material;
        desactivarTodo();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //newSprite = Resources.Load<Sprite>("letra" + nombreLetra) as Sprite;
        if (tipoLetra.Equals("unTrazo"))
        {
            puntoIdEspecial = 0;
        }
        if (mySpriteRenderer == null)
        {
            Debug.Log("sprite renderer null");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void desactivarTodo()
    {
        
        for (int i = 0; i < puntos.Length; i++)
        {
            if (i == (puntos.Length - 1) && t > 0)
            {
                //Este pequeño cambio es para hacer trampa y cuando el niño termine termine la letra
                //I o la letra J, el punto se coloree de azul, pues no encuentro otra manera
                //de dibujar ese punto
                if (newSprite.name.Equals("letraJ Roja") || newSprite.name.Equals("letraI Roja"))
                {
                    puntos[i].GetComponent<MeshRenderer>().materials[0].color = Color.blue;
                    puntos[i].name = "boton azul";
                }else
                {
                    puntos[i].SetActive(false);
                }
            }
            else
            {
                puntos[i].SetActive(false);
            }
            
        }
        for (int j = 0; j < flechas.Length; j++)
        {
            flechas[j].SetActive(false);
        }
        t++;
    }

    //Primera activación
    public void primeraActivacion()
    {
        puntos[0].SetActive(true);
    }


    //estoy tratando de ya no usar esto
    public void activarPuntos()
    {
        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i].SetActive(true);
        }

    }

    public void activarSiguientePunto()
    {
        j++;
        if (j < puntos.Length)
        {
            puntos[j].SetActive(true);
            puntos[j].GetComponent<Puntos>().PSDisableEmission();
        }

    }

    public void activarSiguienteFlecha()
    {
        i++;
        if (i < flechas.Length)
        {
            flechas[i].SetActive(true);
        }
    }

    //modificar esto con el reintento nuevo, creo que ya
    public void reintento()
    {
        i = -1;
        j = 0;
        Debug.Log("cantidad de flechas: " + flechas.Length);
        Debug.Log("cantidad de puntos " + puntos.Length);

        for (int f = 0; f < flechas.Length ; f++)
        {
            //if (flechas[f].activeSelf == true)
            //{
            //Debug.Log("quitare la flecha en " + f);
            flechas[f].SetActive(false);
                
            //}

        }
        for (int p = 1; p < puntos.Length; p++)
        {
            //if (puntos[p].activeSelf == true)
            //{
            puntos[p].GetComponent<MeshRenderer>().material = m;
                puntos[p].SetActive(false);
               // Debug.Log("quite el punto en " + p);
            //}

        }
    }

    public void primerPunto(bool tocado)
    {
        if (tocado)
        {
            Debug.Log("Desactivando emision:");
            puntos[0].GetComponent<Puntos>().PSDisableEmission();
        }
        else
        {

            Debug.Log("Activando emision:");
            puntos[0].GetComponent<Puntos>().PSEnableEmission();
        }
    }

    public void colorearRojo()
    {
        mySpriteRenderer.sprite = newSprite;
        Debug.Log("path del sprite rojo: " + newSprite);
    }
}
