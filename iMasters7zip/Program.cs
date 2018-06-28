using System;
using System.Diagnostics;
using System.IO;

namespace iMasters7zip
{
    class Program
    {
        private static void ZipFile(string file, string outPath)
        {
            //Classe utilizada para se iniciar um processo.
            ProcessStartInfo process = new ProcessStartInfo();
            //Define qual aplicação será executado.
            process.FileName = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\7zip\7z.exe"));
            //Define os argumentos que serão utilizados pela aplicação definida no FileName, no caso abaixo o "a" define que será uma compactação e o "-p" a senha que será necessária para descompactar futuramente.
            process.Arguments = $"a \"{Path.GetFileNameWithoutExtension(file)}\" \"{file}\" \"-piMasters\"";
            //Define o diretório de trabalho.
            process.WorkingDirectory = outPath;
            //Define se o shell do sistema deve ser executado.
            process.UseShellExecute = false;
            //Define se o processo deve ser executado em uma nova janela.
            process.CreateNoWindow = true;
            //Inicia-se o processo.
            var zipFile = Process.Start(process);
            //Aguarda o encerramento do processo iniciado.
            zipFile.WaitForExit();
            //Apaga o arquivo que acabou de ser zipado.
            File.Delete(file);
        }

        private static void UnzipFile(string file, string outPath)
        {
            //Classe utilizada para se iniciar um processo.
            ProcessStartInfo process = new ProcessStartInfo();
            //Define qual aplicação será executado.
            process.FileName = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\7zip\7z.exe"));
            //Define os argumentos que serão utilizados pela aplicação definida no FileName, no caso abaixo o "x" define que será uma descompactação e o "-p" a senha para que isso seja possível".
            process.Arguments = $"x \"{file}\" \"-piMasters\"";
            //Define o diretório de trabalho.
            process.WorkingDirectory = outPath;
            // Define se o shell do sistema deve ser executado.
            process.UseShellExecute = false;
            //Define se o processo deve ser executado em uma nova janela.
            process.CreateNoWindow = true;
            //Inicia-se o processo.
            var unzipFile = Process.Start(process);
            //Aguarda o encerramento do processo iniciado.
            unzipFile.WaitForExit();
            //Apaga o arquivo que acabou de ser zipado.
            File.Delete(file);
        }

        static void Main(string[] args)
        {
            //Localiza-se a pasta para descompactar os arquivos .7z.
            var pathToUnzip = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\ToUnzip"));

            //Localiza-se a pasta para compactar seus arquivos.
            var pathToZip = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\ToZip"));
            
            //Array com todos os arquivos que seram compactados.
            var fileNameToZip = Directory.GetFiles(pathToZip);
            foreach (var file in fileNameToZip)
            {
                ZipFile(file, pathToUnzip);
            }

            //Array com todos os arquivos que seram descompactados.
            var fileNameToUnzip = Directory.GetFiles(pathToUnzip);
            foreach (var file in fileNameToUnzip)
            {
                UnzipFile(file, pathToZip);
            }
        }
    }
}
