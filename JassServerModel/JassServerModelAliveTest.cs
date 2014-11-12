using Jassplan.Model;
using Jassplan.JassServerModelManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JassServerModelAliveTest
{
    class RUNME
    {
        static void Main(string[] args)
        {
            Console.Write("JassServerModel Sanity Test Starting\n");
            try
            {
                JassDataModelManager mm = new JassDataModelManager("test");
                List<JassActivity> activities = mm.ActivitiesGetAll();

                Console.WriteLine("JassServerModel Sanity Test Successfuly\n");

            } catch (Exception e){

                                Console.WriteLine("JassServerModel Sanity Test Error: " + e.Message );

            }

            Console.WriteLine("Press Enter to Exit\n");
            Console.ReadLine();



        }
    }
}
