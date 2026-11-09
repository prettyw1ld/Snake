using Common;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;

namespace Snake
{
    class Program
    {
        public static List<Leaders> Leaders = new List<Leaders>();
        public static List<ViewModelUserSettings> remoteIPAddress = new List<ViewModelUserSettings>();
        public static List<ViewModelGames> viewModelGames = new List<ViewModelGames>();
        private static int localPort = 5001;
        public static int MaxSpeed = 15;

        public void Main()
        {

        }

        private static void Send()
        {
            foreach (ViewModelUserSettings User in remoteIPAddress)
            {
                UdpClient sender = new();
                IPEndPoint endPoint = new IPEndPoint(
                   IPAddress.Parse(User.IPAddress),
                   int.Parse(User.Port)
                );

                try
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(viewModelGames
                        .Find(x => x.IdSnake == User.IdSnake)));

                    sender.Send(bytes, bytes.Length, endPoint);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Отправил данные пользователю: {User.IPAddress}:{User.Port}");
                } 

                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Возникло исключение: " + ex.ToString() + "\n" + ex.Message);
                }
                finally
                {
                    sender.Close();
                }
            }
        }

        public static void Reciever()
        {
            UdpClient receivingUdpClient = new UdpClient(localPort);
            IPEndPoint RemoteIpEndPoint = null;

            try
            {
                Console.WriteLine("Команды сервера");

                while(true)
                {
                    byte[] receiveBytes = receivingUdpClient.Receive(
                        ref RemoteIpEndPoint);

                    string returnData = Encoding.UTF8.GetString(receiveBytes);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Получил команду: " + returnData.ToString());

                    if (returnData.ToString().Contains("/start"))
                    {
                        string[] dataMessage = returnData.ToString().Split("|");
                        ViewModelUserSettings viewModelUserSettings = JsonConvert.DeserializeObject<ViewModelUserSettings>(dataMessage[1]);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Подключился пользователь: {viewModelUserSettings.IPAddress}:{viewModelUserSettings.Port}");
                        remoteIPAddress.Add(viewModelUserSettings);
                        viewModelUserSettings.IdSnake = 
                    }
                }
            }
        }

        public static int AddSnake()
        {
            ViewModelGames viewModelGamesPlayer = new();
            viewModelGamesPlayer.SnakesPlayers = new Snakes()
            {
                Points = new List<Snakes.Point>()
                {
                    new() {X = 30, Y = 10},
                    new() {X = 20, Y = 10},
                    new() {X = 10, Y = 10},

                },
                direction = Snakes.Direction.Start
            };

            viewModelGamesPlayer.Points = new Snakes.Point(new Random().Next(10, 783), new Random().Next(10, 410));
            viewModelGames.Add(viewModelGamesPlayer);

            return viewModelGames.FindIndex(x => x == viewModelGamesPlayer);
        }

    }
}
