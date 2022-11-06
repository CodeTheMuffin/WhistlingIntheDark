using UnityEngine;
//using UnityEngine.UI; // use if using normal Text; AKA if NOT using TextMeshPro
using System.IO;
using System.Collections.Generic;
//using UnityEngine.UI;
//using TMPro; // could use, but don't need the whole namespace


public class VersionManager : MonoBehaviour
{
    const string VERSION = "v0.0.1";

    //public Text version_text; // Use if NOT using TextMeshPro
    public TMPro.TextMeshProUGUI version_text;

    public class VersionMisMatchException : System.Exception
    {
        public VersionMisMatchException()
        { }

        public VersionMisMatchException(string message) : base(message)
        { }
    }

    /*
     WebGL folder structure:

    Root: /

    /Il2CppData
    --- /Metadata/global-metadata.dat
    
    /Resources
    --- /unity_default_resources
    
    /dev
    -- /shm
    -- /null
    -- /random
    -- /stderr
    -- /stdin
    -- /stdout
    -- /tty
    -- /ttyl
    -- /urandom

    /home
    --- /web_user #empty

    /idbfs
    --- /4411def9d576984c8d78253236b2a62f
    
    /proc
    --- /self/fd

    /tmp #empty
    
    # FILES at Root folder

    /RuntimeInitializeOnLoads.json
    /ScriptingAssemblies.json
    /boot.config
    /data.unity3d
     */

    void Start()
    {
        //string path = System.IO.Directory.GetCurrentDirectory();

        if (version_text)
        { version_text.text = VERSION; }

        //Debug.Log("Current Dir: " + path);
        ////Debug.Log("Current Dir2: " + System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath));
        //Debug.Log("Current Dir3: " + Application.dataPath + "/version.txt");
        //
        //Debug.Log("Application platform: " + Application.platform);
        //Debug.Log("Is Console Platform?: " + Application.isConsolePlatform);
        //Debug.Log("Is Mobile Platform?: " + Application.isMobilePlatform);
        //Debug.Log("Is Playing?: " + Application.isPlaying);
        //Debug.Log("Is RuntimePlatform.WebGLPlayer?: " + (Application.platform == RuntimePlatform.WebGLPlayer));
        //Debug.Log("Is RuntimePlatform.WindowsEditor?: " + (Application.platform == RuntimePlatform.WindowsEditor));
        //Debug.Log("Is Editor?: " + (Application.isEditor));
        //
        //Debug.Log("#########\n\n");
        //checkFolders();

        /*Debug.Log("DirectoryInfo Approach::\n\n");
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles();
        foreach (var file in fileInfo) Debug.Log(file);

        Debug.Log("#########");
        Debug.Log("GetFiles and GetDirectories Approach::\n\n");
        string[] folders = System.IO.Directory.GetDirectories(path);
        foreach (var file in folders) Debug.Log(file);
        string[] files = System.IO.Directory.GetFiles(path);
        foreach (var file in files) Debug.Log(file);

        Debug.Log("#########");
        Debug.Log("GetFiles and GetDirectories Approach 2::\n\n");
        var new_path = path + "Resources";

        Debug.Log("new path: " + new_path);

        Debug.Log("Does New Path exist 1?: " + Directory.Exists(new_path));
        Debug.Log("Does New Path exist 2?: " + Directory.Exists(path + "/Resources"));
        Debug.Log("Does New Path exist 3?: " + Directory.Exists(path + "\\Resources"));

        string[] folders2 = System.IO.Directory.GetDirectories(new_path);
        foreach (var file in folders2) Debug.Log(file);
        string[] files2 = System.IO.Directory.GetFiles(new_path);
        foreach (var file in files2) Debug.Log(file);*/


        /*string version_path = "Assets/version.txt";
        string version_path2 = Application.dataPath + "\\Resources\\version.txt";
        Debug.Log("ver path2: " + version_path2);
        StreamReader reader = new StreamReader(version_path2);
        version_text.text = reader.ReadLine(); //Assumes the version number is on the first line
        reader.Close();*/


        if(Application.isEditor)
		{
            //Debug.Log("Checking versions are matching...");

            string version_path = "Assets/version.txt";
            StreamReader reader = new StreamReader(version_path);
            string version_text = reader.ReadLine(); //Assumes the version number is on the first line

            //Debug.Log("Version: " + VERSION);
            //Debug.Log("Version txt: " + version_text);

            if (!VERSION.Equals(version_text))
            {
                string errorMessage = System.String.Format("VERSION constant: {0} DOES NOT MATCH version from txt file: {1}", VERSION, version_text);
                throw new VersionMisMatchException(errorMessage);
            }
        }

    }

    private void checkFolders()
    {
        string path = System.IO.Directory.GetCurrentDirectory();
        //List<string> folders = new List<string>() { "Il2CppData/Metadata", "Resources/unity_default_resources", "dev", 
        //    "home/web_user", "idbfs", "idbfs/4411def9d576984c8d78253236b2a62f", "proc/self/fd", "tmp" };

        List<string> folders = new List<string>() { "Resources", "idbfs", "proc", "dev", "temp" }; 
        

        foreach(string folder in folders)
		{
            checkFolderContent(folder, "\t");
		}
    }

    private void checkFolderContent(string path, string tabs)
    {
        bool doesPathExist = Directory.Exists(path);
        Debug.Log(tabs + "#########");
        Debug.Log(tabs + "\tChecking path: " + path);
        Debug.Log(tabs + "\tDoes path exist?: " + doesPathExist);

        if (doesPathExist)
        {
            //Folders
            string[] folders = System.IO.Directory.GetDirectories(path);
            if (folders.Length == 0) Debug.Log(tabs + ">> No folders found at path: " + path);
            foreach (var folder in folders)
            {
                Debug.Log(tabs + "[FOLDER]: \t" + folder);
                // folder includes the full path including parent path
                checkFolderContent(folder, tabs + "\t"); // resursive
            }

            //Files
            string[] files = System.IO.Directory.GetFiles(path);
            if (files.Length == 0) Debug.Log(tabs + ">> No files found at path: " + path);
            foreach (var file in files)
            { 
                Debug.Log(tabs + "[FILE]: \t" + file);
            }
        }
    }


    /*
    // My answer to post:
    https://answers.unity.com/questions/10364/how-can-script-tell-if-its-in-web-player-vs-standa.html?childToView=1910867#answer-1910867
    As of 11-Jul-2022

    References:
    More Application methods: https://docs.unity3d.com/ScriptReference/Application.html
    More platform types: https://docs.unity3d.com/ScriptReference/RuntimePlatform.html

    Debug.Log("Current platform: " + Application.platform); // get current platoform
    if (Application.platform == RuntimePlatform.WebGLPlayer)
    {
        // do the thing for web GL platform
    }
    else if (Application.platform == RuntimePlatform.WindowsEditor)
    {
        // do the thing for when working in Windows Editor
    }
    else if (Application.platform == RuntimePlatform.OSXEditor)
    {
        // do the thing for when working in Mac OS X Editor
    }
    else if (Application.isConsolePlatform)
    {
        // do the thing for console platforms
    }
    else if (Application.isEditor)
    {
        // do the thing for when in Unity Editor mode as oppose to a specific editor (see above if statements)
    }
    else if (Application.isMobilePlatform)
    { 
        // do the thing for mobile platform
    }
     
     */
}