using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor_2
{
    public abstract class Player
    {
        public abstract void Attack(Dragon dragon);
        public abstract void Attack(Demon demon);
    }

    public class Wizard : Player
    {
        public override void Attack(Dragon dragon)
        {
            dragon.Serul(this);
        }

        public override void Attack(Demon demon)
        {
            demon.Serul(this);
        }
    }

    public class Warrior : Player
    {
        public override void Attack(Dragon dragon)
        {
            dragon.Serul(this);
        }

        public override void Attack(Demon demon)
        {
            demon.Serul(this);
        }
    }

    public abstract class Monster
    {
        protected int health = 10;

        // problem: Dependency inversion
        //          + new player impl -> every monster needs to be modified
        //          + MOnsterHorde.Serul(Attacker a)-ra ami foreach-el a monstereken nem tud ráilleszedni
        //public abstract void Serul(Wizard attacker);
        //public abstract void Serul(Warrior attacker);

        public abstract void Serul(Player attacker);
    }

    public class Dragon : Monster
    {
        // TODO FIX
        public override void Serul(Player attacker)
        {
            attacker.Attack(this);
        }
    }

    public class Demon : Monster
    {
        public override void Serul(Player attacker)
        {
            attacker.Attack(this);
        }
    }

    public class MonsterHorde
    {
        public List<Monster> Monsters { get; private set; }

        public MonsterHorde()
        {
            Monsters = new List<Monster>();
        }

        public void Serul(Player attacker)
        {
            foreach (var monster in Monsters)
            {
                monster.Serul(attacker);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var wizard = new Wizard();

            var horde = new MonsterHorde();
            horde.Monsters.Add(new Dragon());
            horde.Monsters.Add(new Demon());

            horde.Serul(wizard);
        }
    }
}
