using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.Geode.Client;
using GemFireSampleApplication;


namespace GemFireSampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReflectionBasedAutoSerializer autoSerializer = new ReflectionBasedAutoSerializer();
                Serializable.RegisterPdxSerializer(new ReflectionBasedAutoSerializer());
                CacheFactory cacheFactory = CacheFactory.CreateCacheFactory();
                cacheFactory.AddLocator("localhost", 10334);
                cacheFactory.SetPRSingleHopEnabled(true);
                cacheFactory.SetMaxConnections(-1);

                Cache cache = cacheFactory.Create();

                RegionFactory regionFactory = cache.CreateRegionFactory(RegionShortcut.PROXY);
                IRegion<int, SampleDataObject> regionInstanceType =  regionFactory.Create<int, SampleDataObject>("Region1");
                IDictionary<int, SampleDataObject> region = regionInstanceType;

                // Putting one object in at a time
                for (int i = 0; i < 1000; i++)
                {
                    SampleDataObject sampleDataObject = new SampleDataObject("firstName single " + i, "lastName " + i, i);
                    region[i] = sampleDataObject;
                }

                //Bulk inserts
                Dictionary<int, SampleDataObject> bulk = new Dictionary<int, SampleDataObject>();
                for (int i = 0; i < 1000; i++)
                {
                    SampleDataObject sampleDataObject = new SampleDataObject("firstName bulk " + i, "lastName " + i, i);
                    bulk[i] = sampleDataObject;
                    if (i % 100 == 0)
                    {
                        regionInstanceType.PutAll(bulk);
                        bulk.Clear(); 
                    }
                }
                if(bulk.Count() > 0)
                {
                    regionInstanceType.PutAll(bulk);
                    bulk.Clear();
                }
                // iterating over the keys that should be in GemFire
                for (int i = 0; i < 1000; i++)
                {
                    SampleDataObject sampleDataObject = region[i];
                    Console.WriteLine("item - " + i + " : " + sampleDataObject);
                }

                //Simple method to get all of the keys if we want to iterate over keys.
                Console.WriteLine("Number of Object in GemFire " + regionInstanceType.Keys.Count);

                //Query GemFire for the data we need.   Don't forget to setup an index.
                QueryService<int, SampleDataObject> queryService = cache.GetQueryService<int, SampleDataObject>();
                Query<SampleDataObject> query = queryService.NewQuery("select * from /Region1 where id = $1");
                object[] queryParams = { 100 };
                ISelectResults<SampleDataObject> results = query.Execute(queryParams);

                foreach (SampleDataObject si in results)
                {
                    Console.WriteLine("result " + si);
                }
            }
            // An exception should not occur
            catch (GeodeException gfex)
            {
                Console.WriteLine("BasicOperations Geode Exception: {0}", gfex.Message);
            }
            Console.WriteLine("done");
        }
    }
}
