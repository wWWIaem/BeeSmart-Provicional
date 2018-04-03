using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class EstadoJuego : MonoBehaviour
{
    public Dropdown miMenu;
    public static EstadoJuego estadoJuego;
    public List<Player> players;
    public Player ActivePlayer;
    public int nivel;
    public string tema;
    public string palabra;
    public List<String> Palabras;
    public bool menuCargado = false;
    public Boolean exitoso;
    public string path = "";
    public PNGUploader uploader;
    private int numIntentosActual;
    private int numIntentos;


    public conexionDB conexion;

    public int NumIntentosActual { get { return numIntentosActual; } set { numIntentosActual = value; } }
    public int NumIntentos { get { return numIntentos; } set { numIntentos = value; } }

    void Awake()
    {

        //Screen.SetResolution(1920, 1080, true);

        if (estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoJuego != this)
        {

            Destroy(gameObject);
        }
        nivel = 1;
        players = new List<Player>();
        conexion = new conexionDB();
        cargarJugadores();
    }
    // Use this for initialization
    void Start()
    {
        Debug.Log(System.DateTime.Now.ToString());
        //this.uploader = Resources.Load("/Prefabs/pngUploader") as PNGUploader;
    }

    // Update is called once per frame
    void Update()
    {
        this.ActivePlayer.name = this.ActivePlayer.name.Trim();
        string nameScene = SceneManager.GetActiveScene().name;

        if (nameScene.Equals("seleccionarUsuario") && players.Count > 0)
        {
            GameObject.Find("nNino").GetComponent<Text>().text = this.ActivePlayer.name;
        }
        else if (nameScene.Equals("seleccionarUsuario"))
        {
            GameObject.Find("nNino").GetComponent<Text>().text = "no hay usuarios";
        }
        else if (nameScene.Equals("eligeTema"))
        {
            GameObject.Find("nNino").GetComponent<Text>().text = this.ActivePlayer.name;
            GameObject.Find("txtNivel").GetComponent<Text>().text = "Nivel " + nivel;
            cargarMenus();
        }
        else if (nameScene.Equals("paso 1") || nameScene.Equals("paso 2") || nameScene.Equals("paso 3") || nameScene.Equals("paso 4"))
        {
            string nombre = ActivePlayer.name.ToString();
            if (nameScene.Equals("paso 1") || nameScene.Equals("paso 2"))
            {
                GameObject.Find("txtPalabra").GetComponent<Text>().text = this.palabra;
            }

            GameObject.Find("nNino").GetComponent<Text>().text = nombre;
        }


    }


    public void establecerImagenActiva(bool borrarIntento)
    {
        Imagen ultimaImg = conexion.obtenerUltimaImgDesbloqueada(this.ActivePlayer.id, this.palabra);
        if (!borrarIntento)
        {
            conexion.borrarImgIncompleta(ultimaImg.Id);
        }
        else
        {
            this.NumIntentos = ultimaImg.Divisor;
            this.NumIntentosActual = ultimaImg.NumCompletado;
        }


    }


    public int obtenerultimaIndiceImg()
    {
        int id = this.conexion.obtenerUltimoIndiceImg(this.ActivePlayer.id, this.palabra);
        Debug.Log("indice" + id);
        return id < 0 ? 1 : id;
    }



    public void registrarIntento(float duracion, int exitoso)
    {

        if (exitoso == 1)
        {

            guardarImagenDesbloqueada();
        }
        int indiceM = conexion.obtenerIndiceMaximo(this.ActivePlayer.id, this.palabra, this.nivel);
        this.path = nivel <= 2 ? this.ActivePlayer.id + "_" + this.palabra + "_" + (indiceM + 1) + "_" + "Nivel" + this.nivel + ".png" : "";
        // Debug.Log(this.ActivePlayer.id + this.palabra + duracion + path + exitoso);

        int playerActive = ActivePlayer.id;


        String[] datos = this.obtenerSilabasPalabra(System.DateTime.Now.ToString());


        conexion.guardarIntento(this.ActivePlayer.id, this.palabra, duracion, path, exitoso, this.nivel, datos);

        if (nivel == 1 || nivel == 2)
        {
            uploader.StartCoroutine("UploadPNG");
        }
    }


    void cargarMenus()
    {

        cargarLista("geometria", GameObject.Find("btnFiguras").GetComponent<Dropdown>());
        //cargarLista("frutas", GameObject.Find("btnFrutas").GetComponent<Dropdown>());
        //cargarLista("mediosTransporte", GameObject.Find("btnMediosTransporte").GetComponent<Dropdown>());
        cargarLista("animales", GameObject.Find("btnAnimales").GetComponent<Dropdown>());
        cargarLista("hogar", GameObject.Find("btnHogar").GetComponent<Dropdown>());
        cargarLista("colores", GameObject.Find("btnColores").GetComponent<Dropdown>());

        //menús nuevos, solo falta cambiar el botón
        cargarLista("partesDelCuerpo", GameObject.Find("btnPartesDelCuerpo").GetComponent<Dropdown>());
        cargarLista("sentidos", GameObject.Find("btnSentidos").GetComponent<Dropdown>());

        menuCargado = true;

    }





    public void cambiarUsuarioActivo(int i)
    {

        if (players.Count == 1 || players.Count == 0)
        { }
        else if ((this.ActivePlayer.id < players.Count) && (this.ActivePlayer.id + i) > 0 && i > 0)
        {
            Debug.Log("der" + this.ActivePlayer.id + i);
            this.ActivePlayer = players.Find(item => item.id == this.ActivePlayer.id + i);

        }
        else if ((this.ActivePlayer.id + i < players.Count) && (this.ActivePlayer.id + i) > 0 && i < 0)
        {
            Debug.Log("der" + this.ActivePlayer.id + i);
            this.ActivePlayer = players.Find(item => item.id == this.ActivePlayer.id + i);

        }


    }

    public void setUsuario(int i)
    {
        this.ActivePlayer = players.Find(item => item.id == i);

    }




    public void guardarJugador()
    {
        //Player ActivePlayer = new Player();
        //players.Add(ActivePlayer);
        //players.RemoveAt(players.IndexOf(ActivePlayer));
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(rutaArchivo);
        //bf.Serialize(file, players);
        //file.Close();
        //cargarJugadores();
    }

    public void guardarNuevo(String nombre)
    {
        int index = players.Count == 0 ? 0 : players.Count + 1;

        this.ActivePlayer = new Player(index, nombre);
        players.Add(ActivePlayer);
        Debug.Log(ActivePlayer.name + " id: " + ActivePlayer.id);
        conexion.guardarNuevo(nombre);
    }



    public void setIntentos(int divisor)
    {

        this.numIntentos = divisor == 4 ? 6 : divisor == 3 ? 4 : divisor == 2 ? 2 : divisor == 1 ? 1 : divisor == 0 ? 0 : 0;

        //Debug.Log(numIntentos+ "<---------- intentos");

        this.numIntentosActual = 0;

        if (!palabra.Equals(""))
        {
            SceneManager.LoadScene("paso " + this.nivel);
        }
    }





    public void guardarImagenDesbloqueada()
    {
        this.numIntentosActual++;
        conexion.guardarImgDesbloqueada(this.palabra, this.ActivePlayer.id, this.numIntentos, this.NumIntentosActual);
    }


    public bool ImagenIncompleta()
    {
        return this.conexion.obtenerUltimoidImgDesbloqueada(this.ActivePlayer.id, this.palabra) == -1 ? false : true;
    }



    public void cargarUsuario()
    {

    }

    public void cargarJugadores()
    {
        this.players = conexion.obtenerJugadores();
        if (players.Count > 0)
        {
            ActivePlayer = players.Find(item => item.id == 1);
        }

    }


    public void cargarLista(string tema, Dropdown miMenu)
    {

        List<string> Palabras = conexion.obtenerPalabras(tema);
        miMenu.options.Clear();
        miMenu.options.Add(new Dropdown.OptionData() { text = "Ninguno" });

        foreach (String s in Palabras)
        {
            miMenu.options.Add(new Dropdown.OptionData() { text = s });

            //Debug.Log("Aqui deberia de esta popor? " + s);
        }

    }

    public void cargarFechas(int idUsuario, Dropdown miMenu)
    {
        List<string> fechas = conexion.obtenerFechas(idUsuario);
        miMenu.options.Clear();
        miMenu.options.Add(new Dropdown.OptionData() { text = "Ninguno" });

        foreach (String s in fechas)
        {
            miMenu.options.Add(new Dropdown.OptionData() { text = s });

            //Debug.Log("Aqui deberia de esta popor? " + s);
        }
    }

    public void cargarTemas(Dropdown menuTema)
    {
        List<string> Temas = conexion.obtenerTemas();
        menuTema.options.Clear();
        menuTema.options.Add(new Dropdown.OptionData() { text = "Ninguno" });

        foreach (String tema in Temas)
        {
            menuTema.options.Add(new Dropdown.OptionData() { text = tema });

        }
        //string nameScene = SceneManager.GetActiveScene().name;

        //if (nameScene.Equals("reporte"))
        //{
        //    menuTema.options.Add(new Dropdown.OptionData() { text = "" });

        //}


    }

    public void cargarNinios(Dropdown menuNinio)
    {
        List<string> Temas = conexion.obtenerNinios();
        menuNinio.options.Clear();
        menuNinio.options.Add(new Dropdown.OptionData() { text = "Ninguno" });

        foreach (String tema in Temas)
        {
            menuNinio.options.Add(new Dropdown.OptionData() { text = tema });

        }

        //string nameScene = SceneManager.GetActiveScene().name;

        //menuNinio.options.Add(new Dropdown.OptionData() { text = "" });

        //if (nameScene.Equals("reporte")){

        //}
    }


    public string obtenerPalabraSiguiente()
    {


        int index = this.Palabras.IndexOf(this.palabra);
        if (index + 1 == this.Palabras.Count)
        {
            return this.Palabras[0];

        }
        else if (index < Palabras.Count)
        {

            return this.Palabras[index + 1];

        }
        return null;

    }


    public List<string> obtenerPalabrasTema()
    {
        return conexion.obtenerPalabras(this.tema);

    }

    public List<Imagen> cargarImagenes()
    {
        return conexion.obtenerImagenes();
    }

    public List<Imagen> cargarImagenesPorNino()
    {
        return conexion.obtenerImagenesDesbloqueadasPorNino(this.ActivePlayer.id);
    }


    public List<Imagen> cargarImagenesDesbloqueadasporPalabra()
    {
        Debug.Log(this.ActivePlayer.id);
        Debug.Log(this.ActivePlayer.toString());

        return conexion.obtenerImagenesDesbloqueadasPorPalabra(this.ActivePlayer.id, this.palabra);
    }

    public List<Imagen> cargarImagenesDesbloqueadasporPalabra(string palabra)
    {
        return conexion.obtenerImagenesDesbloqueadasPorPalabra(this.ActivePlayer.id, palabra);
    }


    public void setNivel(int nivel)
    {
        this.nivel = nivel;
    }

    public void setTema(string tema)
    {
        this.Palabras = conexion.obtenerPalabras(tema);
        this.tema = tema;
    }

    public void setPalabra(string palabra)
    {
        this.palabra = palabra;
    }

    public String[] obtenerSilabasPalabra(String palabra)
    {
        String npalabra = conexion.obtenerSilabasPalabra(palabra);
        npalabra.IndexOf(" ", 0);

        String[] silabas = new String[npalabra.Split(" "[0]).Length];
        silabas = npalabra.Split(" "[0]);


        return silabas;
    }


    public List<Reporte> cargarReporteDelNinio(int ninio)
    {
        return conexion.generarReportePorNinio(ninio);
    }
    public List<Reporte> cargarReporteDelNinioTema(int ninio, string tema)
    {
        return conexion.generarReportePorNinioTema(ninio, tema);
    }
    public List<Reporte> cargarReporteDelNinioTemaFigura(int ninio, string tema, string figura)
    {
        return conexion.generarReportePorNinioTemaFigura(ninio, tema, figura);
    }
    public List<Reporte> cargarReporteDelNinioTemaFiguraNivel(int ninio, string tema, string figura, int nivel)
    {
        return conexion.generarReportePorNinioTemaFiguraNivel(ninio, tema, figura, nivel);
    }
    public List<Reporte> cargarReporteDelNinioTemaFiguraNivelTurno(int ninio, string tema, string figura, int nivel, string turno)
    {
        return conexion.generarReportePorNinioTemaFiguraNivelTurno(ninio, tema, figura, nivel, turno);
    }
    public List<Reporte> cargarReporteDelNinioTemaFiguraNivelTurnoFecha(int ninio, string tema, string figura, int nivel, string turno, string fecha)
    {
        return conexion.generarReportePorNinioTemaFiguraNivelTurnoFecha(ninio, tema, figura, nivel, turno, fecha);
    }
    public List<Reporte> cargarReporteDelNinioFecha(int ninio, string fecha)
    {
        return conexion.generarReportePorNinioFecha(ninio, fecha);
    }

    public double calcularPorcentaje(int ninio)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinio(ninio).Count;
        y = conexion.generarReportePorNinioSinExitoso(ninio).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }

    public double calcularPorcentajeNinioFecha(int ninio, string fecha)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinioFecha(ninio, fecha).Count;
        y = conexion.generarReportePorNinioFechaSinExitoso(ninio, fecha).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }
    public double calcularPorcentajeNinioTema(int ninio, string tema)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinioTema(ninio, tema).Count;
        y = conexion.generarReportePorNinioTemaSinExitoso(ninio, tema).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }
    public double calcularPorcentajeNinioTemaFigura(int ninio, string tema, string figura)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinioTemaFigura(ninio, tema, figura).Count;
        y = conexion.generarReportePorNinioTemaFiguraSinExitoso(ninio, tema, figura).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }
    public double calcularPorcentajeNinioTemaFiguraNivel(int ninio, string tema, string figura, int nivel)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinioTemaFiguraNivel(ninio, tema, figura, nivel).Count;
        y = conexion.generarReportePorNinioTemaFiguraNivelSinExitoso(ninio, tema, figura, nivel).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }

    public double calcularPorcentajeNinioTemaFiguraNivelTurno(int ninio, string tema, string figura, int nivel, string turno)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinioTemaFiguraNivelTurno(ninio, tema, figura, nivel, turno).Count;
        y = conexion.generarReportePorNinioTemaFiguraNivelTurnoSinExitoso(ninio, tema, figura, nivel, turno).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }
    public double calcularPorcentajeNinioTemaFiguraNivelTurnoFecha(int ninio, string tema, string figura, int nivel, string turno, string fecha)
    {
        double porcentaje = 0;
        double porcentajeRed = 0;
        double x = 0;
        double y = 0;
        x = conexion.generarReportePorNinioTemaFiguraNivelTurnoFecha(ninio, tema, figura, nivel, turno, fecha).Count;
        y = conexion.generarReportePorNinioTemaFiguraNivelTurnoFechaSinExitoso(ninio, tema, figura, nivel, turno, fecha).Count;
        if (x == 0.0 && y == 0.0)
        {
            porcentaje = 0.0;
        }
        else
        {
            porcentaje = (x / y) * 100;
        }
        Debug.Log(porcentaje);
        porcentajeRed = Math.Round(porcentaje, 2);
        Debug.Log(porcentajeRed);
        return porcentajeRed;
    }
    public int numeroDeIntentos(int ninio)
    {
        int y;
        y = conexion.generarReportePorNinioSinExitoso(ninio).Count;
        return y;
    }

    public int numeroDeIntentosNinioTema(int ninio, string tema)
    {
        int y;
        y = conexion.generarReportePorNinioTemaSinExitoso(ninio, tema).Count;
        return y;
    }
    public int numeroDeIntentosNinioFecha(int ninio, string fecha)
    {
        int y;
        y = conexion.generarReportePorNinioFechaSinExitoso(ninio, fecha).Count;
        return y;
    }
    public int numeroDeIntentosNinioTemaFigura(int ninio, string tema, string figura)
    {
        int y;
        y = conexion.generarReportePorNinioTemaFiguraSinExitoso(ninio, tema, figura).Count;
        return y;
    }
    public int numeroDeIntentosNinioTemaFiguraNivel(int ninio, string tema, string figura, int nivel)
    {
        int y;
        y = conexion.generarReportePorNinioTemaFiguraNivelSinExitoso(ninio, tema, figura, nivel).Count;
        return y;
    }
    public int numeroDeIntentosNinioTemaFiguraNivelTurno(int ninio, string tema, string figura, int nivel, string turno)
    {
        int y;
        y = conexion.generarReportePorNinioTemaFiguraNivelTurnoSinExitoso(ninio, tema, figura, nivel, turno).Count;
        return y;
    }
    public int numeroDeIntentosNinioTemaFiguraNivelTurnoFecha(int ninio, string tema, string figura, int nivel, string turno, string fecha)
    {
        int y;
        y = conexion.generarReportePorNinioTemaFiguraNivelTurnoFechaSinExitoso(ninio, tema, figura, nivel, turno, fecha).Count;
        return y;
    }
    //public int numeroDeIntentos(int ninio, string palabra, string nivel, string turno, string sesion)
    //{
    //    int y;
    //    y = conexion.generarReportePalabraNinio(ninio, palabra, nivel, turno, sesion).Count;
    //    return y;
    //}
}

