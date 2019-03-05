using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    // Wrong
    public class BadBusinessLogicComponent
    {
        //public double GetHappyUserPercentage()
        //{
        //    var happyUsers = 20.0;
        //    var dataAccessComponent = CreateDataAccessComponent();
        //    var allUsers = dataAccessComponent.GetData();

        //    return happyUsers / allUsers;
        //}

        // Wrong
        //    private IDataAccessComponent CreateDataAccessComponent()
        //    {
        //        // ez nem factory method
        //        // harmadik/sokadik implementacio-t nem konnyu bedrotozni
        //        // + ilyenkor bele kell modositani a class-ba
        //        // helyes factory method használatkor ilyenkor mindig uj kodot kene irni
        //        if (Configuration.GetSetting("IsInTest") == "Mirror")
        //        {
        //            return new TestDataAccessComponent();
        //        }
        //        else if (Configuration.GetSetting("IsInTest") == "Remote")
        //        {
        //            return new LocalDataAccessComponent();
        //        }
        //        else
        //        {
        //            return new DataAccessComponent();
        //        }

        //    }

        // szinten rossz reflection-el factory-zni
        // business logic-ban amugy se szabad reflection-t rakni
        // reflection-ös rész nem unit testelhető
    }

    public interface IDataAccessComponent
    {
        int GetData();
    }

    public class DataAccessComponent : IDataAccessComponent
    {
        public int GetData()
        {
            // SELECT Count(*) FROM People
            return new Random().Next(1, 41);
        }
    }

    public abstract class BusinessLogicComponent
    {
        public double GetHappyUserPercentage()
        {
            var happyUsers = 20.0;
            var dataAccessComponent = CreateDataAccessComponent();
            var allUsers = dataAccessComponent.GetData();

            return happyUsers / allUsers;
        }

        protected abstract IDataAccessComponent CreateDataAccessComponent();
    }

    public class ProductionBusinessLogicComponent : BusinessLogicComponent
    {
        protected override IDataAccessComponent CreateDataAccessComponent() => new DataAccessComponent();
    }

    public class TestDataAccessComponent : IDataAccessComponent
    {
        public int TestValue { get; set; }

        public int GetData() => TestValue;
    }

    public class TestBusinessLogicComponent : BusinessLogicComponent
    {
        public int TestValue { get; set; }

        protected override IDataAccessComponent CreateDataAccessComponent() => new TestDataAccessComponent() { TestValue = TestValue };
    }

    class Program
    {
        static void Test()
        {
            var expectedResult = 1;

            var blc = new TestBusinessLogicComponent() { TestValue = 20 };

            if (Math.Abs(expectedResult - blc.GetHappyUserPercentage()) < 0.001)
            {
                Console.WriteLine("Test OK");
            }
            else
            {
                Console.WriteLine("Test failed");
            }
        }

        static void Main(string[] args)
        {
            var blc = new ProductionBusinessLogicComponent();
            Console.WriteLine(blc.GetHappyUserPercentage());

            Test();

            Console.ReadKey();
        }
    }
}
