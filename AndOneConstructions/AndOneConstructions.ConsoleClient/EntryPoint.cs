﻿namespace AndOneConstructions.ConsoleClient
{
    using AndOneConstructions.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Data.Context;

    class EntryPoint
    {
        static void Main()
        {
            //var db = new AndOneConstructionsDataContext();

            //var buildings = db.Buildings.ToArray();

            //// for testing
            //foreach (var b in buildings)
            //{
            //    Console.WriteLine(b.Name);
            //}

            //TEST MONGO
            var test = new MongoDBEmployee();
            test.PrintAllEntities();

        }
    }
}