[System.Serializable]
public class Player
{
    public int id;
    public string name;


    public Player()
    {
        //this.id = 04;
        //this.name = "Hola";
        //this.stats = new int[3] { 1,35,6};
    }


    public Player(int id)
    {
        this.id = id;



    }
    public Player(int id, String name)
    {
        this.id = id;
        this.name = name;

    }
    public string toString()
    {
        return "id: " + this.id + " name: " + this.name;
    }


    public override bool Equals(object obj)
    {
        Player p = (Player)obj;
        if (p == null)
            return false;
        else
            return this.id.Equals(p.id);
    }

    public override int GetHashCode()
    {
        return 1;
    }

    public static explicit operator Player(UnityEngine.Object v)
    {
        throw new NotImplementedException();
    }


}



public class Imagen
{
    public int id;
    public int numPart;
    public string nombre;
    public string path;
    public string tema;
    public int divisor;
    public int numCompletado;

    public Imagen(int id, int numPart, string nombre, string path)
    {
        this.id = id;
        this.numPart = numPart;
        this.nombre = nombre;
        this.path = path;
    }
    public Imagen()
    {

    }
    public int NumCompletado
    {
        get
        {
            return numCompletado;
        }

        set
        {
            numCompletado = value;
        }
    }

    public int Divisor
    {
        get
        {
            return divisor;
        }

        set
        {
            divisor = value;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }
}

public class Reporte
{
    public string nombre;
    public string fecha;
    public string tema;
    public string figura;
    public int nivel;
    public string turno;
    public double duracion;
    public string path;

    public Reporte(string fecha, double duracion, string path)
    {
        this.fecha = fecha;
        this.duracion = duracion;
        this.path = path;
    }

    public Reporte(string nombre, string fecha, string tema, string figura, int nivel, string turno, double duracion, string path)
    {
        this.nombre = nombre;
        this.fecha = fecha;
        this.tema = tema;
        this.figura = figura;
        this.nivel = nivel;
        this.turno = turno;
        this.duracion = duracion;
        this.path = path;
    }

    public Reporte(string nombre, string fecha, string turno, double duracion)
    {
        this.nombre = nombre;
        this.fecha = fecha;
        this.turno = turno;
        this.duracion = duracion;
    }
}

