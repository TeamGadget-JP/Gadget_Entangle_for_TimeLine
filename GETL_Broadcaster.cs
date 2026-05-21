using UnityEngine;
using UnityEngine.Playables;
using System.Net.Sockets;
using System.Text;

namespace Gadget.GETL
{
    [RequireComponent(typeof(PlayableDirector))]
    [ExecuteAlways] // Extremely important: Necessary for the seek bar to move when dragged in the editor.
    public class GETL_Broadcaster : MonoBehaviour
    {
        [Header("=== Master Switch ===")]
        [Tooltip("Unchecking this box will completely stop communication with GEC/GEi.")]
        public bool enableSync = true;

        [Header("=== GETL Master Clock Settings ===")]
        public string targetIP = "127.0.0.1";
        [Tooltip("Ports for Cascadeur (GEC)")]
        public int gecPort = 8991; 
        [Tooltip("Port for iClone (GEi)")]
        public int geiPort = 8992; 

        [Header("=== Timeline Settings ===")]
        public float targetFrameRate = 60f;

        private PlayableDirector director;
        private UdpClient udpClient;
        private int lastSentFrame = -1;

        private void OnEnable()
        {
            director = GetComponent<PlayableDirector>();
            try
            {
                udpClient = new UdpClient();
            }
            catch (System.Exception e)
            {
                Debug.LogError("<b>[Team Gadget GETL]</b> UDP Client creation failed: " + e.Message);
            }
        }

        private void OnDisable()
        {
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient = null;
            }
        }

        private void Update()
        {
            // When communication is OFF or when there are no components, nothing happens.
            if (!enableSync || director == null || udpClient == null) return;

            // Convert the current time on the timeline to "frames".
            int currentFrame = Mathf.FloorToInt((float)director.time * targetFrameRate);

            // Packets are unleashed indiscriminately when the frame changes (during seeking) or during playback!
            if (currentFrame != lastSentFrame || director.state == PlayState.Playing)
            {
                SendTimeCode(currentFrame, director.state == PlayState.Playing);
                lastSentFrame = currentFrame;
            }
        }

        private void SendTimeCode(int frame, bool isPlaying)
        {
            // A very simple and lightweight protocol
            // Example: "GETL,30,1" (30th frame, currently playing)
            string message = $"GETL,{frame},{(isPlaying ? 1 : 0)}";
            byte[] data = Encoding.UTF8.GetBytes(message);

            try
            {
                // First attempt: Sent to Cascadeur (GEC)
                udpClient.Send(data, data.Length, targetIP, gecPort);
                
                // Second attempt: Sent to iClone (GEi)
                udpClient.Send(data, data.Length, targetIP, geiPort);
            }
            catch
            {
                // Sending errors are suppressed to prevent the editor from crashing.
            }
        }
    }
}