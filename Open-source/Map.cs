using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace YandexMaps
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    public class Map
    {
        #region API
        /// <summary>
        /// Добавление маркеров,которые требуется отобразить на карте. (Максимальное количество маркеров доступно 100)
        /// </summary>
        public static List<Vector2> SetMarker = new List<Vector2>();
        /// <summary>
        /// Долгота.
        /// </summary>
        public static float Longitude { set; get; }
        /// <summary>
        /// широта.
        /// </summary>
        public static float Latitude { set; get; }
        /// <summary>
        /// Уровень масштабирования карты от 0 до 17.
        /// </summary>
        public static int Size { set; get; }
        /// <summary>
        /// Ширина запрашиваемого изображения карты (в пикселах) доступный максимальный размер ширины 650.
        /// </summary>
        public static int Width { set; get; }
        /// <summary>
        /// Высота запрашиваемого изображения карты (в пикселах) доступный максимальный размер высоты 450.
        /// </summary>
        public static int Height { set; get; }
        /// <summary>
        /// Тип картов.
        /// </summary>
        public static TypeMap SetTypeMap { set; get; }
        /// <summary>
        /// Тип слоёв. Прежде тем чтобы использовать слои, включите параметр EnabledLayer = true;
        /// </summary>
        public static TypeMapLayer SetTypeMapLayer { set; get; }
        /// <summary>
        /// Тип карты.
        /// </summary>
        public enum TypeMap
        {
            /// <summary>
            /// Схема местности и названия географических объектов.
            /// </summary>
            map,
            /// <summary>
            /// Местность, сфотографированная со спутника.
            /// </summary>
            sat
        }
        /// <summary>
        /// Включение/Выключение слоёв.
        /// </summary>
        public static bool EnabledLayer { set; get; }
        /// <summary>
        /// Тип карты слоёв.
        /// </summary>
        public enum TypeMapLayer
        {
            /// <summary>
            /// Слой названия географических объектов.
            /// </summary>
            skl,
            /// <summary>
            /// Слой пробок.
            /// </summary>
            trf,
        }
        /// <summary>
        /// Получение текстуры
        /// </summary>
        /// <returns></returns>
        public static Texture GetTexture { get; private set; }
        /// <summary>
        /// Получение текстуры 2D
        /// </summary>
        /// <returns></returns>
        public static Texture2D GetTexture2D { get; private set; }
        /// <summary>
        /// Загружает карту один раз ( не рекомендуется запускать в методе Update(), для этого есть метод UpdateLoadMap() ).
        /// </summary>
        public static void LoadMap()
        {
            var mono = Object.FindObjectOfType<MonoBehaviour>();
            mono.StartCoroutine(DownloadMap());
        }
        /// <summary>
        /// Загружает карту и обновляет каждый раз ( не рекомендуется запускать в методе Update() ).
        /// </summary>
        public static void UpdateLoadMap()
        {
            var mono = Object.FindObjectOfType<MonoBehaviour>();
            mono.StopCoroutine(DownloadMap(mono));
            mono.StartCoroutine(DownloadMap(mono));
        }
        /// <summary>
        /// Загружает карту и обновляет каждый раз в заданном тайм-аутом ( не рекомендуется запускать в методе Update() ).
        /// </summary>
        /// <param name="timeout">тайм-аут.</param>
        public static void UpdateLoadMap(float timeout)
        {
            var mono = Object.FindObjectOfType<MonoBehaviour>();
            mono.StopCoroutine(DownloadMap(timeout, mono));
            mono.StartCoroutine(DownloadMap(timeout, mono));
        }
        #endregion
        #region System
        private static string Convert(List<Vector2> vectors)
        {

            string convertX = "";
            string convertY = "";

            for (int i = 0; i < vectors.Count; i++)
            {
                convertX = vectors[i].x.ToString().Replace(',', '.');
                convertY = vectors[i].y.ToString().Replace(',', '.');


            }

            return convertX + "," + convertY;

        }
        private static string[] ConvertArry(List<Vector2> vectors)
        {

            string[] convert = new string[SetMarker.Count];

            for (int i = 0; i < SetMarker.Count; i++)
            {

                convert[i] = Convert(SetMarker[i].x) + "," + Convert(SetMarker[i].y);

            }

            return convert;
        }
        private static string Convert(float value)
        {
            string convert = value.ToString().Replace(',', '.');

            return convert;
        }
        private static string Convert(Vector2 vector)
        {
            string convertX = vector.x.ToString().Replace(',', '.');
            string convertY = vector.y.ToString().Replace(',', '.');

            return convertX + "," + convertY;
        }
        private static string Yandex(bool layer)
        {
            var url = layer
                ? "https://static-maps.yandex.ru/1.x/?ll=" +
                Convert(Longitude) + "," +
                Convert(Latitude) +
                "&size=" + Width.ToString() + "," + Height.ToString() +
                "&z=" + Size.ToString() +
                "&l=" + SetTypeMap + "," + SetTypeMapLayer +
                "&pt=" + string.Join("~", ConvertArry(SetMarker))
                : "https://static-maps.yandex.ru/1.x/?ll=" +
                Convert(Longitude) + "," +
                Convert(Latitude) +
                "&size=" + Width.ToString() + "," + Height.ToString() +
                "&z=" + Size.ToString() +
                "&l=" + SetTypeMap +
                "&pt=" + string.Join("~", ConvertArry(SetMarker));
            return url;
        }
        private static IEnumerator DownloadMap()
        {
            Width = Mathf.Clamp(Width, 0, 650);
            Height = Mathf.Clamp(Width, 0, 450);
            Size = Mathf.Clamp(Size, 0, 17);
            if (Width == 0 | Height == 0)
            {
                Width = 650;
                Height = 450;
            }
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(Yandex(EnabledLayer)))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(uwr.error);
                }
                else
                {

                    var download = DownloadHandlerTexture.GetContent(uwr);
                    download.name = "YandexMap";


                    if (uwr.isDone)
                    {
                        GetTexture = download;
                        GetTexture2D = download;
                    }

                }
            }
        }
        private static IEnumerator DownloadMap(MonoBehaviour mono)
        {
            Width = Mathf.Clamp(Width, 0, 650);
            Height = Mathf.Clamp(Width, 0, 450);
            Size = Mathf.Clamp(Size, 0, 17);
            if (Width == 0 | Height == 0)
            {
                Width = 650;
                Height = 450;
            }
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(Yandex(EnabledLayer)))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {

                    var download = DownloadHandlerTexture.GetContent(uwr);
                    download.name = "YandexMap";


                    if (uwr.isDone)
                    {
                        GetTexture = download;
                        GetTexture2D = download;
                    }

                }
            }
            yield return new WaitForSeconds(0.075f);
            mono.StartCoroutine(DownloadMap(mono));
        }
        private static IEnumerator DownloadMap(float timeout, MonoBehaviour mono)
        {
            Width = Mathf.Clamp(Width, 0, 650);
            Height = Mathf.Clamp(Width, 0, 450);
            Size = Mathf.Clamp(Size, 0, 17);
            if (Width == 0 | Height == 0)
            {
                Width = 650;
                Height = 450;
            }
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(Yandex(EnabledLayer)))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {

                    var download = DownloadHandlerTexture.GetContent(uwr);
                    download.name = "YandexMap";


                    if (uwr.isDone)
                    {
                        GetTexture = download;
                        GetTexture2D = download;
                    }

                }
            }
            yield return new WaitForSeconds(timeout);
            mono.StartCoroutine(DownloadMap(timeout, mono));
        }
        #endregion
    }
}