using System;
using System.Threading;

namespace consolelife
{
    class MainClass
    {
        public static void Main(string[] args)
        {
           Life life = new Life(20, 20);
            life.SetCell(0, 1);
            life.SetCell(1, 2);
            life.SetCell(2, 0);
            life.SetCell(2, 1);
            life.SetCell(2, 2);
            ConsoleRenderer cRenderer = new ConsoleRenderer(life);

            while (true)
            {
                Console.WriteLine(cRenderer.Render());
                Thread.Sleep(100);
                life.Iterate();
                Console.Clear();
            }
        }
    }
}
