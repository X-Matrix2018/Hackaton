using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine.Networking;
using System;
using System.Runtime.InteropServices;


public class Server : MonoBehaviour
{
    Town town;
    Peop peop;
    Terr terr;

    public List<float[]> coords;
    public List<GameObject> chels;
    public List<GameObject> builds;
    public GameObject build;
    public GameObject build2;
    public GameObject build3;
    public GameObject chel;
    GameObject clone_build;
    static int port = 10000;
    static string address = "188.68.221.63";
    static string a;
    public object JsonConvert { get; private set; }

    public int n;
    void Start()

    {
        town = new Town();
        peop = new Peop();
        terr = new Terr();
        server();
    }
    public void server()
    {
        MyClass myObject = new MyClass();
        for (int i = 1; i <= 3; i++)
        {
            if (i == 1) { myObject.action = "get_town"; }
            if (i == 2) { myObject.action = "get_people"; }
            if (i == 3) { myObject.action = "get_terrain"; }

            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                myObject.token = "4";
                string message = JsonUtility.ToJson(myObject);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                byte[] data = Encoding.ASCII.GetBytes(message);
                socket.Send(data);
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.ASCII.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                // Debug.Log("ответ сервера: " + builder.ToString());
                JsonUtility.FromJsonOverwrite(builder.ToString(), town);
                JsonUtility.FromJsonOverwrite(builder.ToString(), peop);
                JsonUtility.FromJsonOverwrite(builder.ToString(), terr);
                if (i == 1)
                {
                    coords = new List<float[]>();
                    for (int i2 = 0; i2 <= town.data.buildings.Length - 1; i2++)
                    {
                        coords.Add(new float[2] {town.data.buildings[i2].position[0] , town.data.buildings[i2].position[1] });
                        n = new System.Random().Next(1, 3);
                        Debug.Log(n);
                        if (n == 1)
                        {
                            clone_build = Instantiate(build, new Vector3(town.data.buildings[i2].position[0], 6, town.data.buildings[i2].position[1]) * 5, Quaternion.identity, build.transform.parent);
                        }
                        if (n == 2)
                        {
                            clone_build = Instantiate(build2, new Vector3(town.data.buildings[i2].position[0], 6, town.data.buildings[i2].position[1]) * 5, Quaternion.identity, build.transform.parent);
                        }
                        if (n == 3)
                        {
                            clone_build = Instantiate(build3, new Vector3(town.data.buildings[i2].position[0], 6, town.data.buildings[i2].position[1]) * 5, Quaternion.identity, build.transform.parent);
                        }


                    }
                }
                    // var gg = JsonUtility.FromJson<resultn>(builder.ToString());

                    /*    Debug.Log(all.data.buildings[0].position[0]);
                    if (i == 1)
                    {
                        Debug.Log(builder.ToString());
                        Debug.Log("Town: i= " + town.i);
                        Debug.Log("config: max_x=" + town.config.max_x + " max_y=" + town.config.max_y + " max_building_x=" + town.config.max_building_x + " max_building_y=" + town.config.max_building_y);
                        Debug.Log("data: money=" + town.data.money + " attraction=" + town.data.attraction + " incomes=" + town.data.incomes + " expenses=" + town.data.expenses + " workload=" + town.data.workload + " quarters=" + town.data.quarters + " time=" + town.data.time);
                        Debug.Log("data: buildings:");
                        for (int i2 = 0; i2 <= town.data.buildings.Length - 1; i2++)
                        {
                            Debug.Log("type=" + town.data.buildings[i2].type + " position=" + town.data.buildings[i2].position[0] + "," + town.data.buildings[i2].position[1]);
                        }


                    }
                    if (i == 2)
                    {
                        Debug.Log(builder.ToString());
                        Debug.Log("People:");
                        for (int i2 = 0; i2 <= peop.people.Length - 1; i2++)
                        {
                            Debug.Log("id= " + peop.people[i2].id + " position=" + peop.people[i2].position[0] + "," + peop.people[i2].position[1] + " velocity=" + peop.people[i2].velocity[0] + "," + peop.people[i2].velocity[1] + " happiness=" + peop.people[i2].happiness);
                        }
                    }
                    if (i == 3)
                    {
                        Debug.Log(builder.ToString());
                        Debug.Log("Terrain: data:");
                        for (int i2 = 0; i2 <= terr.data.Length - 1; i2++)
                        {
                            a = "";
                            for (int i3 = 0; i3 <= 15; i3++)
                            {
                                a = a + " " + terr.data[i2].line[i3];

                            }
                            Debug.Log("Line= " + a);
                        }
                    }*/
                    socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }

    [Serializable]
    public class MyClass
    {
        public string token;
        public string action;
    }
    [System.Serializable]
    public class Town
    {
        public string _id;
        public int i;
        public TownCfg config;
        public TownData data;
    }
    [System.Serializable]
    public class Peop
    {
        public People[] people;
    }


    [System.Serializable]
    public class Terr
    {
        public Terrain[] data;
    }



    [System.Serializable]
    public class TownCfg
    {
        public int max_x;
        public int max_y;
        public int max_building_x;
        public int max_building_y;
    }
    [System.Serializable]
    public class TownData
    {
        public double money;
        public double attraction;
        public int incomes;
        public float expenses;
        public int workload;
        public int quarters;
        public string time;
        public TownDataBuild[] buildings;
    }
    [System.Serializable]
    public class TownDataBuild
    {
        public string type;
        public int[] position;
    }
    [System.Serializable]
    public class People
    {
        public int id;
        public int[] position;
        public int[] velocity;
        public float happiness;
    }
    [System.Serializable]
    public class Terrain
    {
        public string[] line;
    }





    void Update()
    {

    }

}
