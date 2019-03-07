using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class User
    {
        public string UserName { get; set; }

        //public void Send(User recipient, string message)
        //{
        //    // ez igy veszelyes mert a fogadot tul lehet terhelni
        //    // nem a fogado donti el hogy mikor dolgozza fel (vagy elfogadja-e)
        //    // + nincs lehetoseg ujraprobalkozni ha kell
        //    recipient.Receive(this, message);
        //}

        public void Receive(string message)
        {
            Console.WriteLine($"Message: {message}");
        }

        public ChatRoom room;

        public void Send(string username, string message)
        {
            room.ForwardMessage(username, message);
        }
    }

    // Mediator
    public class ChatRoom
    {
        private readonly List<User> _users = new List<User>();

        public void Enter(User user)
        {
            _users.Add(user);
            user.room = this;
        }

        internal void ForwardMessage(string userName, string message)
        {
            var recipient = _users.SingleOrDefault(u => u.UserName == userName);
            recipient?.Receive(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var user1 = new User() {UserName = "Pisti"};
            var user2 = new User() {UserName = "Jozsi" };

            var room = new ChatRoom();
            room.Enter(user1);
            room.Enter(user2);

            user1.Send("Jozsi", "Cs");
            user1.Send("Jozsi", "?");

            Console.ReadKey();
        }
    }
}
