using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using UnityEngine.UI;
using System.Net;
using UnityEngine.Networking;

public class FileSelection : MonoBehaviour
{
    public Image imageComponent;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        // !!! Uncomment any of the examples below to show the file browser !!!

        // Example 1: Show a save file dialog using callback approach
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Example 2: Show a select folder dialog using callback approach
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
        // FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        //						   () => { Debug.Log( "Canceled" ); },
        //						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Example 3: Show a select file dialog using coroutine approach
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, true, null, null, "Select Files", "Load");

        // Dialog is closed
        // Print whether the user has selected some files or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
            OnFilesSelected(FileBrowser.Result); // FileBrowser.Result is null, if FileBrowser.Success is false
    }

    void OnFilesSelected(string[] filePaths)
    {
        // Print paths of the selected files
        for (int i = 0; i < filePaths.Length; i++)
            Debug.Log(filePaths[i]);

        // Get the file path of the first selected file
        string filePath = filePaths[0];

        // Read the bytes of the first file via FileBrowserHelpers
        // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
        byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(filePath);
        //CopyFileToPersistentDataPath(filePath);
        //PlayAudioFromBytes(bytes);
        //PlayWavFromFile(CopyFileToPersistentDataPaths(filePath));
        PlayMusicFromFile(filePath);

        // Or, copy the first file to persistentDataPath
        string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(filePath));
        FileBrowserHelpers.CopyFile(filePath, destinationPath);
    }

    public void SetImageFromBytes(byte[] bytes)
    {
        // Texture oluþtur ve byte dizisinden veriyi yükle
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);

        // Texture'tan Sprite oluþtur
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

        // Image'a atama
        imageComponent.sprite = sprite;
    }

    public void CopyFileToPersistentDataPath(string sourceFilePath)
    {
        // Kaynak dosyanýn varlýðýný kontrol et
        if (File.Exists(sourceFilePath))
        {
            // Hedef dosyanýn tam yolunu oluþtur
            string destinationPath = Path.Combine(Application.persistentDataPath, Path.GetFileName(sourceFilePath));

            // Dosyayý kopyala
            File.Copy(sourceFilePath, destinationPath, true);

            Debug.Log("File copied to: " + destinationPath);
        }
        else
        {
            Debug.LogError("Source file does not exist!");
        }
    }

    public string CopyFileToPersistentDataPaths(string sourceFilePath)
    {
        // Hedef dosyanýn tam yolunu oluþtur
        string destinationPath = Path.Combine(Application.persistentDataPath, Path.GetFileName(sourceFilePath));

        // Kaynak dosyanýn varlýðýný kontrol et
        if (File.Exists(sourceFilePath))
        {
            // Dosyayý kopyala
            File.Copy(sourceFilePath, destinationPath, true);

            Debug.Log("File copied to: " + destinationPath);
            return destinationPath;
        }
        else
        {
            Debug.LogError("Source file does not exist!");
            return null;
        }
    }
    /**********************************************************************************************************************************************/

    public void PlayMusicFromFile(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            StartCoroutine(LoadMusic(filePath));
        }
    }

    private IEnumerator LoadMusic(string filePath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file:///" + filePath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading music: " + www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}
