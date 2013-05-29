using System;
using System.Threading;

namespace MacGreener
{
    class MainClass
    {
        const int Angle = 10;
        const int Threshold = 10;

        static void Main()
        {
            var sensor = new IOKitMotionSensor();

            Int16 prevz = 0, currz;
            bool inited = false, sleeping = false;

            while (true)
            {
                var coords = sensor.getCoords();
                currz = coords.z;

                if (inited)
                {
                    if (Math.Abs(currz - prevz) > Threshold)
                    {
                        if (prevz > currz && !sleeping && Math.Abs(coords.x) >= Angle)
                        {
                            IOKit.SleepAwake(IOKit.ASCommands.Sleep);
                            sleeping = true;
                        }
                        else if (sleeping)
                        {
                            if (Math.Abs(coords.x) < Angle && Math.Abs(coords.y) < Angle)
                            {
                                IOKit.SleepAwake(IOKit.ASCommands.Awake);
                                sleeping = false;
                            }
                        }
                    }
                }
                else
                {
                    inited = true;
                }

                prevz = currz;
                Thread.Sleep(100);
            }
        }
    }
}