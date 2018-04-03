
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controlReporte : MonoBehaviour
{


    int inicial = 0;
    public GameObject panelPregunta2;
    List<Reporte> reportesObj = new List<Reporte>();
    double porcentaje = 0;
    List<string> nombres = new List<string>();
    List<string> fechas = new List<string>();
    List<string> temas = new List<string>();
    List<string> figuras = new List<string>();
    List<int> niveles = new List<int>();
    List<string> turnos = new List<string>();
    List<double> tiempos = new List<double>();
    List<string> paths = new List<string>();
    string[] divisionNombre = new string[140];
    string[] divisionFecha = new string[140];
    string[] divisionTema = new string[140];
    string[] divisionFigura = new string[140];
    string[] divisionNivel = new string[140];
    string[] divisionTurno = new string[140];
    string[] divisionTiempo = new string[140];
    int inicio = 0;
    string cadenaNombres;
    string cadenaFechas;
    string cadenaTemas;
    string cadenaFiguras;
    string cadenaNiveles;
    string cadenaTurnos;
    string cadenaTiempos;
    int maximo = 0;
    int maximoTiempo = 0;
    int contFechas = 0;
    int indice = 0;
    int contFechas2 = 0;
    int cantidadIntentos = 0;
    int index;
    int situacion = 0;
    // Use this for initialization

    void Start()
    {
        EstadoJuego.estadoJuego.cargarNinios(GameObject.Find("DropDownListNinios").GetComponent<Dropdown>());
        EstadoJuego.estadoJuego.cargarTemas(GameObject.Find("DropDownListTemas").GetComponent<Dropdown>());
        for (int x = 0; x < 10; x++)
        {
            GameObject.Find("btnBoton" + x.ToString()).GetComponent<Button>().enabled = false;
            GameObject.Find("btnBoton" + x.ToString()).transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void botonClick()
    {
        GetComponent<AudioSource>().Play();
        for (int x = 0; x < 10; x++)
        {
            GameObject.Find("btnBoton" + x.ToString()).GetComponent<Button>().enabled = false;
            GameObject.Find("btnBoton" + x.ToString()).transform.localScale = new Vector3(0, 0, 0);
        }
        reportesObj = new List<Reporte>();
        nombres = new List<string>();
        fechas = new List<string>();
        temas = new List<string>();
        figuras = new List<string>();
        niveles = new List<int>();
        turnos = new List<string>();
        tiempos = new List<double>();
        paths = new List<string>();
        divisionNombre = new string[140];
        divisionFecha = new string[140];
        divisionTema = new string[140];
        divisionFigura = new string[140];
        divisionNivel = new string[140];
        divisionTurno = new string[140];
        divisionTiempo = new string[140];
        inicio = 0;
        cadenaNombres = "";
        cadenaFechas = "";
        cadenaTemas = "";
        cadenaFiguras = "";
        cadenaNiveles = "";
        cadenaTurnos = "";
        cadenaTiempos = "";
        maximo = 0;
        maximoTiempo = 0;
        contFechas = 0;
        indice = 0;
        contFechas2 = 0;
        porcentaje = 0;
        situacion = 0;

        int index = GameObject.Find("DropDownListFiguras").GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions = GameObject.Find("DropDownListFiguras").GetComponent<Dropdown>().options;
        int index2 = GameObject.Find("DropDownListNiveles").GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions2 = GameObject.Find("DropDownListNiveles").GetComponent<Dropdown>().options;
        int index3 = GameObject.Find("DropDownListTurno").GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions3 = GameObject.Find("DropDownListTurno").GetComponent<Dropdown>().options;
        int index4 = GameObject.Find("DropDownListNinios").GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions4 = GameObject.Find("DropDownListNinios").GetComponent<Dropdown>().options;
        int index5 = GameObject.Find("DropDownListSesiones").GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions5 = GameObject.Find("DropDownListSesiones").GetComponent<Dropdown>().options;
        int index6 = GameObject.Find("DropDownListTemas").GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuOptions6 = GameObject.Find("DropDownListTemas").GetComponent<Dropdown>().options;

        if (index == 0 && index2 == 0 && index3 == 0 && index4 == 0 && index5 == 0 && index6 == 0)
        {
            GameObject.Find("txtNinios").GetComponent<Text>().text = "";
            GameObject.Find("txtFechas").GetComponent<Text>().text = "";
            GameObject.Find("txtTemas").GetComponent<Text>().text = "";
            GameObject.Find("txtFiguras").GetComponent<Text>().text = "";
            GameObject.Find("txtNiveles").GetComponent<Text>().text = "";
            GameObject.Find("txtTurnos").GetComponent<Text>().text = "";
            GameObject.Find("txtDuracion").GetComponent<Text>().text = "";
            GameObject.Find("txtPorcentajeExito").GetComponent<Text>().text = "";
            GameObject.Find("txtFin").GetComponent<Text>().text = "";
            GameObject.Find("txtInicio").GetComponent<Text>().text = "";
            GameObject.Find("txtFinal").GetComponent<Text>().text = "";
            GameObject.Find("txtCantidadIntentos").GetComponent<Text>().text = "";
            return;
        }

        if (index4 != 0 && index == 0 && index2 == 0 && index3 == 0 && index5 == 0 && index6 == 0)
        {
            reportesObj = EstadoJuego.estadoJuego.cargarReporteDelNinio(index4);
            porcentaje = EstadoJuego.estadoJuego.calcularPorcentaje(index4);
            cantidadIntentos = EstadoJuego.estadoJuego.numeroDeIntentos(index4);
            cargarReporte(reportesObj, porcentaje, cantidadIntentos);

        }
        else if (index4 != 0 && index == 0 && index2 == 0 && index3 == 0 && index5 == 0 && index6 != 0)
        {
            string tema = menuOptions6[index6].text;
            reportesObj = EstadoJuego.estadoJuego.cargarReporteDelNinioTema(index4, tema);
            porcentaje = EstadoJuego.estadoJuego.calcularPorcentajeNinioTema(index4, tema);
            cantidadIntentos = EstadoJuego.estadoJuego.numeroDeIntentosNinioTema(index4, tema);
            cargarReporte(reportesObj, porcentaje, cantidadIntentos);
        }
        else if (index4 != 0 && index != 0 && index2 == 0 && index3 == 0 && index5 == 0 && index6 != 0)
        {
            string tema = menuOptions6[index6].text;
            string figura = menuOptions[index].text;
            reportesObj = EstadoJuego.estadoJuego.cargarReporteDelNinioTemaFigura(index4, tema, figura);
            porcentaje = EstadoJuego.estadoJuego.calcularPorcentajeNinioTemaFigura(index4, tema, figura);
            cantidadIntentos = EstadoJuego.estadoJuego.numeroDeIntentosNinioTemaFigura(index4, tema, figura);
            cargarReporte(reportesObj, porcentaje, cantidadIntentos);

        }
        else if (index4 != 0 && index != 0 && index2 != 0 && index3 == 0 && index5 == 0 && index6 != 0)
        {
            string tema = menuOptions6[index6].text;
            string figura = menuOptions[index].text;
            string nivel = menuOptions2[index2].text;
            int nivel2 = Int32.Parse(nivel);
            reportesObj = EstadoJuego.estadoJuego.cargarReporteDelNinioTemaFiguraNivel(index4, tema, figura, nivel2);
            porcentaje = EstadoJuego.estadoJuego.calcularPorcentajeNinioTemaFiguraNivel(index4, tema, figura, nivel2);
            cantidadIntentos = EstadoJuego.estadoJuego.numeroDeIntentosNinioTemaFiguraNivel(index4, tema, figura, nivel2);
            cargarReporte(reportesObj, porcentaje, cantidadIntentos);

        }
        else
        if (index4 != 0 && index != 0 && index2 != 0 && index3 != 0 && index5 == 0 && index6 != 0)
        {
            string tema = menuOptions6[index6].text;
            string figura = menuOptions[index].text;
            string nivel = menuOptions2[index2].text;
            int nivel2 = Int32.Parse(nivel);
            string turno = menuOptions3[index3].text;
            reportesObj = EstadoJuego.estadoJuego.cargarReporteDelNinioTemaFiguraNivelTurno(index4, tema, figura, nivel2, turno);
            porcentaje = EstadoJuego.estadoJuego.calcularPorcentajeNinioTemaFiguraNivelTurno(index4, tema, figura, nivel2, turno);
            cantidadIntentos = EstadoJuego.estadoJuego.numeroDeIntentosNinioTemaFiguraNivelTurno(index4, tema, figura, nivel2, turno);
            cargarReporte(reportesObj, porcentaje, cantidadIntentos);

        }
        else if (index4 != 0 && index == 0 && index2 == 0 && index3 == 0 && index5 != 0 && index6 == 0)
        {
            string fecha = menuOptions5[index5].text;
            reportesObj = EstadoJuego.estadoJuego.cargarReporteDelNinioFecha(index4, fecha);
            porcentaje = EstadoJuego.estadoJuego.calcularPorcentajeNinioFecha(index4, fecha);
            cantidadIntentos = EstadoJuego.estadoJuego.numeroDeIntentosNinioFecha(index4, fecha);
            cargarReporte(reportesObj, porcentaje, cantidadIntentos);

        }
        else
        {
            GameObject.Find("txtNinios").GetComponent<Text>().text = "";
            GameObject.Find("txtFechas").GetComponent<Text>().text = "";
            GameObject.Find("txtTemas").GetComponent<Text>().text = "";
            GameObject.Find("txtFiguras").GetComponent<Text>().text = "";
            GameObject.Find("txtNiveles").GetComponent<Text>().text = "";
            GameObject.Find("txtTurnos").GetComponent<Text>().text = "";
            GameObject.Find("txtDuracion").GetComponent<Text>().text = "";
            GameObject.Find("txtPorcentajeExito").GetComponent<Text>().text = "";
            GameObject.Find("txtFin").GetComponent<Text>().text = "";
            GameObject.Find("txtInicio").GetComponent<Text>().text = "";
            GameObject.Find("txtFinal").GetComponent<Text>().text = "";
            GameObject.Find("txtCantidadIntentos").GetComponent<Text>().text = "";
            GameObject.Find("DropDownListNinios").GetComponent<Dropdown>().value = 0;
            GameObject.Find("DropDownListTemas").GetComponent<Dropdown>().value = 0;
            GameObject.Find("DropDownListFiguras").GetComponent<Dropdown>().value = 0;
            GameObject.Find("DropDownListNiveles").GetComponent<Dropdown>().value = 0;
            GameObject.Find("DropDownListTurno").GetComponent<Dropdown>().value = 0;
            GameObject.Find("DropDownListSesiones").GetComponent<Dropdown>().value = 0;

        }


    }

    public void cargarReporte(List<Reporte> reportesObj, double porcentaje, int cantidadIntentos)
    {

        double duracion;
        inicial = 1;
        if (reportesObj.Count == 0)
        {
            Debug.Log(reportesObj.Count);
            this.panelPregunta2.SetActive(true);
            GameObject.Find("txtNinios").GetComponent<Text>().text = "";
            GameObject.Find("txtFechas").GetComponent<Text>().text = "";
            GameObject.Find("txtTemas").GetComponent<Text>().text = "";
            GameObject.Find("txtFiguras").GetComponent<Text>().text = "";
            GameObject.Find("txtNiveles").GetComponent<Text>().text = "";
            GameObject.Find("txtTurnos").GetComponent<Text>().text = "";
            GameObject.Find("txtDuracion").GetComponent<Text>().text = "";
            GameObject.Find("txtPorcentajeExito").GetComponent<Text>().text = "";
            GameObject.Find("txtFin").GetComponent<Text>().text = "";
            GameObject.Find("txtInicio").GetComponent<Text>().text = "";
            GameObject.Find("txtFinal").GetComponent<Text>().text = "";
            GameObject.Find("txtCantidadIntentos").GetComponent<Text>().text = "";
        }
        else
        {
            foreach (Reporte reporte in reportesObj)
            {
                nombres.Add(reporte.nombre);
            }

            foreach (Reporte reporte in reportesObj)
            {
                fechas.Add(reporte.fecha);
            }

            foreach (Reporte reporte in reportesObj)
            {
                temas.Add(reporte.tema);
            }

            foreach (Reporte reporte in reportesObj)
            {
                figuras.Add(reporte.figura);
            }

            foreach (Reporte reporte in reportesObj)
            {
                niveles.Add(reporte.nivel);
            }

            foreach (Reporte reporte in reportesObj)
            {
                turnos.Add(reporte.turno);
            }

            foreach (Reporte reporte in reportesObj)
            {
                duracion = Math.Round(reporte.duracion, 2);
                tiempos.Add(duracion);
            }
            Debug.Log(tiempos.Count);
            foreach (Reporte reporte in reportesObj)
            {
                if (!paths.Contains(reporte.path))
                {
                    paths.Add(reporte.path);
                }
            }

            string[] nombre = nombres.ToArray();

            string[] fecha = fechas.ToArray();

            string[] tema = temas.ToArray();

            string[] figura = figuras.ToArray();

            int[] nivel = niveles.ToArray();

            string[] turno = turnos.ToArray();

            double[] tiempo = tiempos.ToArray();

            string[] path = paths.ToArray();

            while (contFechas < fecha.Length)
            {
                cadenaFechas = "";
                cadenaTiempos = "";
                maximo += 10;
                while (contFechas < maximo)
                {
                    if (contFechas == maximo - 10)
                    {
                        cadenaNombres = nombre[contFechas];
                        cadenaFechas = fecha[contFechas];
                        cadenaTemas = tema[contFechas];
                        cadenaFiguras = figura[contFechas];
                        cadenaNiveles = nivel[contFechas].ToString();
                        cadenaTurnos = turno[contFechas];
                        cadenaTiempos = tiempo[contFechas].ToString();
                        contFechas++;
                    }
                    else
                    {
                        if (contFechas == fecha.Length)
                        {
                            break;
                        }
                        else
                        {
                            cadenaNombres += "\n\n" + nombre[contFechas];
                            cadenaFechas += "\n\n" + fecha[contFechas];
                            cadenaTemas += "\n\n" + tema[contFechas];
                            cadenaFiguras += "\n\n" + figura[contFechas];
                            cadenaNiveles += "\n\n" + nivel[contFechas].ToString(); ;
                            cadenaTurnos += "\n\n" + turno[contFechas];
                            Debug.Log(cadenaFechas);
                            cadenaTiempos += "\n\n" + tiempo[contFechas].ToString();
                            contFechas++;
                        }
                    }

                }
                Debug.Log(cadenaFechas);
                //Aqui se guardan en partes de 10 en 10
                divisionNombre[contFechas2] = cadenaNombres;
                divisionFecha[contFechas2] = cadenaFechas;
                divisionTema[contFechas2] = cadenaTemas;
                divisionFigura[contFechas2] = cadenaFiguras;
                divisionNivel[contFechas2] = cadenaNiveles;
                divisionTurno[contFechas2] = cadenaTurnos;
                divisionTiempo[contFechas2] = cadenaTiempos;
                contFechas2++;
            }
            GameObject.Find("txtFechas").GetComponent<Text>().text = "";
            GameObject.Find("txtDuracion").GetComponent<Text>().text = "";
            GameObject.Find("txtPorcentajeExito").GetComponent<Text>().text = "";
            GameObject.Find("txtNinios").GetComponent<Text>().text = divisionNombre[0];
            GameObject.Find("txtFechas").GetComponent<Text>().text = divisionFecha[0];
            GameObject.Find("txtTemas").GetComponent<Text>().text = divisionTema[0];
            GameObject.Find("txtFiguras").GetComponent<Text>().text = divisionFigura[0];
            GameObject.Find("txtNiveles").GetComponent<Text>().text = divisionNivel[0];
            GameObject.Find("txtTurnos").GetComponent<Text>().text = divisionTurno[0];
            GameObject.Find("txtDuracion").GetComponent<Text>().text = divisionTiempo[0];
            GameObject.Find("txtPorcentajeExito").GetComponent<Text>().text = porcentaje.ToString();
            GameObject.Find("txtCantidadIntentos").GetComponent<Text>().text = cantidadIntentos.ToString();
            GameObject.Find("txtFin").GetComponent<Text>().text = fechas.Count.ToString();
            GameObject.Find("txtInicio").GetComponent<Text>().text = (inicial).ToString();
            GameObject.Find("txtFinal").GetComponent<Text>().text = (contFechas2).ToString();

            if (fechas.Count > 10)
            {
                for (int x = 0; x < 10; x++)
                {
                    GameObject.Find("btnBoton" + x.ToString()).GetComponent<Button>().enabled = true;
                    GameObject.Find("btnBoton" + x.ToString()).transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                for (int x = 0; x < fechas.Count; x++)
                {
                    GameObject.Find("btnBoton" + x.ToString()).GetComponent<Button>().enabled = true;
                    GameObject.Find("btnBoton" + x.ToString()).transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    public void cambiarDivision(int indice)
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Indice" + this.indice + "tamaño:" + divisionFecha.Length);
        if ((this.indice < contFechas2 - 1) && (this.indice + indice) > 0 && indice > 0)
        {
            this.indice += indice;
            inicial += 1;
            GameObject.Find("txtInicio").GetComponent<Text>().text = (inicial).ToString();
            GameObject.Find("txtNinios").GetComponent<Text>().text = divisionNombre[this.indice];
            GameObject.Find("txtFechas").GetComponent<Text>().text = divisionFecha[this.indice];
            GameObject.Find("txtTemas").GetComponent<Text>().text = divisionTema[this.indice];
            GameObject.Find("txtFiguras").GetComponent<Text>().text = divisionFigura[this.indice];
            GameObject.Find("txtNiveles").GetComponent<Text>().text = divisionNivel[this.indice];
            GameObject.Find("txtTurnos").GetComponent<Text>().text = divisionTurno[this.indice];
            GameObject.Find("txtDuracion").GetComponent<Text>().text = divisionTiempo[this.indice];

        }
        else if ((this.indice + indice < contFechas2 - 1) && (this.indice + indice) >= 0 && indice <= 0)
        {
            this.indice += indice;
            inicial -= 1;
            if ((inicial) == 0)
            {
                inicial = 1;
                GameObject.Find("txtInicio").GetComponent<Text>().text = (inicial).ToString();
                GameObject.Find("txtNinios").GetComponent<Text>().text = divisionNombre[this.indice];
                GameObject.Find("txtFechas").GetComponent<Text>().text = divisionFecha[this.indice];
                GameObject.Find("txtTemas").GetComponent<Text>().text = divisionTema[this.indice];
                GameObject.Find("txtFiguras").GetComponent<Text>().text = divisionFigura[this.indice];
                GameObject.Find("txtNiveles").GetComponent<Text>().text = divisionNivel[this.indice];
                GameObject.Find("txtTurnos").GetComponent<Text>().text = divisionTurno[this.indice];
                GameObject.Find("txtDuracion").GetComponent<Text>().text = divisionTiempo[this.indice];
            }
            else
            {

                GameObject.Find("txtInicio").GetComponent<Text>().text = (inicial).ToString();
                GameObject.Find("txtNinios").GetComponent<Text>().text = divisionNombre[this.indice];
                GameObject.Find("txtFechas").GetComponent<Text>().text = divisionFecha[this.indice];
                GameObject.Find("txtTemas").GetComponent<Text>().text = divisionTema[this.indice];
                GameObject.Find("txtFiguras").GetComponent<Text>().text = divisionFigura[this.indice];
                GameObject.Find("txtNiveles").GetComponent<Text>().text = divisionNivel[this.indice];
                GameObject.Find("txtTurnos").GetComponent<Text>().text = divisionTurno[this.indice];
                GameObject.Find("txtDuracion").GetComponent<Text>().text = divisionTiempo[this.indice];
            }


        }
    }

    public void seleccionarPalabra(string nombre)
    {
        Debug.Log(nombre);
        int valor = GameObject.Find(nombre).GetComponent<Dropdown>().value;
        string valorReal = GameObject.Find(nombre).GetComponent<Dropdown>().options[valor].text;
        EstadoJuego.estadoJuego.cargarLista(valorReal, (GameObject.Find("DropDownListFiguras").GetComponent<Dropdown>()));
    }

    public void seleccionarFecha(string nombre)
    {
        int valor = GameObject.Find(nombre).GetComponent<Dropdown>().value;
        string valorReal = GameObject.Find(nombre).GetComponent<Dropdown>().options[valor].text;
        EstadoJuego.estadoJuego.cargarFechas(valor, (GameObject.Find("DropDownListSesiones").GetComponent<Dropdown>()));
    }

    public void cargarImagen(int indiceBoton)
    {
        indiceBoton = indice == 0 ? indiceBoton : (indice) * 10 + indiceBoton;
        if (indiceBoton < paths.Count)
        {
            GetComponent<AudioSource>().Play();
            Debug.Log(Application.streamingAssetsPath + "/Screenshots");
            String itemPath = Application.streamingAssetsPath + "/Screenshots/" + paths[indiceBoton];
            itemPath = itemPath.Replace(@"/", @"\");   // explorer doesn't like front slashes
            System.Diagnostics.Process.Start("explorer.exe", "/select/open," + itemPath);
        }
    }

    public void pasar()
    {
        this.panelPregunta2.SetActive(false);
    }
}


