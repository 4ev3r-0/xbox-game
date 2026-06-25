using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Debugger : MonoBehaviour
{
    private Text debugText;

    private bool visible = true;

    // Smoothed FPS calculation
    private float deltaTime = 0.0f;

    void Start()
    {
        debugText = GetComponent<Text>();

        if (debugText == null)
        {
            Debug.LogError("Debugger script requires a UI Text component.");
        }
    }

    void Update()
    {
        // Toggle debugger
        if (Input.GetButtonDown("Submit"))
        {
            visible = !visible;

            if (debugText != null)
            {
                debugText.enabled = visible;
            }
        }

        if (!visible)
        {
            return;
        }

        // Smooth FPS
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        BuildDebugText();
    }

    void BuildDebugText()
    {
        StringBuilder sb = new StringBuilder();

        float fps = 1.0f / deltaTime;
        float frameMS = deltaTime * 1000.0f;

        //----------------------------------
        // PERFORMANCE
        //----------------------------------

        sb.AppendLine("=== DEBUGGER ===");
        sb.AppendLine("");

        sb.AppendLine("FPS: " + fps.ToString("F1"));
        sb.AppendLine("Frame Time: " + frameMS.ToString("F2") + " ms");
        sb.AppendLine("Delta Time: " + Time.deltaTime.ToString("F4"));

        sb.AppendLine("");

        //----------------------------------
        // DISPLAY
        //----------------------------------

        sb.AppendLine("Resolution: " +
            Screen.width + " x " +
            Screen.height);

        sb.AppendLine("Cameras: " +
            Camera.allCamerasCount);

        sb.AppendLine("");

        //----------------------------------
        // OBJECT COUNTS
        //----------------------------------

        GameObject[] allObjects =
            FindObjectsOfType<GameObject>();

        int activeObjects = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.activeInHierarchy)
            {
                activeObjects++;
            }
        }

        sb.AppendLine("Total Objects: " +
            allObjects.Length);

        sb.AppendLine("Active Objects: " +
            activeObjects);

        sb.AppendLine("");

        //----------------------------------
        // INPUT BUTTONS
        //----------------------------------

        sb.AppendLine("Pressed Buttons:");

        CheckButton(sb, "A");
        CheckButton(sb, "B");
        CheckButton(sb, "X");
        CheckButton(sb, "Y");

        CheckButton(sb, "Submit");
        CheckButton(sb, "Cancel");

        CheckButton(sb, "Left Bumper");
        CheckButton(sb, "Right Bumper");

        CheckButton(sb, "Left Trigger");
        CheckButton(sb, "Right Trigger");

        CheckButton(sb, "Left Joystick Pressed");
        CheckButton(sb, "Right Joystick Pressed");

        sb.AppendLine("");

        //----------------------------------
        // AXES
        //----------------------------------

        sb.AppendLine("Axis Values:");

        sb.AppendLine(
            "LX: " +
            Input.GetAxis("Left Joystick X").ToString("F2"));

        sb.AppendLine(
            "LY: " +
            Input.GetAxis("Left Joystick Y").ToString("F2"));

        sb.AppendLine(
            "RX: " +
            Input.GetAxis("Right Joystick X").ToString("F2"));

        sb.AppendLine(
            "RY: " +
            Input.GetAxis("Right Joystick Y").ToString("F2"));

        sb.AppendLine(
            "DPAD X: " +
            Input.GetAxis("DPAD X").ToString("F2"));

        sb.AppendLine(
            "DPAD Y: " +
            Input.GetAxis("DPAD Y").ToString("F2"));

        sb.AppendLine("");

        //----------------------------------
        // PLAYER POSITION
        //----------------------------------

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 pos = player.transform.position;

            sb.AppendLine(
                "Player Position:");

            sb.AppendLine(
                "X: " + pos.x.ToString("F2"));

            sb.AppendLine(
                "Y: " + pos.y.ToString("F2"));

            sb.AppendLine(
                "Z: " + pos.z.ToString("F2"));
        }

        debugText.text = sb.ToString();
    }

    void CheckButton(StringBuilder sb, string buttonName)
    {
        if (Input.GetButton(buttonName))
        {
            sb.AppendLine(buttonName);
        }
    }
}