﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppLembrete.Droid
{
    internal class FileAccessHelper //ele vai ajudar no acesso dos arquivos do banco
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);//cria uma pasta no cell do user, para salvar os arquivos do DB 
            return System.IO.Path.Combine(path, filename);// IO ->InputOutput
        }
    }
}