using UnityEngine;

[CreateAssetMenu(fileName = "ElevenLabsConfig", menuName = "ElevenLabs/ElevenLabs Configuration")]
public class ElevenLabsConfig : ScriptableObject
{
    public string apiKey = "338086058f3596af0ee592a79233b3e4";
    public string voiceId = "PLjHE1GJcRw3TKOCicFh";
    public string ttsUrl = "https://api.elevenlabs.io/v1/text-to-speech/{0}/stream";

}

