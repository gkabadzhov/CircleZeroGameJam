using Newtonsoft.Json;
using System.Text;
using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using OTBG.Utilities.General;

namespace OTBG.General.Utility
{
    public class WebRequestHandler : Singleton<WebRequestHandler>
    {
        private IEnumerator SendRequestInternal<T>(UnityWebRequest request, Action<T> onSuccess, Action<string> onError)
        {
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    onError?.Invoke(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    T result = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                    onSuccess?.Invoke(result);
                    break;
            }
        }

        public void Get<T>(Uri uri, Action<T> onSuccess = null, Action<string> onError = null)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            SendInternal(webRequest, null, onSuccess, onError);
        }

        public void Patch<T>(Uri uri, object content = null, Action<T> onSuccess = null, Action<string> onError = null)
        {
            UnityWebRequest webRequest = new UnityWebRequest(uri, "PATCH")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };
            webRequest.SetRequestHeader("Content-Type", "application/json");
            SendInternal(webRequest, content, onSuccess, onError);
        }

        public void Post<T>(Uri uri, object content = null, Action<T> onSuccess = null, Action<string> onError = null)
        {
            UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(uri, JsonConvert.SerializeObject(content));
            SendInternal(webRequest, content, onSuccess, onError);
        }

        public void Put<T>(Uri uri, object content = null, Action<T> onSuccess = null, Action<string> onError = null)
        {
            UnityWebRequest webRequest = UnityWebRequest.Put(uri, JsonConvert.SerializeObject(content));
            SendInternal(webRequest, content, onSuccess, onError);
        }

        private void SendInternal<T>(UnityWebRequest request, object content = null, Action<T> onSuccess = null, Action<string> onError = null)
        {
            if (content != null)
            {
                UploadHandlerRaw uH = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content)));
                uH.contentType = "application/json";
                request.uploadHandler = uH;
            }
            StartCoroutine(SendRequestInternal(request, onSuccess, onError));
        }

        public void GetImage(string path, Action<Texture2D> onSuccess = null, Action<string> onError = null)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
            StartCoroutine(GetImageInternal(www, onSuccess, onError));
        }

        private IEnumerator GetImageInternal(UnityWebRequest request, Action<Texture2D> onSuccess, Action<string> onError)
        {
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    onError?.Invoke(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(DownloadHandlerTexture.GetContent(request));
                    //onSuccess?.Invoke(((DownloadHandlerTexture)request.downloadHandler).texture);
                    break;
            }
        }

        public void GetSound(string path, Action<AudioClip> onSuccess = null, Action<string> onError = null)
        {
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG);
            StartCoroutine(GetSoundInternal(www, onSuccess, onError));
        }

        private IEnumerator GetSoundInternal(UnityWebRequest request, Action<AudioClip> onSuccess, Action<string> onError)
        {
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    onError?.Invoke(request.downloadHandler.text);
                    break;
                case UnityWebRequest.Result.Success:
                    onSuccess?.Invoke(DownloadHandlerAudioClip.GetContent(request));
                    break;
            }
        }
    }
}