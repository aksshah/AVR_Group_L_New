using UnityEngine;
using ZXing;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawImageBackground;
    [SerializeField]
    private AspectRatioFitter _aspectRatioFitter;
    [SerializeField]
    private TextMeshProUGUI _textOut;
    [SerializeField]
    private RectTransform _scanZone;

    private bool _isCamAvaible;
    private WebCamTexture _cameraTexture;
    private const string TargetQRCode = "MiniGame2"; // Define the correct QR code text
    private const string GameSceneName = "GameScene"; // Name of the game scene

    void Start()
    {
        SetUpCamera();
    }

    void Update()
    {
        UpdateCameraRender();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length == 0)
        {
            _isCamAvaible = false;
            return;
        }
        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _cameraTexture = new WebCamTexture(devices[i].name, (int)_scanZone.rect.width, (int)_scanZone.rect.height);
                break;
            }
        }
        _cameraTexture.Play();
        _rawImageBackground.texture = _cameraTexture;
        _isCamAvaible = true;
    }

    private void UpdateCameraRender()
    {
        if (_isCamAvaible == false) return;

        float ratio = (float)_cameraTexture.width / (float)_cameraTexture.height;
        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = _cameraTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, -orientation);
    }

    public void OnClickScan()
    {
        Scan();
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_cameraTexture.GetPixels32(), _cameraTexture.width, _cameraTexture.height);

            if (result != null)
            {
                _textOut.text = $"QR Code Found: {result.Text}";

                if (result.Text == TargetQRCode)
                {
                    StartCoroutine(ShowSuccessAndLoadScene());
                }
                else
                {
                    _textOut.text = "Incorrect QR Code.";
                }
            }
            else
            {
                _textOut.text = "Failed to Read QR CODE.";
            }
        }
        catch
        {
            _textOut.text = "Error occurred during scanning.";
        }
    }

    private IEnumerator ShowSuccessAndLoadScene()
    {
        _textOut.text = "QR Code Correct! Transitioning...";
        yield return new WaitForSeconds(2); // Display success message for 2 seconds
        SceneManager.LoadScene(GameSceneName); // Load the next scene
    }
}
