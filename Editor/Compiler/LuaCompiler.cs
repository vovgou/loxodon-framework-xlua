using Loxodon.Framework.Security.Cryptography;
using System;
using System.Diagnostics;
using System.IO;

namespace Loxodon.Framework.XLua.Editors
{
    public class LuaCompiler
    {
        private string command;
        private IEncryptor encryptor;

        public LuaCompiler(string command) : this(command, null)
        {
        }

        public LuaCompiler(string command, IEncryptor encryptor)
        {
            this.command = command;
            this.encryptor = encryptor;
        }

        public void Compile(string inputFilename, string outputFilename)
        {
            Compile(new FileInfo(inputFilename), new FileInfo(outputFilename));
        }

        public void Compile(FileInfo inputFile, FileInfo outputFile)
        {
            if (!inputFile.Exists)
            {
                UnityEngine.Debug.LogErrorFormat("Not found the file \"{0}\"", inputFile.FullName);
                return;
            }

            if (!outputFile.Directory.Exists)
                outputFile.Directory.Create();

            RunCMD(command, string.Format(" -o {0} {1}", outputFile.FullName, inputFile.FullName));

            if (this.encryptor != null && outputFile.Exists)
            {
                byte[] buffer = File.ReadAllBytes(outputFile.FullName);
                File.WriteAllBytes(outputFile.FullName, encryptor.Encrypt(buffer));
            }
        }

        public static void RunCMD(string command, string args)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = command;
                start.Arguments = args;

                start.RedirectStandardInput = true;
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;

                start.CreateNoWindow = true;
                start.ErrorDialog = true;
                start.UseShellExecute = false;

                Process process = Process.Start(start);
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();
                process.Close();

                if (!string.IsNullOrEmpty(output))
                    UnityEngine.Debug.Log(output);

                if (!string.IsNullOrEmpty(error))
                    UnityEngine.Debug.LogError(error);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
    }
}