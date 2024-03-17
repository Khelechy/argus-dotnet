﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using Argus.Events;
using argus_dotnet.src.Helpers;

namespace Argus
{
    public class Argus
    {
        public event EventHandler<ArgusEventArgs> ArgusEventRaised;

        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        private TcpClient _client;
        private NetworkStream _stream;

        public Argus(ArgusConfig config)
        {
            this._username = config.Username ?? "";
            this._password = config.Password ?? "";

            this._host = config.Host ?? "localhost";
            this._port = config.Port == 0 ? 1337 : config.Port;

        }

        public void Connect()
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(_host, _port);

                _stream = _client.GetStream();

                SendAuthData(_username, _password);

                var buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    if (true)
                    {
                        var (isJson, argusEvent, message) = Helpers.IsJsonString(response);
                        if (isJson)
                        {
                            OnRaiseCustomEvent(new ArgusEventArgs(argusEvent));
                        }
                        else
                        {
                            Console.WriteLine("Received: " + response);
                        }
                        
                    }
                }

                _stream?.Close();
                _client?.Close();
            }
            catch (Exception ex)
            {
                Dispose();
                throw new ArgusException(ex.Message, ex);  
            }
        }

        protected virtual void OnRaiseCustomEvent(ArgusEventArgs e)
        {
            EventHandler<ArgusEventArgs> raiseEvent = ArgusEventRaised;

            // Event will be null if there are no subscribers
            if (raiseEvent != null)
            {
                // Call to raise the event.
                raiseEvent(this, e);
            }
        }

        public void Dispose()
        {
            _stream?.Close();
            _client?.Close();
        }

        private void SendAuthData(string username, string password)
        {
            string connectionString = $"<ArgusAuth>{username}:{password}</ArgusAuth>";
            byte[] buffer = Encoding.ASCII.GetBytes(connectionString);
            _stream.Write(buffer, 0, buffer.Length);
        }
    }

}
