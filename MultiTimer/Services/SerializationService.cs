using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MultiTimer.Services
{
    public class SerializationService
    {
        private static string _CredentialsFilePath
        {
            get => string.Concat(Directory.GetCurrentDirectory(), "\\MultiTimer_Cred.xml");
        }

        private static string _TimeSheetEntryFilePath
        {
            get => string.Concat(Directory.GetCurrentDirectory(), "\\MultiTimer_TimeSheets.xml");
        }

        private static void Decrypt(string filePath)
        {
            File.Decrypt(filePath);
        }

        private static object Deserialize(Type type, string filePath)
        {
            XmlSerializer serializer;

            serializer = new XmlSerializer(type);
            using Stream reader = new FileStream(filePath, FileMode.Open);
            return serializer.Deserialize(reader);
        }

        private static void Encrypt(string filePath)
        {
            File.Encrypt(filePath);
        }

        private static void Serialize(object obj, Type type, string filePath)
        {
            XmlSerializer serializer;
            TextWriter writer;

            serializer = new XmlSerializer(type);
            writer = new StreamWriter(filePath);

            serializer.Serialize(writer, obj);
            writer.Close();
        }

        public static KeyValuePair<string, string> DeserializeCredentials()
        {
            KeyValuePair<string, string> credentials;

            Decrypt(_CredentialsFilePath);
            credentials = (KeyValuePair<string, string>)Deserialize(typeof(KeyValuePair<string, string>), _CredentialsFilePath);
            Encrypt(_CredentialsFilePath);

            return credentials;
        }

        public static TimesheetEntry DeserializeTimeSheetEntry()
        {
            return (TimesheetEntry)Deserialize(typeof(TimesheetEntry), _TimeSheetEntryFilePath);
        }

        public static void SerializeTimeSheet(TimesheetEntry timeSheetEntry)
        {
            Serialize(timeSheetEntry, typeof(TimesheetEntry), _TimeSheetEntryFilePath);
        }

        public static void SerializeCredentials(KeyValuePair<string, string> userCredentials)
        {
            Serialize(userCredentials, typeof(byte[]), _CredentialsFilePath);
            Encrypt(_CredentialsFilePath);
        }
    }
}