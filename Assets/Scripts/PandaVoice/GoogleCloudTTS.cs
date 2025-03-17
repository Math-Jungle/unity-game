using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;

public class GoogleCloudTTS : MonoBehaviour
{
    [SerializeField] private string apiKey = "YOUR_API_KEY"; // Replace with your actual API key
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string languageCode = "en-US";
    [SerializeField] private string voiceName = "en-US-Neural2-D"; // Friendly child-like voice
    [SerializeField][Range(0.25f, 4.0f)] private float pitch = 1.2f; // Higher pitch for panda
    [SerializeField][Range(0.25f, 4.0f)] private float speakingRate = 1.0f;

    private bool isPlaying = false;
    private AudioClip currentClip;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Speak(string text)
    {
        if (string.IsNullOrEmpty(apiKey) || apiKey == "YOUR_API_KEY")
        {
            Debug.LogError("Please set your Google Cloud API key in the inspector");
            return;
        }

        StopSpeaking();
        StartCoroutine(GetAudioFromGoogle(text));
    }

    public void StopSpeaking()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        isPlaying = false;
    }

    public bool IsSpeaking()
    {
        return isPlaying;
    }

    IEnumerator GetAudioFromGoogle(string text)
    {
        // Format the request according to Google Cloud TTS API specifications
        string requestBody = "{"
            + "\"input\":{"
            + "\"text\":\"" + text + "\""
            + "},"
            + "\"voice\":{"
            + "\"languageCode\":\"" + languageCode + "\","
            + "\"name\":\"" + voiceName + "\""
            + "},"
            + "\"audioConfig\":{"
            + "\"audioEncoding\":\"MP3\","
            + "\"pitch\":" + pitch.ToString("F1") + ","
            + "\"speakingRate\":" + speakingRate.ToString("F1")
            + "}"
            + "}";

        string url = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + apiKey;

        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            isPlaying = true;
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Google TTS API Error: " + www.error);
                isPlaying = false;
            }
            else
            {
                // Process the response
                string response = www.downloadHandler.text;
                GoogleTTSResponse ttsResponse = JsonUtility.FromJson<GoogleTTSResponse>(response);

                // Convert base64 to audio data
                byte[] audioBytes = Convert.FromBase64String(ttsResponse.audioContent);

                // Start another coroutine to load the audio
                StartCoroutine(LoadAndPlayAudio(audioBytes));
            }
        }
    }

    IEnumerator LoadAndPlayAudio(byte[] audioData)
    {
        // Create temporary file path
        string tempPath = $"{Application.persistentDataPath}/temp_audio_{DateTime.Now.Ticks}.mp3";
        System.IO.File.WriteAllBytes(tempPath, audioData);

        // Use UnityWebRequest to load the audio clip
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + tempPath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Audio loading error: " + www.error);
                isPlaying = false;
            }
            else
            {
                currentClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = currentClip;
                audioSource.Play();

                // Wait until the audio finishes playing
                yield return new WaitForSeconds(currentClip.length);

                // Delete the temporary file
                try
                {
                    System.IO.File.Delete(tempPath);
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Could not delete temp file: " + e.Message);
                }

                isPlaying = false;
            }
        }
    }
}

[Serializable]
public class GoogleTTSResponse
{
    public string audioContent;
}