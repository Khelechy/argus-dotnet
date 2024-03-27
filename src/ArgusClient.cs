using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using Argus.Events;
using Argus.src.Helpers;
using System.Threading;


namespace Argus
{
    public class ArgusClient
    {
        public event EventHandler<ArgusEventArgs> ArgusEventRaised;

        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        private CancellationTokenSource _tokenSource;
        private Thread _thread;

        private TcpClient _client;
        private NetworkStream _stream;

        public ArgusClient(ArgusConfig config)
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
                _tokenSource = new CancellationTokenSource();

                _client = new TcpClient();
                _client.Connect(_host, _port);

                _stream = _client.GetStream();

                SendAuthData(_username, _password);

                var buffer = new byte[1024];

                int bytesRead;


                _thread = new(() =>
                {

                    while (!_tokenSource.IsCancellationRequested)
                    {
                        bytesRead = _stream.Read(buffer, 0, buffer.Length);
                        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        if (!string.IsNullOrEmpty(response))
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

                });

                _thread.Start();

            }
            catch (Exception ex)
            {
                Dispose();
                throw new ArgusException(ex.Message, ex);
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Argus disconnection requested");
            _tokenSource?.Cancel();
            _thread.Join();
            _tokenSource?.Dispose();
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
