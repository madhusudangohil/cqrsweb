using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using cqrscommon;

namespace cqrseventlistener
{
    public class EventReader
    {
        public static void ReadQueue()
        {
            
                var files = Directory.GetFiles(@"D:\Learning\TVCQRS\Queue");
               
                foreach (var file in files)
                {
                    try
                    {
                        using (var fileStream = new FileStream(file, FileMode.Open))
                        {
                            BinaryFormatter formatter = new BinaryFormatter();

                            var eventRecord = (EventRecord)formatter.Deserialize(fileStream);

                            using (var ms = new MemoryStream(eventRecord.EventData))
                            {
                                var eventargs = (EventArgs)formatter.Deserialize(ms);
                                var eventhandler = new cqrseventlistener.EventHandler<EventArgs>(new Denormalizer(eventRecord.EntityKey));
                                eventhandler.Handle(eventargs);
                            }
                            Console.WriteLine("Event Processed from File");
                        }
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {

                    }

                }

            
        }

    }
}
