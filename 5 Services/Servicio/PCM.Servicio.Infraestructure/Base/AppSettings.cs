using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace PCM.Servicio.Infraestructure.Base
{
    [Serializable()]
    public class AppSettings 
    {
        public string LocalConnection;

        public void save()
        {
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 }; // Where to store these keys is the tricky part, 
                                                     // you may need to obfuscate them or get the user to input a password each time
            byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            Stream stream = File.Open("ServiceConfig.xml", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            // Encryption
            using (var cryptoStream = new CryptoStream(stream, des.CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                // This is where you serialize the class
                formatter.Serialize(cryptoStream, this);
            }
            //BinaryFormatter formatter = new BinaryFormatter();

            //formatter.Serialize(stream, this);
            stream.Close();

            //Empties obj.
            // obj = null;

        }

        public void load()
        {
            byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 }; // Where to store these keys is the tricky part, 
                                                     // you may need to obfuscate them or get the user to input a password each time
            byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };
            //Opens file "data.xml" and deserializes the object from it.
            Stream stream = File.Open("ServiceConfig.xml", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //formatter = new BinaryFormatter();
            //  AppSettings obj = new AppSettings();
            using (var cryptoStream = new CryptoStream(stream, des.CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                // This is where you deserialize the class
                AppSettings oAppSettings = new AppSettings();
               // oAppSettings = (AppSettings)formatter.Deserialize(cryptoStream);
               this.LocalConnection = ((AppSettings)formatter.Deserialize(cryptoStream)).LocalConnection;
            }

            //this.LocalConnection = ((AppSettings)formatter.Deserialize(stream)).LocalConnection;
            stream.Close();

            // Console.WriteLine("");
            //  Console.WriteLine("After deserialization the object contains: ");
            // obj.Print();

        }

    }
}
